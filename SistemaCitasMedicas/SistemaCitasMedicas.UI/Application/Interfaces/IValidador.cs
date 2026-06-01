using SistemaCitasMedicas.UI.Application.DTOs;

namespace SistemaCitasMedicas.UI.Application.Interfaces
{
    public interface IValidador<T>
    {
        ResultadoValidacion Validar(T entidad);
    }
}
