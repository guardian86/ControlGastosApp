using System;

namespace ControlGastos.Core.DTOs
{
    public class DepositoDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? FondoMonetarioNombre { get; set; }
        public decimal Monto { get; set; }
    }
}
