using System;
using System.Collections.Generic;
using System.Linq;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Infrastructure.Repositories
{
    public class EspecialidadRepository : IEspecialidadRepository
    {
        private readonly List<Especialidad> _especialidades = new List<Especialidad>();

        public IEnumerable<Especialidad> GetAll()
        {
            return _especialidades.ToList();
        }

        public Especialidad? GetById(Guid id)
        {
            return _especialidades.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Especialidad entity)
        {
            _especialidades.Add(entity);
        }

        public void Update(Especialidad entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Nombre = entity.Nombre;
                existing.Descripcion = entity.Descripcion;
            }
        }

        public void Delete(Guid id)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                _especialidades.Remove(existing);
            }
        }
    }
}
