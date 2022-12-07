using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.FotoComandos;
using Tributacao_Singular.Aplicacao.Excecoes;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Tributacao_Singular.Aplicacao.Comandos
{
    public class FotoCommandHandler :
        IRequestHandler<AtualizarFotoComando, bool>,
        IRequestHandler<RemoverFotoComando, bool>,
        IRequestHandler<AdicionarFotoComando, bool>
    {
        private readonly IMediatorHandler mediadorHandler;
        private readonly IFotoRepositorio repositorioFoto;
        private readonly IMapper mapper;

        public FotoCommandHandler(IMediatorHandler mediadorHandler, IFotoRepositorio repositorioFoto, IMapper mapper)
        {
            this.mediadorHandler = mediadorHandler;
            this.repositorioFoto = repositorioFoto; 
            this.mapper = mapper;
        }


        public async Task<bool> Handle(AdicionarFotoComando request, CancellationToken cancellationToken)
        {
            try
            {
                var foto = new FotoViewModel();
                foto.Src = request.Src;
                foto.idUsuario = request.idUsuario;


                await repositorioFoto.Adicionar(mapper.Map<Foto>(foto));

                return true;

            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Adicionar", ex.Message));
                return false;
            }
        }

        public async Task<bool> Handle(AtualizarFotoComando request, CancellationToken cancellationToken)
        {
            try
            {
                var FotoExiste = await repositorioFoto.ObterPorId(request.Id);

                if (FotoExiste == null)
                {
                    await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Atualizar", "Não existe a foto informada."));
                    return false;
                }

                FotoExiste.Id = request.Id;
                FotoExiste.Src = request.Src;
                FotoExiste.idUsuario = request.idUsuario;

                await repositorioFoto.Atualizar(FotoExiste);

                return true;
            }
            catch (Exception ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Atualizar", ex.Message));
                return false;
            }
        }

        public async Task<bool> Handle(RemoverFotoComando request, CancellationToken cancellationToken)
        {
            try
            {
                await repositorioFoto.Remover(request.Id);
                return true;
            }
            catch (DominioException ex)
            {
                await mediadorHandler.PublicarNotificacao(new NotificacaoDominio("Remover", ex.Message));
                return false;
            }
        }

        
    }
}
