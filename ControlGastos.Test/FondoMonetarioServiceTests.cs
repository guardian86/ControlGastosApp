using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ControlGastos.Application.Services;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Core.Entities;

namespace ControlGastos.Test
{
    [TestClass]
    public class FondoMonetarioServiceTests
    {
        [TestMethod]
        public void ObtenerTodos_DeberiaRetornarListaDeFondos()
        {
            var fondos = new List<FondoMonetario>
            {
                new FondoMonetario { Id = 1, Nombre = "Caja Principal", Saldo = 1000m },
                new FondoMonetario { Id = 2, Nombre = "Banco", Saldo = 5000m }
            };
            var repoMock = new Mock<IFondoMonetarioRepository>();
            repoMock.Setup(r => r.ObtenerTodos()).Returns(fondos);
            var service = new FondoMonetarioService(repoMock.Object);

            var resultado = service.ObtenerTodos();

            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Count());
            Assert.AreEqual("Caja Principal", resultado.First().Nombre);
        }

        [TestMethod]
        public void Crear_ConDatosValidos_DeberiaCrearCorrectamente()
        {
            var nuevo = new FondoMonetario { Nombre = "Ahorros", Saldo = 200m };
            var repoMock = new Mock<IFondoMonetarioRepository>();
            repoMock.Setup(r => r.Crear(It.IsAny<FondoMonetario>())).Returns(1);
            var service = new FondoMonetarioService(repoMock.Object);

            var idCreado = service.Crear(nuevo);

            Assert.AreEqual(1, idCreado);
            repoMock.Verify(r => r.Crear(It.IsAny<FondoMonetario>()), Times.Once);
        }

        [TestMethod]
        public void Actualizar_DeberiaActualizarCorrectamente()
        {
            var existente = new FondoMonetario { Id = 1, Nombre = "Caja Principal", Saldo = 1200m };
            var repoMock = new Mock<IFondoMonetarioRepository>();
            repoMock.Setup(r => r.Actualizar(existente)).Returns(true);
            var service = new FondoMonetarioService(repoMock.Object);

            var actualizado = service.Actualizar(existente);

            Assert.IsTrue(actualizado);
            repoMock.Verify(r => r.Actualizar(existente), Times.Once);
        }

        [TestMethod]
        public void Eliminar_DeberiaEliminarCorrectamente()
        {
            int idEliminar = 1;
            var repoMock = new Mock<IFondoMonetarioRepository>();
            repoMock.Setup(r => r.Eliminar(idEliminar)).Returns(true);
            var service = new FondoMonetarioService(repoMock.Object);

            var eliminado = service.Eliminar(idEliminar);

            Assert.IsTrue(eliminado);
            repoMock.Verify(r => r.Eliminar(idEliminar), Times.Once);
        }
    }
}
