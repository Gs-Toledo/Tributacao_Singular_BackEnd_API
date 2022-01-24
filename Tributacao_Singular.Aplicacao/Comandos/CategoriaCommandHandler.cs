using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos;
using Tributacao_Singular.Aplicacao.Excecoes;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Tributacao_Singular.Aplicacao.Comandos
{
    public class CategoriaCommandHandler :
        IRequestHandler<AtualizarCategoriaComando, bool>,
        IRequestHandler<RemoverCategoriaComando, bool>,
        IRequestHandler<AdicionarCategoriaComando, bool>
    {
        private readonly IMediatorHandler mediadorHandler;
        private readonly ICategoriaRepositorio respositorioCategoria;
        private readonly IMapper mapper;

        public CategoriaCommandHandler(IMediatorHandler mediadorHandler, ICategoriaRepositorio respositorioCategoria, IMapper mapper)
        {
            this.mediadorHandler = mediadorHandler;
            this.respositorioCategoria = respositorioCategoria;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AdicionarCategoriaComando request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request)) return false;

                var categoria = new CategoriaViewModel();
                categoria.Id = request.Id;
                categoria.descricao = request.descricao;
                categoria.Cofins = request.Cofins;
                categoria.ICMS = request.ICMS;
                categoria.IPI = request.IPI;

                await respositorioCategoria.Adicionar(mapper.Map<Categoria>(categoria));

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

        public async Task<bool> Handle(AtualizarCategoriaComando request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request)) return false;

                var CategoriaExiste = await respositorioCategoria.ObterPorId(request.Id);

                if (CategoriaExiste == null) 
                {
                    await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Atualizar", "Não Existe uma Categoria Informada."));
                    return false;
                }

                CategoriaExiste.descricao = request.descricao;
                CategoriaExiste.Cofins = request.Cofins;
                CategoriaExiste.ICMS = request.ICMS;
                CategoriaExiste.IPI = request.IPI;

                await respositorioCategoria.Atualizar(CategoriaExiste);

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

        public async Task<bool> Handle(RemoverCategoriaComando request, CancellationToken cancellationToken)
        {
            try
            {
                if (!ValidarComando(request)) return false;

                await respositorioCategoria.Remover(request.Id);

                return true;
            }
            catch (DominioException ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Remover", ex.Message));
                return false;
            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Remover", ex.Message));
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
