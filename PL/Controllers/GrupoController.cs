using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class GrupoController : Controller
    {
        // GET: Grupo
        public ActionResult GetAll(int IdUsuario = 1)
        {
            ML.GrupoUsuario grupo = new ML.GrupoUsuario();
            ML.Result result = BL.Grupo.GetByIdUsuario(IdUsuario);
            if (result.Correct)
            {
                grupo.Grupos = result.Objects;
                grupo.Usuario = new ML.Usuario();
                grupo.Usuario.IdUsuario = IdUsuario;
                return View(grupo);
            }
            else
            {
                return View();
            }
        }
        //public ActionResult Add(ML.GrupoUsuario usuario)
        public ActionResult Add(int idUsuario)
        {
            //ML.Result resultUsuario = BL.Usuario.GetById(usuario.Usuario.IdUsuario);
            ML.Result resultUsuario = BL.Usuario.GetById(idUsuario);
            if (resultUsuario.Correct)
            {

                ML.Result resultUsuarios = BL.Grupo.GetAllUsuarios();
                if (resultUsuarios.Correct)
                {
                    //usuario.Usuario.Usuarios = resultUsuarios.Objects;
                    //usuario.Usuario = ((ML.Usuario)resultUsuario.Object);
                    ML.GrupoUsuario grupoUsuario = new ML.GrupoUsuario();
                    grupoUsuario.Usuario = new ML.Usuario();
                    grupoUsuario.Usuario = ((ML.Usuario)resultUsuario.Object);

                    grupoUsuario.Usuario.Usuarios = resultUsuarios.Objects;
                    //return View(usuario);
                    return View(grupoUsuario);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult Add(ML.GrupoUsuario grupoUsuario)
        {
            ML.Result resultGrupo = BL.Grupo.Add(grupoUsuario.Grupo.Nombre);
            if (resultGrupo.Correct)
            {
                grupoUsuario.Grupo.IdGrupo = ((int)resultGrupo.Object);

                foreach (string idUsuario in grupoUsuario.Usuario.Usuarios)
                {
                    ML.GrupoUsuario grupoUsuarioItem = new ML.GrupoUsuario();

                    grupoUsuarioItem.Usuario = new ML.Usuario();
                    grupoUsuarioItem.Usuario.IdUsuario = int.Parse(idUsuario);

                    ML.Result resul = BL.Grupo.AddUsuarios(grupoUsuarioItem.Usuario.IdUsuario, grupoUsuario.Grupo.IdGrupo);
                }
            }
            else
            {
                return PartialView("Modal");
            }
            return PartialView("Modal");
        }
        public ActionResult Chat(int IdUsuario, int IdGrupo)
        {

            ML.Mensaje mensaje = new ML.Mensaje();
            mensaje.Grupo = new ML.Grupo();
            mensaje.Usuario = new ML.Usuario();

            ML.Result result = BL.Grupo.GetMensajes(IdGrupo, IdUsuario);
            if (result.Correct)
            {
                mensaje.Mensajes = result.Objects;
                mensaje.Grupo.IdGrupo = IdGrupo;
                mensaje.Usuario.IdUsuario = IdUsuario;

                return View(mensaje);
            }
            return View();
        } 
        [HttpPost]
        //public ActionResult EnviarMensaje(string mensaje, int IdGrupo, int IdUsuario)
        public ActionResult EnviarMensaje(string texto, ML.Mensaje mensaje)
        {
            ML.Result result = BL.Grupo.EnviarMensajer(texto, mensaje.Grupo.IdGrupo, mensaje.Usuario.IdUsuario);
            if(result.Correct)
            {
                return View();
            }
            else
            {
                return View();
            }
        }
    }
}