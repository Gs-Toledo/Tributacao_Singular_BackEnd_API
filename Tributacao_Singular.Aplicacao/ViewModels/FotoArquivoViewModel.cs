using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Aplicacao.ViewModels
{
    public class FotoArquivoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public IFormFile Src { get; set; }
        public Guid idUsuario { get; set; }
    }
}
