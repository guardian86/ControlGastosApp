using System;
using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Core.DTOs
{
    public class DepositoDto
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string? FondoMonetarioNombre { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Monto debe ser positivo.")]
        public decimal Monto { get; set; }
    }
}
