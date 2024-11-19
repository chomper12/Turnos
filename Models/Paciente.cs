using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class Paciente
    {
        [Key]
        public int IdPaciente { get; set; }
        [Required (ErrorMessage ="Debes ingresar un Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage ="Debes ingresar un Apellido")]
        public string Apellido { get; set; }
        [Required (ErrorMessage ="Debes ingresar una Dirección")]
        [Display(Name ="Dirección")]
        public string Direccion { get; set; }
        [Required (ErrorMessage ="Debes ingresar un Teléfono")]
        public string Telefono { get; set; }
        [Required(ErrorMessage ="Debes ingresar un Email Valido")]
        [EmailAddress]
        public string Email { get; set; }

        public List<Turno>? Turnos { get; set; }
    }
}
