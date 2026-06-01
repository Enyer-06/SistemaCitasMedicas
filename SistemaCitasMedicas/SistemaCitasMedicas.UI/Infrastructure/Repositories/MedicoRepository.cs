using System;
using System.Collections.Generic;
using System.Linq;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Infrastructure.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly List<Medico> _medicos = new List<Medico>();

        public IEnumerable<Medico> GetAll()
        {
            return _medicos.ToList();
        }

        public Medico? GetById(Guid id)
        {
            return _medicos.FirstOrDefault(m => m.Id == id);
        }

        public void Add(Medico entity)
        {
            _medicos.Add(entity);
        }

        public void Update(Medico entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Nombre = entity.Nombre;
                existing.Apellido = entity.Apellido;
                existing.Telefono = entity.Telefono;
                existing.Email = entity.Email;
                existing.EspecialidadId = entity.EspecialidadId;
            }
        }

        public void Delete(Guid id)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                _medicos.Remove(existing);
            }
        }
    }
}
