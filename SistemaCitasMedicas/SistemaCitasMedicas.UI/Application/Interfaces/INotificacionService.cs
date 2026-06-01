using SistemaCitasMedicas.UI.Domain.Entities;

namespace SistemaCitasMedicas.UI.Application.Interfaces
{
    public interface INotificacionService
    {
        void EnviarRecordatorio(Cita cita);
    }
}
