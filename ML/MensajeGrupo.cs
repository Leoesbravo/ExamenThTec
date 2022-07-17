using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class MensajeGrupo
    {
        public int IdMensajeGrupo { get; set; }
        public ML.Grupo Grupo { get; set; }
        public ML.Usuario Usuario { get; set; }
        public string Mensaje { get; set; }
    }
}
