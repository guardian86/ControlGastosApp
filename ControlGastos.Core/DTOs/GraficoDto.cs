using System;
using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Core.DTOs
{
    public class GraficoDto
    {
        [Required]
        public string? TipoGastoNombre { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Presupuestado debe ser positivo.")]
        public decimal Presupuestado { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Ejecutado debe ser positivo.")]
        public decimal Ejecutado { get; set; }
    }
}
