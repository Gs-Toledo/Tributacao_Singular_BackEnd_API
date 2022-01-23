using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Servico.Extensoes;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Tributacao_Singular.Servico.Controllers
{
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ConfiguracaoApp _appSettings;
        private readonly ILogger _logger;

        public AuthController(IMediatorHandler mediadorHandler,
                              INotificationHandler<NotificacaoDominio> notificacoesHandler,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<ConfiguracaoApp> appSettings,
                              IUser user,
                              ILogger<AuthController> logger) : base(notificacoesHandler, mediadorHandler, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        [HttpPost("nova-conta-Administrador")]
        public async Task<IActionResult> RegistrarAdm(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return ValidateModelState(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                //add permissao de Adm
                await _userManager.AddClaimAsync(user, new Claim("Administrador", "Listar,Adicionar,Atualizar,Excluir"));

                return Response(await GerarJwt(user.Email));
            }
            foreach (var error in result.Errors)
            {
                NotifyError(String.Empty, error.Description);
            }

            return Response(registerUser);
        }

        [HttpPost("nova-conta-Cliente")]
        public async Task<IActionResult> RegistrarCliente(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return ValidateModelState(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                //add permissao de Usuario
                await _userManager.AddClaimAsync(user, new Claim("Cliente", "Listar,Adicionar,Atualizar,Excluir,AdicionarProduto"));

                await _signInManager.SignInAsync(user, false);
                return Response(await GerarJwt(user.Email));
            }
            foreach (var error in result.Errors)
            {
                NotifyError(string.Empty, error.Description);
            }

            return Response(registerUser);
        }

        [HttpPost("entrar")]
        public async Task<IActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return ValidateModelState(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation("Usuario " + loginUser.Email + " logado com sucesso");
                return Response(await GerarJwt(loginUser.Email));
            }
            if (result.IsLockedOut)
            {
                NotifyError(string.Empty, "Usuário temporariamente bloqueado por tentativas inválidas");
                return Response(loginUser);
            }

            NotifyError(string.Empty, "Usuário ou Senha incorretos");
            return Response(loginUser);
        }

        private async Task<LoginResponseViewModel> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Segredo);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
