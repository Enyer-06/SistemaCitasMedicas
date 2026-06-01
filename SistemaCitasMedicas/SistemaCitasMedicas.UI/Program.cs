using System;
using System.Windows.Forms;
using SistemaCitasMedicas.UI.Application.Services;
using SistemaCitasMedicas.UI.Application.Validators;
using SistemaCitasMedicas.UI.Infrastructure.Repositories;
using SistemaCitasMedicas.UI.Infrastructure.Notifications;
using SistemaCitasMedicas.UI.Presentation.Forms;

namespace SistemaCitasMedicas.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            global::SistemaCitasMedicas.UI.ApplicationConfiguration.Initialize();

            var especialidadRepo = new EspecialidadRepository();
            var pacienteRepo = new PacienteRepository();
            var medicoRepo = new MedicoRepository();
            var citaRepo = new CitaRepository();

            var notificacion = new EmailNotificacionService();
            var citaValidador = new CitaValidador(medicoRepo, pacienteRepo);

            var pacienteService = new PacienteService(pacienteRepo, new PacienteValidador());
            var medicoService = new MedicoService(medicoRepo, especialidadRepo, new MedicoValidador());
            var especialidadService = new EspecialidadService(especialidadRepo);
            var citaService = new CitaService(citaRepo, citaValidador, notificacion);

            System.Windows.Forms.Application.Run(new MainForm(pacienteService, medicoService, especialidadService, citaService));
        }
    }
}