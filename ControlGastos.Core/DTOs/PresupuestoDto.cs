using System;
using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Core.DTOs
{
    public class PresupuestoDto
    {
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string? TipoGastoNombre { get; set; }

        [Range(1, 12, ErrorMessage = "Mes debe estar entre 1 y 12.")]
        public int Mes { get; set; }

        [Range(2000, 2100, ErrorMessage = "Año fuera de rango.")]
        public int Anio { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Monto debe ser positivo.")]
        public decimal MontoPresupuestado { get; set; }
    }
}
