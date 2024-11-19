using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class Turno
    {
        [Key]
        public int IdTurno { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }

        [Display(Name = "Fecha Hora Inicio")]
        public DateTime FechaHoraInicio { get; set; }

        [Display(Name = "Fecha Hora Fin")]
        public DateTime? FechaHoraFin { get; set; }

        // Propiedades de navegación
        public  Paciente? Paciente { get; set; }
        public Medico? Medico { get; set; }  // Asegúrate de que esta propiedad esté definida
    }
}
