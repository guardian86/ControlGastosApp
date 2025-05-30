using System;
using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Core.DTOs
{
    public class FondoMonetarioDto
    {
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        [Required]
        public string? Tipo { get; set; }

        public string? Descripcion { get; set; }
    }
}
