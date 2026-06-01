using System.Collections.Generic;

namespace SistemaCitasMedicas.UI.Application.DTOs
{
    public class ResultadoValidacion
    {
        public bool EsValido => Errores.Count == 0;
        public List<string> Errores { get; set; } = new List<string>();
    }
}
