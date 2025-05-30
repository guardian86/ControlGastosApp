using System;
using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Core.DTOs
{
    public class MovimientoDto
    {
        [Required]
        public string? Tipo { get; set; } // Gasto o Depósito

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string? FondoMonetarioNombre { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Monto debe ser positivo.")]
        public decimal Monto { get; set; }

        public string? Descripcion { get; set; }
    }
}
