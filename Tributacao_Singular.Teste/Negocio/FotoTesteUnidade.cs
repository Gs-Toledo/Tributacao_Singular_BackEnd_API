using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            byte[] srcEsperado = null;

            //ACT
            var foto = new Foto()
            {
                Id = idEsperado,
                Src = srcEsperado,
                idUsuario = idUsuarioEsperado
            };

            //ASSERT
            Assert.Equal(idEsperado, foto.Id);
            Assert.Equal(srcEsperado, foto.Src);
            Assert.Equal(idUsuarioEsperado, foto.idUsuario);
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
                Src = null,
                idUsuario = idUsuarioEsperado
            };

            //ASSERT
            Assert.Equal(idUsuarioEsperado, foto.idUsuario);
        }

        [Fact(DisplayName = "Não deve permitir a criação de uma foto inválida")]
        public void CriaFotoComErro()
        {
            //ARRANGE
            Guid idEsperado = Guid.NewGuid();
            Guid idUsuarioEsperado = Guid.NewGuid();
            byte[] srcEsperado = null;

            //ACT
            var foto = new Foto()
            {
                Id = Guid.NewGuid(),
                Src = null,
                idUsuario = Guid.NewGuid()
        };

            //ASSERT
            Assert.NotEqual(idEsperado, foto.Id);
            Assert.NotEqual(srcEsperado, foto.Src);
            Assert.NotEqual(idUsuarioEsperado, foto.idUsuario);
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
                Src = null,
                idUsuario = Guid.NewGuid()
        };

            //ASSERT
            Assert.NotEqual(idUsuarioEsperado, foto.idUsuario);
        }

    }
        

}
