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
    public class DepositoServiceTests
    {
        
        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnDeposito()
        {
            var deposito = new Deposito { Id = 1, Fecha = System.DateTime.Today, FondoMonetarioId = 2, Monto = 500m };
            var repoMock = new Mock<IDepositoRepository>();
            repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(deposito);
            var service = new DepositoService(repoMock.Object);

            var resultado = await service.GetByIdAsync(1);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(1, resultado.Id);
            Assert.AreEqual(500m, resultado.Monto);
        }

        [TestMethod]
        public async Task GetByFondoMonetarioIdAsync_ShouldReturnDepositos()
        {
            var depositos = new List<Deposito>
            {
                new Deposito { Id = 1, Fecha = System.DateTime.Today, FondoMonetarioId = 2, Monto = 500m },
                new Deposito { Id = 2, Fecha = System.DateTime.Today, FondoMonetarioId = 2, Monto = 200m }
            };
            var repoMock = new Mock<IDepositoRepository>();
            repoMock.Setup(r => r.GetByFondoMonetarioIdAsync(2)).ReturnsAsync(depositos);
            var service = new DepositoService(repoMock.Object);

            var resultado = await service.GetByFondoMonetarioIdAsync(2);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Count());
        }

        [TestMethod]
        public async Task AddAsync_ShouldCallRepository()
        {
            var deposito = new Deposito { Id = 0, Fecha = System.DateTime.Today, FondoMonetarioId = 2, Monto = 100m };
            var repoMock = new Mock<IDepositoRepository>();
            repoMock.Setup(r => r.AddAsync(deposito)).Returns(Task.CompletedTask);
            var service = new DepositoService(repoMock.Object);

            await service.AddAsync(deposito);

            repoMock.Verify(r => r.AddAsync(deposito), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldCallRepository()
        {
            var deposito = new Deposito { Id = 1, Fecha = System.DateTime.Today, FondoMonetarioId = 2, Monto = 600m };
            var repoMock = new Mock<IDepositoRepository>();
            repoMock.Setup(r => r.UpdateAsync(deposito)).Returns(Task.CompletedTask);
            var service = new DepositoService(repoMock.Object);

            await service.UpdateAsync(deposito);

            repoMock.Verify(r => r.UpdateAsync(deposito), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldCallRepository()
        {
            int idEliminar = 1;
            var repoMock = new Mock<IDepositoRepository>();
            repoMock.Setup(r => r.DeleteAsync(idEliminar)).Returns(Task.CompletedTask);
            var service = new DepositoService(repoMock.Object);

            await service.DeleteAsync(idEliminar);

            repoMock.Verify(r => r.DeleteAsync(idEliminar), Times.Once);
        }
    }
}
