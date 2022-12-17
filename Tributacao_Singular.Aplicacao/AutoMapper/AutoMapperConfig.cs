using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos;
using Tributacao_Singular.Aplicacao.Comandos.ClienteComandos;
using Tributacao_Singular.Aplicacao.Comandos.FotoComandos;
using Tributacao_Singular.Aplicacao.Comandos.ProdutoComandos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Modelos;

namespace Tributacao_Singular.Aplicacao.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ClienteViewModel, AdicionarClienteComando>()
              .ConstructUsing(x => new AdicionarClienteComando(
                  x.Id,
                  x.cnpj,
                  x.Produtos,
                  x.nome
           ));

            CreateMap<ClienteViewModel, AtualizarClienteComando>()
              .ConstructUsing(x => new AtualizarClienteComando(
                  x.Id,
                  x.cnpj,
                  x.Produtos,
                  x.nome
           ));

            CreateMap<ClienteViewModel, RemoverClienteComando>()
              .ConstructUsing(x => new RemoverClienteComando(
                  x.Id
           ));

            CreateMap<ProdutoViewModel, AdicionarProdutoComando>()
              .ConstructUsing(x => new AdicionarProdutoComando(
                  x.Id,
                  x.descricao,
                  x.NCM,
                  x.EAN,
                  x.Status,
                  x.ClienteId
           ));

            CreateMap<ProdutoViewModel, RemoverProdutoComando>()
              .ConstructUsing(x => new RemoverProdutoComando(
                  x.Id
           ));

            CreateMap<ProdutoViewModel, AtualizarProdutoComando>()
              .ConstructUsing(x => new AtualizarProdutoComando(
                  x.Id,
                  x.descricao,
                  x.NCM,
                  x.EAN,
                  x.Status,
                  x.CategoriaId
           ));

            CreateMap<CategoriaViewModel, RemoverCategoriaComando>()
              .ConstructUsing(x => new RemoverCategoriaComando(
                  x.Id
           ));

            CreateMap<CategoriaViewModel, AtualizarCategoriaComando>()
              .ConstructUsing(x => new AtualizarCategoriaComando(
                  x.Id,
                  x.descricao,
                  x.ICMS,
                  x.Cofins,
                  x.IPI
           ));

            CreateMap<CategoriaViewModel, AdicionarCategoriaComando>()
              .ConstructUsing(x => new AdicionarCategoriaComando(
                  x.Id,
                  x.descricao,
                  x.ICMS,
                  x.Cofins,
                  x.IPI
           ));

            CreateMap<FotoViewModel, RemoverFotoComando>()
              .ConstructUsing(x => new RemoverFotoComando(
                  x.Id
           ));

            CreateMap<FotoViewModel, AtualizarFotoComando>()
              .ConstructUsing(x => new AtualizarFotoComando(
                  x.Id,
                  x.Src,
                  x.idUsuario
           ));

            CreateMap<FotoViewModel, AdicionarFotoComando>()
              .ConstructUsing(x => new AdicionarFotoComando(
                  x.Id,
                  x.Src,
                  x.idUsuario
           ));

            CreateMap<ClienteViewModel, Cliente>().ReverseMap();

            CreateMap<ProdutoViewModel, Produto>().ReverseMap();

            CreateMap<CategoriaViewModel, Categoria>().ReverseMap();

            CreateMap<FotoViewModel, Foto>().ReverseMap();
        }
    }
}
