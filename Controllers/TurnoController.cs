using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class TurnoController : Controller
    {
        private readonly TurnosContext _context;
        private IConfiguration _configuration;
        public TurnoController(TurnosContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            ViewBag.IdMedico = new SelectList(_context.Medico.Select(medico => new 
            {
               IdMedico = medico.IdMedico,NombreCompleto = medico.Nombre + " "+medico.Apellido}),"IdMedico","NombreCompleto");

            ViewBag.IdPaciente = new SelectList(_context.Paciente.Select(paciente => new { IdPaciente = paciente.IdPaciente, NombreCompleto = paciente.Nombre + " " + paciente.Apellido }), "IdPaciente", "NombreCompleto");
            return View();
        }

        public JsonResult ObtenerTurnos(int idMedico)
        {
            List<Turno> turnos = _context.Turno
                .Where(t => t.IdMedico == idMedico)
                .ToList();

            // Verifica que se estén devolviendo los turnos
            if (turnos.Count == 0)
            {
                // Esto puede ayudar a depurar
                return Json(new { message = "No se encontraron turnos." });
            }

            return Json(turnos);
        }


        [HttpPost]
        public JsonResult GrabarTurno(Turno turno)
        {
            var ok = false;
            try
            {
                _context.Turno.Add(turno);
                _context.SaveChanges();
                ok = true;
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} excepcion encontrada",e.Message);
            }
            var jsonREsult = new {ok = ok};
            return Json(jsonREsult);
        }
        [HttpPost]
        public JsonResult Eliminarurnos(int idTurno)
        {
            var ok = false;
            try
            {
                var turnoEliminar = _context.Turno.Where(t => t.IdTurno == idTurno).FirstOrDefault();
                if (turnoEliminar != null)
                {
                    _context.Turno.Remove(turnoEliminar);
                    _context.SaveChanges();
                    ok = true;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Excepcion encontrada", e.Message);

            }
            var jsonREsult = new { ok = ok};
            return Json(jsonREsult);
        }
    }
}
