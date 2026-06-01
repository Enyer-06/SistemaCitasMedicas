using System;
using SistemaCitasMedicas.UI.Application.DTOs;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Application.Validators
{
    public class MedicoValidador : ValidadorBase, IValidador<Medico>
    {
        public ResultadoValidacion Validar(Medico medico)
        {
            var resultado = new ResultadoValidacion();

            if (!ValidarCampoRequerido(medico.Nombre))
            {
                resultado.Errores.Add("El nombre del médico es requerido.");
            }

            if (!ValidarCampoRequerido(medico.Apellido))
            {
                resultado.Errores.Add("El apellido del médico es requerido.");
            }

            if (medico.EspecialidadId == Guid.Empty)
            {
                resultado.Errores.Add("Debe asignar una especialidad al médico.");
            }

            if (!string.IsNullOrWhiteSpace(medico.Telefono) && !ValidarTelefono(medico.Telefono))
            {
                resultado.Errores.Add("El teléfono del médico no tiene un formato válido.");
            }

            if (!string.IsNullOrWhiteSpace(medico.Email) && !ValidarEmail(medico.Email))
            {
                resultado.Errores.Add("El correo electrónico del médico no tiene un formato válido.");
            }

            return resultado;
        }
    }
}
