using System;
using System.Collections.Generic;

namespace ControlGastos.Core.Entities
{
    public class GastoDetalle
    {
        public int Id { get; set; }
        public int GastoEncabezadoId { get; set; }
        public GastoEncabezado GastoEncabezado { get; set; }
        public int TipoGastoId { get; set; }
        public TipoGasto TipoGasto { get; set; }
        public decimal Monto { get; set; }
    }
}
