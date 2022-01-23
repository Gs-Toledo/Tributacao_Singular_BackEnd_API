using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos;
using Tributacao_Singular.Aplicacao.Comandos.ClienteComandos;
using Tributacao_Singular.Aplicacao.Comandos.ProdutoComandos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Modelos;

namespace Tributacao_Singular.Aplicacao.AutoMapper
{
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

            CreateMap<CategoriaViewModel, AtualizarCategoriaComando>()
              .ConstructUsing(x => new AtualizarCategoriaComando(
                  x.Id,
                  x.descricao,
                  x.ICMS,
                  x.Cofins,
                  x.IPI
           ));

            CreateMap<ClienteViewModel, Cliente>().ReverseMap();

            CreateMap<ProdutoViewModel, Produto>().ReverseMap();

            CreateMap<CategoriaViewModel, Categoria>().ReverseMap();
        }
    }
}
