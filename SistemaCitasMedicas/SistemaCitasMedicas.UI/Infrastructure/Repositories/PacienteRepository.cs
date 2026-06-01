using System;
using System.Collections.Generic;
using System.Linq;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly List<Paciente> _pacientes = new List<Paciente>();

        public IEnumerable<Paciente> GetAll()
        {
            return _pacientes.ToList();
        }

        public Paciente? GetById(Guid id)
        {
            return _pacientes.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Paciente entity)
        {
            _pacientes.Add(entity);
        }

        public void Update(Paciente entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Nombre = entity.Nombre;
                existing.Apellido = entity.Apellido;
                existing.Telefono = entity.Telefono;
                existing.Email = entity.Email;
                existing.FechaNacimiento = entity.FechaNacimiento;
            }
        }

        public void Delete(Guid id)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                _pacientes.Remove(existing);
            }
        }
    }
}
