using System;
using SistemaCitasMedicas.UI.Application.DTOs;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Application.Validators
{
    public class PacienteValidador : ValidadorBase, IValidador<Paciente>
    {
        public ResultadoValidacion Validar(Paciente paciente)
        {
            var resultado = new ResultadoValidacion();

            if (!ValidarCampoRequerido(paciente.Nombre))
            {
                resultado.Errores.Add("El nombre del paciente es requerido.");
            }
            else if (paciente.Nombre.Length > 100)
            {
                resultado.Errores.Add("El nombre no puede exceder los 100 caracteres.");
            }

            if (!ValidarCampoRequerido(paciente.Apellido))
            {
                resultado.Errores.Add("El apellido del paciente es requerido.");
            }
            else if (paciente.Apellido.Length > 100)
            {
                resultado.Errores.Add("El apellido no puede exceder los 100 caracteres.");
            }

            if (!ValidarTelefono(paciente.Telefono))
            {
                resultado.Errores.Add("El teléfono debe contener solo dígitos (se permiten formatos básicos) y tener entre 7 y 15 caracteres.");
            }

            if (!ValidarEmail(paciente.Email))
            {
                resultado.Errores.Add("El correo electrónico no tiene un formato válido.");
            }

            if (paciente.FechaNacimiento.Date >= DateTime.Today)
            {
                resultado.Errores.Add("La fecha de nacimiento debe ser una fecha anterior al día de hoy.");
            }

            return resultado;
        }
    }
}
