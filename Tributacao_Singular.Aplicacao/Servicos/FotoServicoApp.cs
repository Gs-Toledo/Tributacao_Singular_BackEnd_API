using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.FotoComandos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;

namespace Tributacao_Singular.Aplicacao.Servicos
{
    public class FotoServicoApp : IFotoServicoApp
    {
        private readonly IMapper mapper;
        private readonly IMediatorHandler mediador;
        private readonly IFotoRepositorio repositorioFoto;

        public FotoServicoApp(IMapper mapper, IMediatorHandler mediador, IFotoRepositorio repositorioFoto)
        {
            this.mapper = mapper;
            this.mediador = mediador;
            this.repositorioFoto = repositorioFoto;
        }

        public async Task<bool> AdicionarAsync(FotoViewModel fotoViewModel)
        {
            var adicionarComando = mapper.Map<AdicionarFotoComando>(fotoViewModel);
            return await mediador.EnviarComando(adicionarComando);
        }

        public async Task<bool> AtualizarAsync(FotoViewModel fotoViewModel)
        {
            var adicionarComando = mapper.Map<AtualizarFotoComando>(fotoViewModel);
            return await mediador.EnviarComando(adicionarComando);
        }

        public async Task<FotoViewModel> RecuperarFoto(Guid idUsuario)
        {
            var foto = await repositorioFoto.RecuperarFotoUsuario(idUsuario);

            return mapper.Map<FotoViewModel>(foto);
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var fotoViewModel = new FotoViewModel();
            fotoViewModel.Id = id;

            var adicionarComando = mapper.Map<RemoverFotoComando>(fotoViewModel);
            return await mediador.EnviarComando(adicionarComando);
        }
    }
}
