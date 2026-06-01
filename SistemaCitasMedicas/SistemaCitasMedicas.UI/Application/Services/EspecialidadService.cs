using System;
using System.Collections.Generic;
using System.Linq;
using SistemaCitasMedicas.UI.Application.DTOs;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Application.Services
{
    public class EspecialidadService
    {
        private readonly IEspecialidadRepository _especialidadRepository;

        public EspecialidadService(IEspecialidadRepository especialidadRepository)
        {
            _especialidadRepository = especialidadRepository;
        }

        public ResultadoOperacion RegistrarEspecialidad(Especialidad especialidad)
        {
            if (string.IsNullOrWhiteSpace(especialidad.Nombre))
            {
                return ResultadoOperacion.Failure("El nombre de la especialidad es requerido.");
            }

            var todas = _especialidadRepository.GetAll();
            if (todas.Any(e => e.Nombre.Trim().Equals(especialidad.Nombre.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return ResultadoOperacion.Failure("Ya existe una especialidad registrada con este nombre.");
            }

            especialidad.Id = Guid.NewGuid();
            _especialidadRepository.Add(especialidad);
            return ResultadoOperacion.Success("Especialidad registrada exitosamente.");
        }

        public IEnumerable<Especialidad> ListarEspecialidades()
        {
            return _especialidadRepository.GetAll();
        }
    }
}
