using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class Medico
    {
        [Key]
        public int IdMedico { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar una Direccion")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Telefono")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Debe ingresar un Email")]
        [EmailAddress(ErrorMessage = "No es una direccion de Email correcta")]
        public string Email { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime HorarioAtencionDesde { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime HorarioAtencionHasta { get; set; }

        public List<MedicoEspecialidad>? MedicoEspecialidad { get; set; }

        public List<Turno>? Turnos { get; set; } // Asegúrate de que esta propiedad esté definida
    }
}
