using System;
using SistemaCitasMedicas.UI.Application.DTOs;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Application.Validators
{
    public class CitaValidador : ValidadorBase, IValidador<Cita>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;

        public CitaValidador(IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository)
        {
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
        }

        public ResultadoValidacion Validar(Cita cita)
        {
            var resultado = new ResultadoValidacion();

            if (cita.PacienteId == Guid.Empty)
            {
                resultado.Errores.Add("El paciente es requerido.");
            }
            else if (_pacienteRepository.GetById(cita.PacienteId) == null)
            {
                resultado.Errores.Add("El paciente seleccionado no existe.");
            }

            if (cita.MedicoId == Guid.Empty)
            {
                resultado.Errores.Add("El médico es requerido.");
            }
            else if (_medicoRepository.GetById(cita.MedicoId) == null)
            {
                resultado.Errores.Add("El médico seleccionado no existe.");
            }

            if (!EstaEnFuturo(cita.Fecha))
            {
                resultado.Errores.Add("La fecha de la cita debe ser un día en el futuro.");
            }

            var horaInicio = new TimeSpan(8, 0, 0);
            var horaFin = new TimeSpan(18, 0, 0);
            if (cita.Hora < horaInicio || cita.Hora > horaFin)
            {
                resultado.Errores.Add("El horario de la cita debe estar entre las 08:00 AM y las 06:00 PM.");
            }

            // Validación de longitud máxima de Motivo (max 500 caracteres)
            if (!string.IsNullOrEmpty(cita.Motivo) && cita.Motivo.Length > 500)
            {
                resultado.Errores.Add("El motivo de la cita no puede exceder los 500 caracteres.");
            }

            return resultado;
        }
    }
}
