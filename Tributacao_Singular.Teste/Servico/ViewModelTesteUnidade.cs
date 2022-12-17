using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.ViewModels;

namespace Tributacao_Singular.Teste.Servico
{
    public class ViewModelTesteUnidade : IClassFixture<Inicializar>
    {
        private readonly ServiceProvider provedorServico;

        public ViewModelTesteUnidade(Inicializar inicializar)
        {
            provedorServico = inicializar.ProvedorServico;
        }

        [Fact]
        public void DeveGerarListaProdutoViewModel() 
        {
            ListaProdutoViewModel listaProduto = new()
            {
                produtoViewModels = new List<ProdutoViewModel>() { new ProdutoViewModel() }
            };

            Assert.True(listaProduto.produtoViewModels.Any());
        }

        [Fact]
        public void DeveGerarLoginUserViewModel()
        {
            LoginUserViewModel loginUser = new LoginUserViewModel();

            loginUser.Email = "joao@email.com";
            loginUser.Password = "senhaJoao";

            Assert.Equal("joao@email.com", loginUser.Email);
            Assert.Equal("senhaJoao", loginUser.Password);
        }

        [Fact]
        public void DeveGerarUserTokenViewModel()
        {
            UserTokenViewModel token = new UserTokenViewModel();

            token.Email = "joao@email.com";
            token.Claims = new List<ClaimViewModel>();
            token.Id = new Guid("dfffc6c1-b8d7-41bd-affd-762fcad11514").ToString();

            Assert.Equal("joao@email.com", token.Email);
            Assert.Equal(new Guid("dfffc6c1-b8d7-41bd-affd-762fcad11514").ToString(), token.Id);
        }

        [Fact]
        public void DeveGerarLoginResponseViewModel()
        {
            LoginResponseViewModel loginResponse = new LoginResponseViewModel();

            loginResponse.AccessToken = String.Empty;
            loginResponse.UserToken = new UserTokenViewModel();
            loginResponse.ExpiresIn = 2;

            Assert.Equal(String.Empty, loginResponse.AccessToken);
            Assert.Equal(2, loginResponse.ExpiresIn);
        }

        [Fact]
        public void DeveGerarClaimViewModel()
        {
            ClaimViewModel claim = new ClaimViewModel();

            claim.Type = "Administrador";
            claim.Value = "Teste";

            Assert.Equal("Teste", claim.Value);
            Assert.Equal("Administrador", claim.Type);
        }

        [Fact]
        public void DeveGerarRegisterClienteViewModel()
        {
            RegisterClienteViewModel registerCliente = new RegisterClienteViewModel();

            registerCliente.nome = "Joao";
            registerCliente.cnpj = "11111111111";
            registerCliente.Email = "joao@email.com";
            registerCliente.Password = "!Abc123";

            Assert.Equal("joao@email.com", registerCliente.Email);
            Assert.Equal("11111111111", registerCliente.cnpj);
            Assert.Equal("Joao", registerCliente.nome);
            Assert.Equal("!Abc123", registerCliente.Password);
        }

        [Fact]
        public void DeveGerarUpdateUserViewModel()
        {
            UpdateUserViewModel updateUser = new UpdateUserViewModel();

            updateUser.Email = "joao@email.com";
            updateUser.Password = "!Abc123";

            Assert.Equal("joao@email.com", updateUser.Email);
            Assert.Equal("!Abc123", updateUser.Password);
        }

        [Fact]
        public void DeveGerarRegisterUserViewModel()
        {
            RegisterUserViewModel registerUser = new RegisterUserViewModel();

            registerUser.Email = "joao@email.com";
            registerUser.Password = "!Abc123";
            registerUser.ConfirmPassword = "!Abc123";

            Assert.Equal("joao@email.com", registerUser.Email);
            Assert.Equal("!Abc123", registerUser.Password);
            Assert.Equal("!Abc123", registerUser.ConfirmPassword);
        }

        [Fact]
        public void DeveGerarFotoArquivoViewModel()
        {
            FotoArquivoViewModel registerUser = new FotoArquivoViewModel();

            registerUser.idUsuario = new Guid("dfffc6c1-b8d7-41bd-affd-762fcad11514");
            registerUser.Id = new Guid("dfffc6c1-b8d7-41bd-affd-762fcad11514");

            Assert.Equal(new Guid("dfffc6c1-b8d7-41bd-affd-762fcad11514"), registerUser.Id);
            Assert.Equal(new Guid("dfffc6c1-b8d7-41bd-affd-762fcad11514"), registerUser.idUsuario);
        }
    }
}
