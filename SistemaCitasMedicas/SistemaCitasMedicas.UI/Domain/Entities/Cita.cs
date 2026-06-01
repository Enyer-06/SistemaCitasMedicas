using System;
using SistemaCitasMedicas.UI.Domain.Enums;

namespace SistemaCitasMedicas.UI.Domain.Entities
{
    public class Cita
    {
        public Guid Id { get; set; }
        public Guid PacienteId { get; set; }
        public Guid MedicoId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public EstadoCita Estado { get; set; }
        public string Motivo { get; set; } = string.Empty;
    }
}
