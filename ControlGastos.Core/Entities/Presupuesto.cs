using System;

namespace ControlGastos.Core.Entities
{
    public class Presupuesto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; } 
        public int TipoGastoId { get; set; }
        public TipoGasto TipoGasto { get; set; }
        public int Mes { get; set; } // 1-12
        public int Anio { get; set; }
        public decimal MontoPresupuestado { get; set; }
    }
}
