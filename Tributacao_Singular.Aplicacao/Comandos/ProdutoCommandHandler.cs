using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.ProdutoComandos;
using Tributacao_Singular.Aplicacao.Excecoes;
using Tributacao_Singular.Negocio.Interfaces;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Tributacao_Singular.Aplicacao.Comandos
{
    public class ProdutoCommandHandler :
        IRequestHandler<AtualizarProdutoComando, bool>,
        IRequestHandler<RemoverProdutoComando, bool>
    {
        private readonly IMediatorHandler mediadorHandler;
        private readonly IProdutoRepositorio respositorioProduto;
        private readonly IMapper mapper;

        public ProdutoCommandHandler(IMediatorHandler mediadorHandler, IProdutoRepositorio respositorioProduto, IMapper mapper)
        {
            this.mediadorHandler = mediadorHandler;
            this.respositorioProduto = respositorioProduto;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AtualizarProdutoComando request, CancellationToken cancellationToken)
        {
            try 
            {
                if (!ValidarComando(request)) return false;

                var ProdutoExiste = await respositorioProduto.ObterPorId(request.Id);

                if (ProdutoExiste == null) 
                {
                    await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Atualizar", "Não Existe um Produto Informado."));
                    return false;
                }

                ProdutoExiste.descricao = request.descricao;
                ProdutoExiste.EAN = request.EAN;
                ProdutoExiste.NCM = request.NCM;
                ProdutoExiste.Status = request.Status;
                ProdutoExiste.CategoriaId = request.CategoriaId;

                await respositorioProduto.Atualizar(ProdutoExiste);

                return true;
            }
            catch (DominioException ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Atualizar", ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Atualizar", ex.Message));
                return false;
            }
        }

        public async Task<bool> Handle(RemoverProdutoComando request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request)) return false;

                await respositorioProduto.Remover(request.Id);

                return true;
            }
            catch (DominioException ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Atualizar", ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Atualizar", ex.Message));
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
