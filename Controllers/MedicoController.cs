using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class MedicoController : Controller
    {
        private readonly TurnosContext _context;

        public MedicoController(TurnosContext context)
        {
            _context = context;
        }

        // GET: Medico
        public async Task<IActionResult> Index()
        {
            var medicos = await _context.Medico
                .Include(m => m.MedicoEspecialidad)
                .ThenInclude(me => me.Especialidad)
                .ToListAsync();
            return View(medicos);
        }


        // GET: Medico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
                .Where(m => m.IdMedico == id).Include(me => me.MedicoEspecialidad)
                .ThenInclude(e => e.Especialidad).FirstOrDefaultAsync();
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medico/Create
        public IActionResult Create()
        {
            if (_context == null || _context.Especialidad == null)
            {
                // Maneja el caso en que el contexto o la lista de especialidades es null.
                return NotFound("No se pudo encontrar la información de especialidades.");
            }

            var especialidades = _context.Especialidad.ToList();
            if (especialidades == null || !especialidades.Any())
            {
                // Maneja el caso en que no hay especialidades en la base de datos.
                ViewBag.ListaEspecialidades = new List<SelectListItem>();
            }
            else
            {
                ViewBag.ListaEspecialidades = new SelectList(especialidades, "IdEspecialidad", "Descripcion");
            }

            return View();
        }



        // POST: Medico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Medico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMedico,Nombre,Apellido,Direccion,Telefono,Email,HorarioAtencionDesde,HorarioAtencionHasta")] Medico medico, int IdEspecialidad)
        {
            if (ModelState.IsValid)
            {
                // Guardar el medico primero
                _context.Add(medico);
                await _context.SaveChangesAsync(); // Guardar Medico

                // Crear y guardar MedicoEspecialidad
                var medicoEspecialidad = new MedicoEspecialidad
                {
                    IdMedico = medico.IdMedico,
                    IdEspecialidad = IdEspecialidad
                };

                _context.Add(medicoEspecialidad);
                await _context.SaveChangesAsync(); // Guardar MedicoEspecialidad

                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }


        // GET: Medico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico.Where(m => m.IdMedico == id)
                .Include(me => me.MedicoEspecialidad).FirstOrDefaultAsync();
            if (medico == null)
            {
                return NotFound();
            }

            int? especialidadSeleccionada = null;
            if (medico.MedicoEspecialidad != null && medico.MedicoEspecialidad.Count > 0)
            {
                especialidadSeleccionada = medico.MedicoEspecialidad[0].IdEspecialidad;
            }

            ViewData["ListaEspecialidades"] = new SelectList(
                _context.Especialidad, "IdEspecialidad", "Descripcion", medico.MedicoEspecialidad[0].IdEspecialidad
                );
            return View(medico);
        }

        // POST: Medico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMedico,Nombre,Apellido,Direccion,Telefono,Email,HorarioAtencionDesde,HorarioAtencionHasta")] Medico medico, int IdEspecialidad)
        {
            if (id != medico.IdMedico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar los datos del médico
                    _context.Update(medico);
                    await _context.SaveChangesAsync();

                    // Buscar la entidad MedicoEspecialidad existente
                    var medicoEspecialidad = await _context.MedicoEspecialidad
                        .FirstOrDefaultAsync(me => me.IdMedico == id);

                    if (medicoEspecialidad != null)
                    {
                        // Eliminar la entidad existente
                        _context.MedicoEspecialidad.Remove(medicoEspecialidad);
                        await _context.SaveChangesAsync();
                    }

                    // Crear una nueva relación MedicoEspecialidad con la nueva especialidad
                    var nuevaMedicoEspecialidad = new MedicoEspecialidad
                    {
                        IdMedico = id,
                        IdEspecialidad = IdEspecialidad
                    };

                    _context.Add(nuevaMedicoEspecialidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.IdMedico))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }


        // GET: Medico/Delete/5
        // GET: Medico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
                .Include(m => m.MedicoEspecialidad)
                .ThenInclude(me => me.Especialidad)
                .FirstOrDefaultAsync(m => m.IdMedico == id);

            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicoEspecialidad = await _context.MedicoEspecialidad
                .FirstOrDefaultAsync(me => me.IdMedico == id);

            _context.MedicoEspecialidad.Remove(medicoEspecialidad);
            await _context.SaveChangesAsync();

            var medico = await _context.Medico.FindAsync(id);
            if (medico != null)
            {
                _context.Medico.Remove(medico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.IdMedico == id);
        }

        public string TraerHorarioAtencionDesde(int idMedico)
        {
            var horarioAtencionDesde = _context.Medico.Where(m => m.IdMedico == idMedico).FirstOrDefault().HorarioAtencionDesde;
            return horarioAtencionDesde.Hour + ":" + horarioAtencionDesde.Minute;
        }
        public string TraerHorarioAtencionHasta(int idMedico)
        {
            var horarioAtencionHasta = _context.Medico.Where(m => m.IdMedico == idMedico).FirstOrDefault().HorarioAtencionHasta;
            return horarioAtencionHasta.Hour + ":" + horarioAtencionHasta.Minute;
        }
    }
}
