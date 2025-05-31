using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlGastos.Application.Services;
using ControlGastos.Core.Interfaces.Repositories;
using ControlGastos.Core.Entities;

namespace ControlGastos.Test
{
    [TestClass]
    public class TipoGastoServiceTests
    {
        [TestMethod]
        public async Task GetAllAsync_DeberiaRetornarListaDeTipoGasto()
        {
            // Arrange
            var tipos = new List<TipoGasto>
            {
                new TipoGasto { Id = 1, Codigo = "001", Nombre = "Alimentación" },
                new TipoGasto { Id = 2, Codigo = "002", Nombre = "Transporte" }
            };
            var repoMock = new Mock<ITipoGastoRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(tipos);
            var service = new TipoGastoService(repoMock.Object);

            // Act
            var resultado = await service.GetAllAsync();

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Count());
            Assert.AreEqual("Alimentación", resultado.First().Nombre);
        }

        [TestMethod]
        public async Task AddAsync_DeberiaAgregarTipoGasto()
        {
            // Arrange
            var repoMock = new Mock<ITipoGastoRepository>();
            var service = new TipoGastoService(repoMock.Object);
            var tipo = new TipoGasto { Id = 3, Codigo = "003", Nombre = "Salud" };

            // Act
            await service.AddAsync(tipo);

            // Assert
            repoMock.Verify(r => r.AddAsync(tipo), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_DeberiaActualizarTipoGasto()
        {
            // Arrange
            var repoMock = new Mock<ITipoGastoRepository>();
            var service = new TipoGastoService(repoMock.Object);
            var tipo = new TipoGasto { Id = 1, Codigo = "001", Nombre = "Alimentación" };

            // Act
            await service.UpdateAsync(tipo);

            // Assert
            repoMock.Verify(r => r.UpdateAsync(tipo), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_DeberiaEliminarTipoGasto()
        {
            // Arrange
            var repoMock = new Mock<ITipoGastoRepository>();
            var service = new TipoGastoService(repoMock.Object);
            int idEliminar = 1;

            // Act
            await service.DeleteAsync(idEliminar);

            // Assert
            repoMock.Verify(r => r.DeleteAsync(idEliminar), Times.Once);
        }
    }
}
