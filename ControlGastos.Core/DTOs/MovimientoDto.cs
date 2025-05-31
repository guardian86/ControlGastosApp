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


  
    public class FiltroMovimientosDto : IValidatableObject
    {
        // Usar DateTime? para permitir nulo si el DateEdit lo maneja bien o si quieres un valor vacío inicial
        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime? FechaInicio { get; set; } = DateTime.Today.AddMonths(-1); 

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        public DateTime? FechaFin { get; set; } = DateTime.Today;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
         
            if (FechaInicio.HasValue && FechaFin.HasValue)
            {
                if (FechaInicio.Value > FechaFin.Value)
                {
                   
                    yield return new ValidationResult(
                        "La fecha de inicio no puede ser posterior a la fecha de fin.", 
                        new[] { nameof(FechaFin) }
                    );


                    //yield return new ValidationResult(
                    //    "La fecha de inicio no puede ser posterior a la fecha de fin.",
                    //    new[] { nameof(FechaInicio) }
                    //);
                }
            }
            else
            {
                yield return new ValidationResult(
                    "Ambas fechas deben estar establecidas.",
                    new[] { nameof(FechaInicio), nameof(FechaFin) }
                );
            }
        }
    }

}
