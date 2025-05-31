using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGastos.Core.DTOs
{
    public class DepositoWriteDto
    {
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "Debe seleccionar un fondo monetario.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un fondo monetario válido.")]
        public int FondoMonetarioId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El monto debe ser positivo.")]
        public decimal Monto { get; set; }
    }
}
