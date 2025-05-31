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
    public class FondoMonetarioServiceTests
    {
        [TestMethod]
        public async Task GetAllAsync_DeberiaRetornarListaDeFondos()
        {
            var fondos = new List<FondoMonetario>
            {
                new FondoMonetario { Id = 1, Nombre = "Caja Principal", Tipo = "CajaMenuda" },
                new FondoMonetario { Id = 2, Nombre = "Banco", Tipo = "CuentaBancaria" }
            };
            var repoMock = new Mock<IFondoMonetarioRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(fondos);
            var service = new FondoMonetarioService(repoMock.Object);

            var resultado = await service.GetAllAsync();

            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Count());
            Assert.AreEqual("Caja Principal", resultado.First().Nombre);
        }

        [TestMethod]
        public async Task AddAsync_DeberiaAgregarFondo()
        {
            var repoMock = new Mock<IFondoMonetarioRepository>();
            var service = new FondoMonetarioService(repoMock.Object);
            var fondo = new FondoMonetario { Id = 3, Nombre = "Nuevo Fondo", Tipo = "CajaMenuda" };

            await service.AddAsync(fondo);

            repoMock.Verify(r => r.AddAsync(fondo), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_DeberiaActualizarFondo()
        {
            var repoMock = new Mock<IFondoMonetarioRepository>();
            var service = new FondoMonetarioService(repoMock.Object);
            var fondo = new FondoMonetario { Id = 1, Nombre = "Caja Principal", Tipo = "CajaMenuda" };

            await service.UpdateAsync(fondo);

            repoMock.Verify(r => r.UpdateAsync(fondo), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_DeberiaEliminarFondo()
        {
            var repoMock = new Mock<IFondoMonetarioRepository>();
            var service = new FondoMonetarioService(repoMock.Object);
            int idEliminar = 1;

            await service.DeleteAsync(idEliminar);

            repoMock.Verify(r => r.DeleteAsync(idEliminar), Times.Once);
        }
    }
}
