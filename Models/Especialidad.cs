using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class Especialidad
    {
        [Key]
        public int IdEspecialidad { get; set; }
        [StringLength(200,ErrorMessage ="El campo descripci�n debe tener como maximo 200 caractares")]
        [Required(ErrorMessage ="Debe ingresar una descripci�n")]
        [Display(Name ="Descripci�n",Prompt ="Ingrese una Descripci�n")]
        public string? Descripcion { get; set; }
        public List<MedicoEspecialidad>? MedicoEspecialidad { get; set; }
    }
}