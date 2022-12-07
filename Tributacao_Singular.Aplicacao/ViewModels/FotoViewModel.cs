using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Aplicacao.ViewModels
{
    public class FotoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] Src { get; set; }
        public Guid idUsuario { get; set; }
    }
}
