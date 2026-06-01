using System;
using System.Collections.Generic;
using System.Linq;
using SistemaCitasMedicas.UI.Application.DTOs;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;
using SistemaCitasMedicas.UI.Domain.Enums;

namespace SistemaCitasMedicas.UI.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IValidador<Cita> _validador;
        private readonly INotificacionService _notificacionService;

        public CitaService(
            ICitaRepository citaRepository, 
            IValidador<Cita> validador, 
            INotificacionService notificacionService)
        {
            _citaRepository = citaRepository;
            _validador = validador;
            _notificacionService = notificacionService;
        }

        public ResultadoOperacion AgendarCita(CitaDTO dto)
        {
            var cita = new Cita
            {
                PacienteId = dto.PacienteId,
                MedicoId = dto.MedicoId,
                Fecha = dto.Fecha,
                Hora = dto.Hora,
                Motivo = dto.Motivo,
                Estado = EstadoCita.Programada
            };

            var validacion = _validador.Validar(cita);
            if (!validacion.EsValido)
            {
                return ResultadoOperacion.Failure(string.Join("\n", validacion.Errores));
            }

            // Validación de conflictos en la agenda (excluyendo citas canceladas)
            var citasActivas = _citaRepository.GetAll().Where(c => c.Estado != EstadoCita.Cancelada);

            bool medicoOcupado = citasActivas.Any(c => 
                c.MedicoId == cita.MedicoId && 
                c.Fecha.Date == cita.Fecha.Date && 
                c.Hora == cita.Hora);

            if (medicoOcupado)
            {
                return ResultadoOperacion.Failure("El médico ya tiene una cita agendada para esa fecha y hora.");
            }

            bool pacienteOcupado = citasActivas.Any(c => 
                c.PacienteId == cita.PacienteId && 
                c.Fecha.Date == cita.Fecha.Date && 
                c.Hora == cita.Hora);

            if (pacienteOcupado)
            {
                return ResultadoOperacion.Failure("El paciente ya tiene una cita agendada para esa misma fecha y hora.");
            }

            cita.Id = Guid.NewGuid();
            _citaRepository.Add(cita);

            try
            {
                _notificacionService.EnviarRecordatorio(cita);
            }
            catch (Exception ex)
            {
                return ResultadoOperacion.Success($"Cita agendada, pero falló el envío de notificación: {ex.Message}");
            }

            return ResultadoOperacion.Success("Cita agendada exitosamente.");
        }

        public ResultadoOperacion CancelarCita(Guid citaId)
        {
            var cita = _citaRepository.GetById(citaId);
            if (cita == null)
            {
                return ResultadoOperacion.Failure("La cita no existe.");
            }

            if (cita.Estado == EstadoCita.Cancelada)
            {
                return ResultadoOperacion.Failure("La cita ya está cancelada.");
            }

            cita.Estado = EstadoCita.Cancelada;
            _citaRepository.Update(cita);

            return ResultadoOperacion.Success("Cita cancelada exitosamente.");
        }

        public ResultadoOperacion ReprogramarCita(Guid citaId, DateTime nuevaFecha, TimeSpan nuevaHora)
        {
            var cita = _citaRepository.GetById(citaId);
            if (cita == null)
            {
                return ResultadoOperacion.Failure("La cita no existe.");
            }

            if (cita.Estado == EstadoCita.Cancelada)
            {
                return ResultadoOperacion.Failure("No se puede reprogramar una cita cancelada.");
            }

            var tempCita = new Cita
            {
                PacienteId = cita.PacienteId,
                MedicoId = cita.MedicoId,
                Fecha = nuevaFecha,
                Hora = nuevaHora,
                Estado = EstadoCita.Reprogramada,
                Motivo = cita.Motivo
            };

            var validacion = _validador.Validar(tempCita);
            if (!validacion.EsValido)
            {
                return ResultadoOperacion.Failure(string.Join("\n", validacion.Errores));
            }

            // Excluir la cita actual que estamos reprogramando para evitar falsos positivos
            var citasActivas = _citaRepository.GetAll().Where(c => c.Id != citaId && c.Estado != EstadoCita.Cancelada);

            bool medicoOcupado = citasActivas.Any(c => 
                c.MedicoId == cita.MedicoId && 
                c.Fecha.Date == nuevaFecha.Date && 
                c.Hora == nuevaHora);

            if (medicoOcupado)
            {
                return ResultadoOperacion.Failure("El médico ya tiene otra cita agendada para esa fecha y hora.");
            }

            bool pacienteOcupado = citasActivas.Any(c => 
                c.PacienteId == cita.PacienteId && 
                c.Fecha.Date == nuevaFecha.Date && 
                c.Hora == nuevaHora);

            if (pacienteOcupado)
            {
                return ResultadoOperacion.Failure("El paciente ya tiene otra cita agendada para esa misma fecha y hora.");
            }

            cita.Fecha = nuevaFecha;
            cita.Hora = nuevaHora;
            cita.Estado = EstadoCita.Reprogramada;
            _citaRepository.Update(cita);

            try
            {
                _notificacionService.EnviarRecordatorio(cita);
            }
            catch
            {
                // Silently swallow notification errors in reschedule
            }

            return ResultadoOperacion.Success("Cita reprogramada exitosamente.");
        }

        public IEnumerable<Cita> ConsultarPorPaciente(Guid pacienteId)
        {
            return _citaRepository.GetByPaciente(pacienteId);
        }

        public IEnumerable<Cita> ConsultarPorMedico(Guid medicoId)
        {
            return _citaRepository.GetByMedico(medicoId);
        }

        public IEnumerable<Cita> ObtenerTodas()
        {
            return _citaRepository.GetAll();
        }

        public ResultadoOperacion EnviarRecordatorio(Guid citaId)
        {
            var cita = _citaRepository.GetById(citaId);
            if (cita == null)
            {
                return ResultadoOperacion.Failure("La cita no existe.");
            }

            try
            {
                _notificacionService.EnviarRecordatorio(cita);
                return ResultadoOperacion.Success("Recordatorio enviado exitosamente.");
            }
            catch (Exception ex)
            {
                return ResultadoOperacion.Failure($"Error al enviar recordatorio: {ex.Message}");
            }
        }
    }
}
