namespace SistemaCitasMedicas.UI.Application.DTOs
{
    public class ResultadoOperacion
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;

        public static ResultadoOperacion Success(string mensaje = "")
        {
            return new ResultadoOperacion { Exito = true, Mensaje = mensaje };
        }

        public static ResultadoOperacion Failure(string mensaje)
        {
            return new ResultadoOperacion { Exito = false, Mensaje = mensaje };
        }
    }
}
