using System;

namespace ControlGastos.Core.DTOs
{
    public class MovimientoDto
    {
        public string? Tipo { get; set; } // Gasto o Depósito
        public DateTime Fecha { get; set; }
        public string? FondoMonetarioNombre { get; set; }
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }
    }
}
