using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class PacienteController : Controller
    {
        private readonly TurnosContext _context;

        public PacienteController(TurnosContext context)
        {
            _context = context;
        }
        // GET: PacienteController1
        public async Task<ActionResult> Index()
        {

            return View(await _context.Paciente.ToListAsync());
        }

        // GET: PacienteController1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var paciente = await _context.Paciente.FirstOrDefaultAsync(p => p.IdPaciente == id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // GET: PacienteController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PacienteController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("IdPaciente,Nombre,Apellido,Direccion,Telefono,Email")] Paciente paciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(paciente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(paciente);
            }
            catch 
            {
                return View();
            }
        }

        // GET: PacienteController1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: PacienteController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("IdPaciente,Nombre,Apellido,Direccion,Telefono,Email")]Paciente paciente)
        {

                if(id != paciente.IdPaciente)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                   _context.Update(paciente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(paciente);
            
        }

        // GET: PacienteController1/Delete/5
        public ActionResult Delete(int id)
        {
            // Supongamos que tienes un contexto de base de datos llamado db
            var paciente = _context.Paciente.Find(id); // Obtener el paciente por ID
            if (paciente == null)
            {
                return NotFound(); // Si no se encuentra el paciente, retornar NotFound
            }
            return View(paciente); // Pasar el paciente a la vista
        }

        // POST: PacienteController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var paciente = _context.Paciente.Find(id); // Buscar el paciente de nuevo
                if (paciente != null)
                {
                    _context.Paciente.Remove(paciente); // Eliminar el paciente de la base de datos
                    _context.SaveChanges(); // Guardar los cambios en la base de datos
                }
                return RedirectToAction(nameof(Index)); // Redirigir al índice después de eliminar
            }
            catch
            {
                return View();
            }
        }
    }
}
