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
        IRequestHandler<RemoverClienteComando, bool>
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
                    await mediadorHandler.PublicarNotificacao(new NotificacaoDominio(request.Tipo, "Já existe um Cliente com o cnpj informado."));
                    return false;
                }
                else 
                {
                    var clienteViewModel = new ClienteViewModel();
                    clienteViewModel.cnpj = request.cnpj;
                    clienteViewModel.Id = request.Id;
                    clienteViewModel.nome = request.nome;
                    clienteViewModel.Produtos = request.Produtos;

                    var cliente = mapper.Map<Cliente>(clienteViewModel);

                    await respositorioCliente.Adicionar(cliente);

                    return true;
                }
            }
            catch (DominioException ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio(request.Tipo, ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio(request.Tipo, ex.Message));
                return false;
            }
        }

        public async Task<bool> Handle(AtualizarClienteComando request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request)) return false;

                var ClienteExiste = await respositorioCliente.ObterClienteProdutosPorId(request.Id);

                var ClienteComCnpjInformado = await respositorioCliente.Buscar(x => x.cnpj.Equals(request.cnpj) && !x.Id.Equals(request.Id));

                if (ClienteExiste == null)
                {
                    await mediadorHandler.PublicarNotificacao(new NotificacaoDominio(request.Tipo, "Não Existe um Cliente Informado."));
                    return false;
                }
                else if (ClienteComCnpjInformado.Any())
                {
                    await mediadorHandler.PublicarNotificacao(new NotificacaoDominio(request.Tipo, "Existe um Cliente com CPNJ Informado."));
                    return false;
                }
                else 
                {
                    ClienteExiste.nome = request.nome;
                    ClienteExiste.cnpj = request.cnpj;

                    await respositorioCliente.Atualizar(ClienteExiste);

                    return true;
                }
            }
            catch (DominioException ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio(request.Tipo, ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio(request.Tipo, ex.Message));
                return false;
            }
        }

        public async Task<bool> Handle(RemoverClienteComando request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request)) return false;

                var listaProdutos = await respositorioProduto.ObterProdutosPorClienteId(request.Id);

                if(listaProdutos != null) 
                {
                    foreach (var item in listaProdutos)
                    {
                        await respositorioProduto.Remover(item.Id);
                    }

                    await respositorioCliente.Remover(request.Id);

                    return true;
                }
                else 
                {
                    await respositorioCliente.Remover(request.Id);

                    return true;
                }
            }
            catch (DominioException ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio(request.Tipo, ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio(request.Tipo, ex.Message));
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
