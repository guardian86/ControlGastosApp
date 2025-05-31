using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGastos.Core.DTOs
{
    // DTO para mostrar GastoDetalle
    public class GastoDetalleReadDto
    {
        public int Id { get; set; }
        public string? TipoGastoNombre { get; set; } 
        public string? TipoGastoCodigo { get; set; } 
        public decimal Monto { get; set; }
    }

    // DTO para mostrar GastoEncabezado
    public class GastoEncabezadoReadDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? FondoMonetarioNombre { get; set; } 
        public string? Observaciones { get; set; }
        public string? NombreComercio { get; set; }
        public string? TipoDocumento { get; set; }
        public List<GastoDetalleReadDto> Detalles { get; set; } = new();
    }
}
