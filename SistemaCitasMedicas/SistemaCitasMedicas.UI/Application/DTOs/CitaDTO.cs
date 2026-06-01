using System;

namespace SistemaCitasMedicas.UI.Application.DTOs
{
    public class CitaDTO
    {
        public Guid PacienteId { get; set; }
        public Guid MedicoId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Motivo { get; set; } = string.Empty;
    }
}
