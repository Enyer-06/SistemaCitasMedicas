using System;
using System.Text.RegularExpressions;

namespace SistemaCitasMedicas.UI.Application.Validators
{
    public abstract class ValidadorBase
    {
        protected bool ValidarCampoRequerido(string valor)
        {
            return !string.IsNullOrWhiteSpace(valor);
        }

        protected bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        protected bool ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono)) return false;
            
            // Allow some formatting chars but verify numeric digits
            var clean = telefono.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace("+", "");
            if (clean.Length < 7 || clean.Length > 15) return false;
            
            foreach (char c in clean)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }

        protected bool EstaEnFuturo(DateTime fecha)
        {
            return fecha.Date > DateTime.Today;
        }
    }
}
