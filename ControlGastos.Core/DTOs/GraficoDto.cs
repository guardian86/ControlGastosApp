using System;

namespace ControlGastos.Core.DTOs
{
    public class GraficoDto
    {
        public string? TipoGastoNombre { get; set; }
        public decimal Presupuestado { get; set; }
        public decimal Ejecutado { get; set; }
    }
}
