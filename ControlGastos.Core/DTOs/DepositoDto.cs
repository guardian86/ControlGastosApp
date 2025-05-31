using System;
using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Core.DTOs
{
    public class DepositoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; } = DateTime.Today;

        // Para el formulario de creación/edición, este será el ID seleccionado
        [Required(ErrorMessage = "Debe seleccionar un fondo monetario.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un fondo monetario válido.")]
        public int FondoMonetarioId { get; set; }

        public string? FondoMonetarioNombre { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El monto debe ser positivo y mayor que cero.")] 
        public decimal Monto { get; set; }
    }
}
