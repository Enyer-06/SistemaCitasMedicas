using System;

namespace SistemaCitasMedicas.UI.Domain.Entities
{
    public class Especialidad
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}
