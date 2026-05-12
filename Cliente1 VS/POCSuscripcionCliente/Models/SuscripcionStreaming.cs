using System;

namespace POCSuscripcionCliente.Models
{
    public class SuscripcionStreaming
    {
        public int id { get; set; }
        public DateTime fechaInicio { get; set; }
        public bool activa { get; set; }
        public int dispositivosSimultaneos { get; set; }
        public double costoMensual { get; set; }
        public string plataforma { get; set; }
        public Usuario usuario { get; set; }

        public string EstadoTexto
        {
            get
            {
                return activa ? "Activa" : "Inactiva";
            }
        }

        public string UsuarioNombre
        {
            get
            {
                return usuario != null ? usuario.nombre : "";
            }
        }

        public int CodigoUsuario
        {
            get
            {
                return usuario != null ? usuario.codigo : 0;
            }
        }
    }
}