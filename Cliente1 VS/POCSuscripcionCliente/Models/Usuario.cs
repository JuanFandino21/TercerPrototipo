using System;

namespace POCSuscripcionCliente.Models
{
    public class Usuario
    {
        public int codigo { get; set; }
        public long numDocumento { get; set; }
        public string tipoDoc { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public string estado { get; set; }

        public string EstadoTexto
        {
            get
            {
                return estado == "AC" ? "Activo" : "Inactivo";
            }
        }
    }
}