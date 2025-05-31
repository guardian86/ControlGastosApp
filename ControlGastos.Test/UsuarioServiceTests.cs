using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlGastos.Application.Services;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Core.Entities;
using ControlGastos.Core.DTOs;

namespace ControlGastos.Test
{
    [TestClass]
    public class UsuarioServiceTests
    {
        [TestMethod]
        public void ObtenerTodos_DeberiaRetornarListaDeUsuarios()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nombre = "admin", Email = "admin@demo.com" },
                new Usuario { Id = 2, Nombre = "usuario", Email = "usuario@demo.com" }
            };
            var repoMock = new Mock<IUsuarioRepository>();
            repoMock.Setup(r => r.ObtenerTodos()).Returns(usuarios);
            var service = new UsuarioService(repoMock.Object);

            // Act
            var resultado = service.ObtenerTodos();

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Count());
            Assert.AreEqual("admin", resultado.First().Nombre);
        }

        [TestMethod]
        public void Crear_ConDatosValidos_DeberiaCrearCorrectamente()
        {
            // Arrange
            var nuevoUsuario = new UsuarioDto { Nombre = "nuevo", Email = "nuevo@demo.com" };
            var repoMock = new Mock<IUsuarioRepository>();
            repoMock.Setup(r => r.Crear(It.IsAny<UsuarioDto>())).Returns(1);
            var service = new UsuarioService(repoMock.Object);

            // Act
            var idCreado = service.Crear(nuevoUsuario);

            // Assert
            Assert.AreEqual(1, idCreado);
            repoMock.Verify(r => r.Crear(It.IsAny<UsuarioDto>()), Times.Once);
        }

        [TestMethod]
        public void Actualizar_DeberiaActualizarCorrectamente()
        {
            // Arrange
            var usuarioExistente = new UsuarioDto { Id = 1, Nombre = "admin", Email = "admin@demo.com" };
            var repoMock = new Mock<IUsuarioRepository>();
            repoMock.Setup(r => r.Actualizar(usuarioExistente)).Returns(true);
            var service = new UsuarioService(repoMock.Object);

            // Act
            var actualizado = service.Actualizar(usuarioExistente);

            // Assert
            Assert.IsTrue(actualizado);
            repoMock.Verify(r => r.Actualizar(usuarioExistente), Times.Once);
        }

        [TestMethod]
        public void Eliminar_DeberiaEliminarCorrectamente()
        {
            // Arrange
            int idEliminar = 1;
            var repoMock = new Mock<IUsuarioRepository>();
            repoMock.Setup(r => r.Eliminar(idEliminar)).Returns(true);
            var service = new UsuarioService(repoMock.Object);

            // Act
            var eliminado = service.Eliminar(idEliminar);

            // Assert
            Assert.IsTrue(eliminado);
            repoMock.Verify(r => r.Eliminar(idEliminar), Times.Once);
        }
    }
}
