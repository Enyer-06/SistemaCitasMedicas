using System;
using System.Collections.Generic;
using SistemaCitasMedicas.UI.Application.DTOs;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Application.Services
{
    public class MedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IEspecialidadRepository _especialidadRepository;
        private readonly IValidador<Medico> _validador;

        public MedicoService(
            IMedicoRepository medicoRepository, 
            IEspecialidadRepository especialidadRepository, 
            IValidador<Medico> validador)
        {
            _medicoRepository = medicoRepository;
            _especialidadRepository = especialidadRepository;
            _validador = validador;
        }

        public ResultadoOperacion RegistrarMedico(Medico medico)
        {
            var validacion = _validador.Validar(medico);
            if (!validacion.EsValido)
            {
                return ResultadoOperacion.Failure(string.Join("\n", validacion.Errores));
            }

            var especialidad = _especialidadRepository.GetById(medico.EspecialidadId);
            if (especialidad == null)
            {
                return ResultadoOperacion.Failure("La especialidad seleccionada no existe.");
            }

            medico.Id = Guid.NewGuid();
            _medicoRepository.Add(medico);
            return ResultadoOperacion.Success("Médico registrado exitosamente.");
        }

        public ResultadoOperacion AsignarEspecialidad(Guid medicoId, Guid especialidadId)
        {
            var medico = _medicoRepository.GetById(medicoId);
            if (medico == null)
            {
                return ResultadoOperacion.Failure("El médico no existe.");
            }

            var especialidad = _especialidadRepository.GetById(especialidadId);
            if (especialidad == null)
            {
                return ResultadoOperacion.Failure("La especialidad no existe.");
            }

            medico.EspecialidadId = especialidadId;
            _medicoRepository.Update(medico);
            return ResultadoOperacion.Success("Especialidad asignada con éxito.");
        }

        public IEnumerable<Medico> ListarMedicos()
        {
            return _medicoRepository.GetAll();
        }
    }
}
