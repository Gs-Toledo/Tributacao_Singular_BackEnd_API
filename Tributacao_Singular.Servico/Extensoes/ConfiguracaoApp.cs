using System.Diagnostics.CodeAnalysis;

namespace Tributacao_Singular.Servico.Extensoes
{
    [ExcludeFromCodeCoverage]
    public class ConfiguracaoApp
    {
        public string Segredo { get; set; }
        public int ExpiracaoHoras { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }
}
