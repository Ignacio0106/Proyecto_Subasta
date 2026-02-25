namespace Subasta.Web.Helpers
{
    public static class SweetAlertHelper
    {
        public static string CrearNotificacion(string titulo, string mensaje, SweetAlertMessageType tipo)
        {
            return $"Swal.fire('{titulo}', '{mensaje}', '{tipo}')";
        }
    }

    public enum SweetAlertMessageType
    {
        success,
        error,
        warning,
        info
    }
}