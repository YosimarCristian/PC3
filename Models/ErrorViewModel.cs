namespace PC3SALAZAR.Models
{
    public class ErrorViewModel
    {
        // Identificador único de la solicitud para fines de diagnóstico
        public string? RequestId { get; set; }

        // Indica si debe mostrarse el RequestId (true si no está vacío)
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
