using System;
using System.Collections.Generic;
using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Application.Interfaces
{
    public interface ICitaRepository : IRepository<Cita>
    {
        IEnumerable<Cita> GetByPaciente(Guid id);
        IEnumerable<Cita> GetByMedico(Guid id);
    }
}
