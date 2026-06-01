using System;
using System.Diagnostics;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Infrastructure.Notifications
{
    public class EmailNotificacionService : INotificacionService
    {
        public void EnviarRecordatorio(Cita cita)
        {
            // Simular el envío de un correo electrónico imprimiendo en depuración y consola
            string mensaje = $"[NOTIFICACIÓN EMAIL] Cita: {cita.Id} | Paciente: {cita.PacienteId} | Médico: {cita.MedicoId} | Fecha: {cita.Fecha:dd/MM/yyyy} a las {cita.Hora:hh\\:mm} | Estado: {cita.Estado}";
            
            Debug.WriteLine(mensaje);
            Console.WriteLine(mensaje);
        }
    }
}
