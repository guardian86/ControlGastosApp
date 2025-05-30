using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ControlGastos.Application.Services;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Core.Entities;
using ControlGastos.Core.DTOs;

namespace ControlGastos.Test
{
    [TestClass]
    public class MovimientoServiceTests
    {
        [TestMethod]
        public void ObtenerTodos_DeberiaRetornarListaDeMovimientos()
        {
            var movimientos = new List<Movimiento>
            {
                new { Id = 1, Tipo = "Ingreso", Monto = 1000m },
                new { Id = 2, Tipo = "Egreso", Monto = 200m }
            };
            var repoMock = new Mock<IMovimientoRepository>();
            repoMock.Setup(r => r.ObtenerTodos()).Returns(movimientos);
            var service = new MovimientoService(repoMock.Object);

            var resultado = service.ObtenerTodos();

            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Count());
            Assert.AreEqual("Ingreso", resultado.First().Tipo);
        }

        [TestMethod]
        public void Crear_ConDatosValidos_DeberiaCrearCorrectamente()
        {
            var nuevo = new { Tipo = "Ingreso", Monto = 500m };
            var repoMock = new Mock<IMovimientoRepository>();
            repoMock.Setup(r => r.Crear(It.IsAny<object>())).Returns(1);
            var service = new MovimientoService(repoMock.Object);

            var idCreado = service.Crear(nuevo);

            Assert.AreEqual(1, idCreado);
            repoMock.Verify(r => r.Crear(It.IsAny<object>()), Times.Once);
        }

        [TestMethod]
        public void Actualizar_DeberiaActualizarCorrectamente()
        {
            var existente = new { Id = 1, Tipo = "Ingreso", Monto = 1200m };
            var repoMock = new Mock<IMovimientoRepository>();
            repoMock.Setup(r => r.Actualizar(existente)).Returns(true);
            var service = new MovimientoService(repoMock.Object);

            var actualizado = service.Actualizar(existente);

            Assert.IsTrue(actualizado);
            repoMock.Verify(r => r.Actualizar(existente), Times.Once);
        }

        [TestMethod]
        public void Eliminar_DeberiaEliminarCorrectamente()
        {
            int idEliminar = 1;
            var repoMock = new Mock<IMovimientoRepository>();
            repoMock.Setup(r => r.Eliminar(idEliminar)).Returns(true);
            var service = new MovimientoService(repoMock.Object);

            var eliminado = service.Eliminar(idEliminar);

            Assert.IsTrue(eliminado);
            repoMock.Verify(r => r.Eliminar(idEliminar), Times.Once);
        }
    }
}
