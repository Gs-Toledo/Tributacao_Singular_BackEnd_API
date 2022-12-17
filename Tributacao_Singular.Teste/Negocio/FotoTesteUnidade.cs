using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using Tributacao_Singular.Negocio.Modelos;

namespace Tributacao_Singular.Teste.Negocio
{
    public class FotoTesteUnidade
    {
        [Fact(DisplayName = "Deve criar foto com sucesso")]
        public void CriaFotoComSucesso()
        {
            //ARRANGE
            Guid idEsperado = Guid.NewGuid();
            Guid idUsuarioEsperado = Guid.NewGuid();
            byte[] srcEsperado = new byte[4];

            //ACT
            var foto = new Foto()
            {
                Id = idEsperado,
                Src = srcEsperado,
                idUsuario = idUsuarioEsperado
            };

            //ASSERT
            Assert.True(foto.EhValido());
        }

        [Fact(DisplayName = "Deve criar uma foto com id usuário válido")]
        public void CriaFotoComIdUsuarioValido()
        {
            //ARRANGE
            Guid idUsuarioEsperado = Guid.NewGuid();

            //ACT
            var foto = new Foto()
            {
                Id = Guid.NewGuid(),
                Src = new byte[4],
                idUsuario = idUsuarioEsperado
            };

            //ASSERT
            Assert.True(foto.EhValido());
        }

        [Fact(DisplayName = "Não deve permitir a criação de uma foto inválida")]
        public void CriaFotoComErro()
        {
            //ACT
            var foto = new Foto()
            {
                Id = Guid.NewGuid(),
                Src = new byte[0],
                idUsuario = Guid.NewGuid()
            };

            //ASSERT
            Assert.False(foto.EhValido());
        }

        [Fact(DisplayName = "Não deve permitir a criação de uma foto com idUsuario inválido")]
        public void CriaFotoComIdUsuarioInvalido()
        {
            //ARRANGE
            Guid idUsuarioEsperado = Guid.NewGuid();

            //ACT
            var foto = new Foto()
            {
                Id = Guid.NewGuid(),
                Src = new byte[0],
                idUsuario = It.IsAny<Guid>()
        };

            //ASSERT
            Assert.False(foto.EhValido());
        }

    }
        

}
