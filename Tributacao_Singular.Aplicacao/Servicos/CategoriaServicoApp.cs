using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;

namespace Tributacao_Singular.Aplicacao.Servicos
{
    public class CategoriaServicoApp : ICategoriaServicoApp
    {
        private readonly IMapper mapper;
        private readonly IMediatorHandler mediador;
        private readonly ICategoriaRepositorio respositorioCategoria;

        public CategoriaServicoApp(IMapper mapper, IMediatorHandler mediador, ICategoriaRepositorio respositorioCategoria)
        {
            this.mapper = mapper;
            this.mediador = mediador;
            this.respositorioCategoria = respositorioCategoria;
        }

        public async Task<bool> AdicionarAsync(CategoriaViewModel categoriaViewModel)
        {
            var adicionarComando = mapper.Map<AdicionarCategoriaComando>(categoriaViewModel);
            return await mediador.EnviarComando(adicionarComando);
        }

        public async Task<bool> AtualizarAsync(CategoriaViewModel categoriaViewModel)
        {
            var adicionarComando = mapper.Map<AtualizarCategoriaComando>(categoriaViewModel);
            return await mediador.EnviarComando(adicionarComando);
        }

        public async Task<List<CategoriaViewModel>> ListarTodosAsync()
        {
            return mapper.Map<List<CategoriaViewModel>>(await respositorioCategoria.ObterTodos());
        }

        public async Task<CategoriaViewModel> ObterPorIdAsync(Guid id)
        {
            return mapper.Map<CategoriaViewModel>(await respositorioCategoria.ObterPorId(id));
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var categoriaModel = new CategoriaViewModel();
            categoriaModel.Id = id;

            var adicionarComando = mapper.Map<RemoverCategoriaComando>(categoriaModel);
            return await mediador.EnviarComando(adicionarComando);
        }
    }
}
