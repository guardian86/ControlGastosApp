using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Core.DTOs
{
    public class GastoEncabezadoDto
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string? FondoMonetarioNombre { get; set; }

        public string? Observaciones { get; set; }
        public string? NombreComercio { get; set; }
        public string? TipoDocumento { get; set; }
        public List<GastoDetalleDto> Detalles { get; set; } = new();
    }
    public class GastoDetalleDto
    {
        public int TipoGastoId { get; set; }

        [Required]
        public string? TipoGastoNombre { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Monto debe ser positivo.")]
        public decimal Monto { get; set; }
    }
}
