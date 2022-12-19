using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Aplicacao.ViewModels
{
    public class CategoriaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public decimal ICMS { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public decimal Cofins { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public decimal IPI { get; set; }

    }
}
