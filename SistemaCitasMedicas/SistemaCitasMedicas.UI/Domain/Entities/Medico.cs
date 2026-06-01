using System;

namespace SistemaCitasMedicas.UI.Domain.Entities
{
    public class Medico
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid EspecialidadId { get; set; }
    }
}
