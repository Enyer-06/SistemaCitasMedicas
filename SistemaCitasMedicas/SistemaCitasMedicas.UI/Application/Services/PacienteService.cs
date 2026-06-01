using System;
using System.Collections.Generic;
using SistemaCitasMedicas.UI.Application.DTOs;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Application.Services
{
    public class PacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IValidador<Paciente> _validador;

        public PacienteService(IPacienteRepository pacienteRepository, IValidador<Paciente> validador)
        {
            _pacienteRepository = pacienteRepository;
            _validador = validador;
        }

        public ResultadoOperacion RegistrarPaciente(Paciente paciente)
        {
            var validacion = _validador.Validar(paciente);
            if (!validacion.EsValido)
            {
                return ResultadoOperacion.Failure(string.Join("\n", validacion.Errores));
            }

            paciente.Id = Guid.NewGuid();
            _pacienteRepository.Add(paciente);
            return ResultadoOperacion.Success("Paciente registrado exitosamente.");
        }

        public Paciente? ObtenerPaciente(Guid id)
        {
            return _pacienteRepository.GetById(id);
        }

        public IEnumerable<Paciente> ListarPacientes()
        {
            return _pacienteRepository.GetAll();
        }
    }
}
