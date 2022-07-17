using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Mensaje
    {
        public int IdMensaje { get; set; }
        public ML.Usuario Usuario { get; set; }
        public string Texto { get; set; }
        public List<object> Mensajes { get; set; }
        public ML.MensajeGrupo MensajeGrupo { get; set; }
        public ML.GrupoUsuario GrupoUsuario { get; set; }
        public ML.Grupo Grupo { get; set; }
        public int IdUsuario { get; set; }

    }
}
