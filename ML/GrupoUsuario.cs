using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class GrupoUsuario
    {
        public int IdGrupoUsuario { get; set; }
        public ML.Grupo Grupo { get; set; }
        public ML.Usuario Usuario { get; set; }
        public List<object> Grupos { get; set; }
    }
}
