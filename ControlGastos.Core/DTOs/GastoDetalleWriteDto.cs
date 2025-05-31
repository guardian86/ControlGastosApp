using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGastos.Core.DTOs
{
    // DTO para crear/actualizar GastoDetalle (usado dentro de GastoEncabezadoWriteDto)
    public class GastoDetalleWriteDto
    {
        // public int Id { get; set; } // Solo si vas a permitir actualizar detalles existentes por ID

        [Required(ErrorMessage = "Debe seleccionar un tipo de gasto.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de gasto válido.")]
        public int TipoGastoId { get; set; } // <--- ID del TipoGasto

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Monto { get; set; }
    }

    // DTO para crear/actualizar GastoEncabezado
    public class GastoEncabezadoWriteDto
    {
        // public int Id { get; set; } // Para actualizaciones, omite para creación
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un fondo monetario.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un fondo monetario válido.")]
        public int FondoMonetarioId { get; set; } // <--- ID del FondoMonetario

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder los 500 caracteres.")]
        public string? Observaciones { get; set; }

        [StringLength(100, ErrorMessage = "El nombre del comercio no puede exceder los 100 caracteres.")]
        public string? NombreComercio { get; set; }

        [StringLength(50, ErrorMessage = "El tipo de documento no puede exceder los 50 caracteres.")]
        public string? TipoDocumento { get; set; }

        // Lista de detalles que se van a crear/actualizar junto con el encabezado
        public List<GastoDetalleWriteDto> Detalles { get; set; } = new();
    }
}
