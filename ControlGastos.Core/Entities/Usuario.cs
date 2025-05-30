using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Core.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = "admin";
    }
}
