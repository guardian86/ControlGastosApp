using System;

namespace ControlGastos.Core.DTOs
{
    public class PresupuestoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? TipoGastoNombre { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal MontoPresupuestado { get; set; }
    }
}
