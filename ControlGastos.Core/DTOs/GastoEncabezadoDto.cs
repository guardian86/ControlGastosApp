using System;
using System.Collections.Generic;

namespace ControlGastos.Core.DTOs
{
    public class GastoEncabezadoDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? FondoMonetarioNombre { get; set; }
        public string? Observaciones { get; set; }
        public string? NombreComercio { get; set; }
        public string? TipoDocumento { get; set; }
        public List<GastoDetalleDto> Detalles { get; set; } = new();
    }
    public class GastoDetalleDto
    {
        public string? TipoGastoNombre { get; set; }
        public decimal Monto { get; set; }
    }
}
