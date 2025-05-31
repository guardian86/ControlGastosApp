using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGastos.Core.DTOs
{
    
    public class FiltroGraficoDto
    {
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
                }
            }
        }
    }
}
