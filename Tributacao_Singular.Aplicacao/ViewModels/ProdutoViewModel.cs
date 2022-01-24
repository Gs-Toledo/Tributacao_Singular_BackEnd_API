using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Aplicacao.ViewModels
{
    public class ProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo {0} precisa ter {1} caracteres")]
        public string NCM { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(13, ErrorMessage = "O campo {0} precisa ter {1} caracteres")]
        public string EAN { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa ter entre {0} e {1} caracteres"), MinLength(2)]
        public string descricao { get; set; }

        public int Status { get; set; }

        public Guid CategoriaId { get; set; }

        public Guid ClienteId { get; set; }
    }
}
