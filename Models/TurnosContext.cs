using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Models
{
    public class TurnosContext : DbContext
    {
        public TurnosContext(DbContextOptions<TurnosContext> opciones) : base(opciones)
        {
        }

        public DbSet<Especialidad> Especialidad { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<MedicoEspecialidad> MedicoEspecialidad { get; set; }
        public DbSet<Turno> Turno { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Especialidad
            modelBuilder.Entity<Especialidad>(entidad =>
            {
                entidad.ToTable("Especialidad");
                entidad.HasKey(e => e.IdEspecialidad);
                entidad.Property(e => e.Descripcion)
                      .IsRequired()
                      .HasMaxLength(200)
                      .IsUnicode(false);
            });

            // Configuración de Paciente
            modelBuilder.Entity<Paciente>(entidad =>
            {
                entidad.ToTable("Paciente");
                entidad.HasKey(p => p.IdPaciente);
                entidad.Property(p => p.Nombre)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false);
                entidad.Property(p => p.Apellido)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false);
                entidad.Property(p => p.Direccion)
                      .IsRequired()
                      .HasMaxLength(250)
                      .IsUnicode(false);
                entidad.Property(p => p.Telefono)
                      .IsRequired()
                      .HasMaxLength(20)
                      .IsUnicode(false);
                entidad.Property(p => p.Email)
                      .IsRequired()
                      .HasMaxLength(100)
                      .IsUnicode(false);
            });

            // Configuración de Medico
            modelBuilder.Entity<Medico>(entidad =>
            {
                entidad.ToTable("Medico");
                entidad.HasKey(m => m.IdMedico);
                entidad.Property(m => m.Nombre)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false);
                entidad.Property(m => m.Apellido)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false);
                entidad.Property(m => m.Direccion)
                      .IsRequired()
                      .HasMaxLength(250)
                      .IsUnicode(false);
                entidad.Property(m => m.Telefono)
                      .IsRequired()
                      .HasMaxLength(20)
                      .IsUnicode(false);
                entidad.Property(m => m.Email)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false);
                entidad.Property(m => m.HorarioAtencionDesde)
                      .IsRequired();
                entidad.Property(m => m.HorarioAtencionHasta)
                      .IsRequired();
            });

            // Configuración de MedicoEspecialidad
            modelBuilder.Entity<MedicoEspecialidad>().HasKey(x => new { x.IdMedico, x.IdEspecialidad });
            modelBuilder.Entity<MedicoEspecialidad>()
                .HasOne(x => x.Medico)
                .WithMany(p => p.MedicoEspecialidad)
                .HasForeignKey(p => p.IdMedico);
            modelBuilder.Entity<MedicoEspecialidad>()
                .HasOne(x => x.Especialidad)
                .WithMany() // Ajusta esto si tienes la relación inversa
                .HasForeignKey(x => x.IdEspecialidad);

            // Configuración de Turno
            modelBuilder.Entity<Turno>(entidad =>
            {
                entidad.ToTable("Turno");
                entidad.HasKey(m => m.IdTurno);
                entidad.Property(m => m.IdPaciente).IsRequired();
                entidad.Property(m => m.IdMedico).IsRequired();
                entidad.Property(m => m.FechaHoraInicio).IsRequired();
                entidad.Property(m => m.FechaHoraFin).IsRequired();
            });

            modelBuilder.Entity<Turno>()
                .HasOne(x => x.Paciente)
                .WithMany(p => p.Turnos)
                .HasForeignKey(p => p.IdPaciente);

            modelBuilder.Entity<Turno>()
                .HasOne(x => x.Medico)
                .WithMany(m => m.Turnos)
                .HasForeignKey(p => p.IdMedico);
        }
    }
}
