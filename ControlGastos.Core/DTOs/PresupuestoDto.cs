using System;
using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Core.DTOs
{
    public class PresupuestoDto 
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
        
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de gasto.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de gasto válido.")]
        public int TipoGastoId { get; set; } 

        public string? TipoGastoNombre { get; set; }

        [Required(ErrorMessage = "El mes es obligatorio.")]
        [Range(1, 12, ErrorMessage = "El mes debe estar entre 1 y 12.")]
        public int Mes { get; set; }

        [Required(ErrorMessage = "El año es obligatorio.")]
        [Range(2020, 2050, ErrorMessage = "Ingrese un año válido (ej. 2024).")] 
        public int Anio { get; set; }

        [Required(ErrorMessage = "El monto presupuestado es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El monto presupuestado debe ser positivo y mayor que cero.")]
        public decimal MontoPresupuestado { get; set; }
    }
}
