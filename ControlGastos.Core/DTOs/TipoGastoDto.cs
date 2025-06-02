using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGastos.Core.DTOs
{
     public class TipoGastoDto
    {
        public int Id { get; set; }

        // [Required(ErrorMessage = "El código es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no debe exceder los 20 caracteres.")]
        public string? Codigo { get; set; } 

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
        public string? Nombre { get; set; }

        [StringLength(250, ErrorMessage = "La descripción no debe exceder los 250 caracteres.")]
        public string? Descripcion { get; set; }
    }
}
