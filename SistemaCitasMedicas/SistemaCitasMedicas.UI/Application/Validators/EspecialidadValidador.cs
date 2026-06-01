using SistemaCitasMedicas.UI.Application.DTOs;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Application.Validators
{
    /// <summary>
    /// Validador específico para la entidad Especialidad.
    /// Centraliza las reglas de validación de especialidades médicas (DRY, SRP).
    /// </summary>
    public class EspecialidadValidador : ValidadorBase, IValidador<Especialidad>
    {
        public ResultadoValidacion Validar(Especialidad especialidad)
        {
            var resultado = new ResultadoValidacion();

            if (!ValidarCampoRequerido(especialidad.Nombre))
            {
                resultado.Errores.Add("El nombre de la especialidad es requerido.");
            }

            return resultado;
        }
    }
}
