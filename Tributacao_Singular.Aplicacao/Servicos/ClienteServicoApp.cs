using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.ClienteComandos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;

namespace Tributacao_Singular.Aplicacao.Servicos
{
    public class ClienteServicoApp : IClienteServicoApp
    {
        private readonly IMapper mapper;
        private readonly IMediatorHandler mediador;
        private readonly IClienteRepositorio respositorioCliente;

        public ClienteServicoApp(IMapper mapper, IMediatorHandler mediador, IClienteRepositorio respositorioCliente)
        {
            this.mapper = mapper;
            this.mediador = mediador;
            this.respositorioCliente = respositorioCliente;
        }

        public async Task<bool> AdicionarAsync(ClienteViewModel clienteViewModel) 
        {
            var adicionarComando = mapper.Map<AdicionarClienteComando>(clienteViewModel);
            return await mediador.EnviarComando(adicionarComando);
        }

        public async Task<bool> AtualizarAsync(ClienteViewModel clienteViewModel)
        {
            var adicionarComando = mapper.Map<AtualizarClienteComando>(clienteViewModel);
            return await mediador.EnviarComando(adicionarComando);
        }

        public async Task<bool> RemoverAsync(Guid Id)
        {
            var clienteModel = new ClienteViewModel();
            clienteModel.Id = Id;

            var adicionarComando = mapper.Map<RemoverClienteComando>(clienteModel);
            return await mediador.EnviarComando(adicionarComando);
        }

        public async Task<List<ClienteViewModel>> ListarTodosAsync()
        {
            return mapper.Map<List<ClienteViewModel>>( await respositorioCliente.ObterTodos() );
        }

        public async Task<ClienteViewModel> ObterPorIdAsync(Guid id)
        {
            return mapper.Map<ClienteViewModel>(await respositorioCliente.ObterPorId(id));
        }

        public async Task<ClienteViewModel> ObterClienteProdutosPorIdAsync(Guid id)
        {
            return mapper.Map<ClienteViewModel>(await respositorioCliente.ObterClienteProdutosPorId(id));
        }

        public async Task<ClienteViewModel> ObterClienteProdutosPorCnpjAsync(string cnpj)
        {
            return mapper.Map<ClienteViewModel>(await respositorioCliente.ObterClienteProdutosPorCnpj(cnpj));
        }

        public async Task<IEnumerable<ClienteViewModel>> ObterTodosClienteProdutosAsync()
        {
            return mapper.Map<IEnumerable<ClienteViewModel>>(await respositorioCliente.ObterTodosClienteProdutos());
        }
    }
}
