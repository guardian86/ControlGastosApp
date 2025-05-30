//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System.Collections.Generic;
//using System.Linq;
//using ControlGastos.Application.Services;
//using ControlGastos.Core.Interfaces.Repositories;
//using ControlGastos.Core.Entities;
//using ControlGastos.Core.DTOs;

//namespace ControlGastos.Test
//{
//    [TestClass]
//    public class GastoServiceTests
//    {
//        [TestMethod]
//        public void ObtenerTodos_DeberiaRetornarListaDeGastos()
//        {
//            var gastos = new List<Gasto>
//            {
//                new { Id = 1, Descripcion = "Compra supermercado", Monto = 150m },
//                new { Id = 2, Descripcion = "Taxi", Monto = 30m }
//            };
//            var repoMock = new Mock<IGastoRepository>();
//            repoMock.Setup(r => r.ObtenerTodos()).Returns(gastos);
//            var service = new GastoService(repoMock.Object);

//            var resultado = service.ObtenerTodos();

//            Assert.IsNotNull(resultado);
//            Assert.AreEqual(2, resultado.Count());
//            Assert.AreEqual("Compra supermercado", resultado.First().Descripcion);
//        }

//        [TestMethod]
//        public void Crear_ConDatosValidos_DeberiaCrearCorrectamente()
//        {
//            var nuevo = new { Descripcion = "Cine", Monto = 50m };
//            var repoMock = new Mock<IGastoRepository>();
//            repoMock.Setup(r => r.Crear(It.IsAny<object>())).Returns(1);
//            var service = new GastoService(repoMock.Object);

//            var idCreado = service.Crear(nuevo);

//            Assert.AreEqual(1, idCreado);
//            repoMock.Verify(r => r.Crear(It.IsAny<object>()), Times.Once);
//        }

//        [TestMethod]
//        public void Actualizar_DeberiaActualizarCorrectamente()
//        {
//            var existente = new { Id = 1, Descripcion = "Compra supermercado", Monto = 170m };
//            var repoMock = new Mock<IGastoRepository>();
//            repoMock.Setup(r => r.Actualizar(existente)).Returns(true);
//            var service = new GastoService(repoMock.Object);

//            var actualizado = service.Actualizar(existente);

//            Assert.IsTrue(actualizado);
//            repoMock.Verify(r => r.Actualizar(existente), Times.Once);
//        }

//        [TestMethod]
//        public void Eliminar_DeberiaEliminarCorrectamente()
//        {
//            int idEliminar = 1;
//            var repoMock = new Mock<IGastoRepository>();
//            repoMock.Setup(r => r.Eliminar(idEliminar)).Returns(true);
//            var service = new GastoService(repoMock.Object);

//            var eliminado = service.Eliminar(idEliminar);

//            Assert.IsTrue(eliminado);
//            repoMock.Verify(r => r.Eliminar(idEliminar), Times.Once);
//        }
//    }
//}
