using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.ClienteComandos;
using Tributacao_Singular.Aplicacao.Excecoes;
using Tributacao_Singular.Negocio.Interfaces;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;
using Tributacao_Singular.Negocio.Modelos;
using AutoMapper;
using Tributacao_Singular.Aplicacao.ViewModels;

namespace Tributacao_Singular.Aplicacao.Comandos
{
    public class ClienteCommandHandler :
        IRequestHandler<AdicionarClienteComando, bool>,
        IRequestHandler<AtualizarClienteComando, bool>, 
        IRequestHandler<RemoverClienteComando, bool>,
        IRequestHandler<AdicionarProdutoClienteComando, bool>
    {
        private readonly IMediatorHandler mediadorHandler;
        private readonly IClienteRepositorio respositorioCliente;
        private readonly IProdutoRepositorio respositorioProduto;
        private readonly IMapper mapper;

        public ClienteCommandHandler(IMediatorHandler mediadorHandler, IClienteRepositorio respositorioCliente, IMapper mapper, IProdutoRepositorio respositorioProduto)
        {
            this.mediadorHandler = mediadorHandler;
            this.respositorioCliente = respositorioCliente;
            this.mapper = mapper;
            this.respositorioProduto = respositorioProduto;
        }

        public async Task<bool> Handle(AdicionarClienteComando request, CancellationToken cancellationToken)
        {
            try 
            {
                if (!ValidarComando(request)) return false;

                var ClienteExiste = await respositorioCliente.ObterPorCnpj(request.cnpj);

                if (ClienteExiste != null) 
                {
                    await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Adicionar", "Já existe um Cliente com o cnpj informado."));
                    return false;
                }

                var clienteViewModel = new ClienteViewModel();
                clienteViewModel.cnpj = request.cnpj;
                clienteViewModel.Id = request.Id;
                clienteViewModel.nome = request.nome;
                clienteViewModel.Produtos = request.Produtos;

                var cliente = mapper.Map<Cliente>(clienteViewModel);

                await respositorioCliente.Adicionar(cliente);

                return true;
            }
            catch (DominioException ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Adicionar", ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Adicionar", ex.Message));
                return false;
            }
        }

        public async Task<bool> Handle(AtualizarClienteComando request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request)) return false;

                var clienteViewModel = new ClienteViewModel();
                clienteViewModel.cnpj = request.cnpj;
                clienteViewModel.Id = request.Id;
                clienteViewModel.nome = request.nome;
                clienteViewModel.Produtos = request.Produtos;

                var cliente = mapper.Map<Cliente>(clienteViewModel);

                await respositorioCliente.Atualizar(cliente);

                return true;
            }
            catch (DominioException ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Adicionar", ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Adicionar", ex.Message));
                return false;
            }
        }

        public async Task<bool> Handle(RemoverClienteComando request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request)) return false;

                await respositorioCliente.Remover(request.Id);

                return true;
            }
            catch (DominioException ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Adicionar", ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Adicionar", ex.Message));
                return false;
            }
        }

        public async Task<bool> Handle(AdicionarProdutoClienteComando request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request)) return false;

                var ClienteExiste = await respositorioCliente.ObterPorId(request.Id);

                if (ClienteExiste == null)
                {
                    await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Adicionar", "Não existe um Cliente informado."));
                    return false;
                }

                var clienteBd = await respositorioProduto.ObterPorClienteId(request.Id);

                var ListaProdutos = clienteBd.Produtos.ToList();

                foreach(var item in request.Produtos) 
                {
                    ListaProdutos.Add(mapper.Map<Produto>(item));
                }

                clienteBd.Produtos = ListaProdutos;

                var cliente = mapper.Map<Cliente>(clienteBd);

                await respositorioCliente.Atualizar(cliente);

                return true;
            }
            catch (DominioException ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Adicionar", ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Adicionar", ex.Message));
                return false;
            }
        }

        private bool ValidarComando(Comando mensagem)
        {
            if (mensagem.EhValido()) return true;

            foreach (var error in mensagem.ResultadoValidacao.Errors)
            {
                mediadorHandler.PublicarNotificacao(new NotificacaoDominio(mensagem.Tipo, error.ErrorMessage));
            }

            return false;
        }
    }
}
