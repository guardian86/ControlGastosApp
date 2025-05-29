using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGastos.Core.Entities
{
    public class GastoEncabezado
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int FondoMonetarioId { get; set; }
        public FondoMonetario? FondoMonetario { get; set; }
        public string? Observaciones { get; set; }
        public string? NombreComercio { get; set; }
        public string? TipoDocumento { get; set; }
        public ICollection<GastoDetalle> Detalles { get; set; } = new List<GastoDetalle>();
    }
}
