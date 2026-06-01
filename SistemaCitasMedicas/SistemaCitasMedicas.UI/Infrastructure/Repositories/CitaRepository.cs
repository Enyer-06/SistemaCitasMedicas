using System;
using System.Collections.Generic;
using System.Linq;
using SistemaCitasMedicas.UI.Application.Interfaces;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Infrastructure.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly List<Cita> _citas = new List<Cita>();

        public IEnumerable<Cita> GetAll()
        {
            return _citas.ToList();
        }

        public Cita? GetById(Guid id)
        {
            return _citas.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Cita entity)
        {
            _citas.Add(entity);
        }

        public void Update(Cita entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.PacienteId = entity.PacienteId;
                existing.MedicoId = entity.MedicoId;
                existing.Fecha = entity.Fecha;
                existing.Hora = entity.Hora;
                existing.Estado = entity.Estado;
                existing.Motivo = entity.Motivo;
            }
        }

        public void Delete(Guid id)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                _citas.Remove(existing);
            }
        }

        public IEnumerable<Cita> GetByPaciente(Guid id)
        {
            return _citas.Where(c => c.PacienteId == id).ToList();
        }

        public IEnumerable<Cita> GetByMedico(Guid id)
        {
            return _citas.Where(c => c.MedicoId == id).ToList();
        }
    }
}
