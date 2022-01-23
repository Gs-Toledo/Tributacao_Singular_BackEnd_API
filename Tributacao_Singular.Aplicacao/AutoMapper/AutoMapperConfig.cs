using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.ClienteComandos;
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

            CreateMap<ClienteViewModel, AdicionarProdutoClienteComando>()
              .ConstructUsing(x => new AdicionarProdutoClienteComando(
                  x.Id,
                  x.Produtos
           ));

            CreateMap<ClienteViewModel, Cliente>().ReverseMap();

            CreateMap<ProdutoViewModel, Produto>().ReverseMap();

        }
    }
}
