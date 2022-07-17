using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Grupo
    {
        public static ML.Result GetByIdUsuario(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {

                    using (SqlCommand cmd = new SqlCommand("GrupoGetByIdUsuario"))
                    {
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[0].Value = IdUsuario;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        DataTable tableGrupo = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tableGrupo);

                        if (tableGrupo.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableGrupo.Rows)
                            {
                                ML.GrupoUsuario grupo = new ML.GrupoUsuario();
                                grupo.IdGrupoUsuario = int.Parse(row[0].ToString());

                                grupo.Grupo = new ML.Grupo();
                                grupo.Grupo.IdGrupo = int.Parse(row[1].ToString());

                                grupo.Usuario = new ML.Usuario();
                                grupo.Usuario.IdUsuario = int.Parse(row[2].ToString());

                                grupo.Grupo.Nombre = row[3].ToString();

                                result.Objects.Add(grupo);
                            }

                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetAllUsuarios()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {

                    using (SqlCommand cmd = new SqlCommand("UsuarioGetAll"))
                    {
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable tableUsuario = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.IdUsuario = int.Parse(row[0].ToString());

                                usuario.NombreCompleto = row[1].ToString();
                                usuario.Email = row[2].ToString();
                                usuario.UserName = row[3].ToString();
                                if (row[4].ToString() == "")
                                {
                                    usuario.Imagen = new byte[0];
                                }
                                else
                                {

                                    usuario.Imagen = (byte[])row[4];
                                }
                                usuario.Estado = new ML.Estado();
                                usuario.Estado.IdEstado = int.Parse(row[5].ToString());
                                usuario.Estatus = new ML.Estatus();
                                usuario.Estatus.IdEstatus = int.Parse(row[6].ToString());

                                usuario.Estado.Nombre = row[7].ToString();
                                usuario.Estatus.Nombre = row[8].ToString();


                                result.Objects.Add(usuario);
                            }

                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result Add(string nombre)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "GrupoAdd"; //Nombre SP
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = nombre;
                        collection[1] = new SqlParameter("IdGrupo", SqlDbType.Int);
                        collection[1].Direction = ParameterDirection.Output;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();
                        int RowsAffected = cmd.ExecuteNonQuery();

                        result.Object = Convert.ToInt32(cmd.Parameters["IdGrupo"].Value);

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }

                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
        public static ML.Result AddUsuarios(int idUsuario, int idGrupo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "GrupoAddUsuarios"; 
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[0].Value = idUsuario;
                        collection[1] = new SqlParameter("IdGrupo", SqlDbType.Int);
                        collection[1].Value = idGrupo;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }

                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
        public static ML.Result GetMensajes(int IdGrupo, int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {

                    using (SqlCommand cmd = new SqlCommand("GrupoGetMensajes"))
                    {
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdGrupo", SqlDbType.Int);
                        collection[0].Value = IdGrupo;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        DataTable tableGrupo = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tableGrupo);

                        if (tableGrupo.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableGrupo.Rows)
                            {
                                //ML.GrupoUsuario grupoUsuario = new ML.GrupoUsuario();
                                //grupoUsuario.IdGrupoUsuario = int.Parse(row[0].ToString());

                                //grupoUsuario.Grupo = new ML.Grupo();
                                //grupoUsuario.Grupo.IdGrupo = int.Parse(row[1].ToString());

                                //grupoUsuario.Grupo.Mensaje = new ML.Mensaje();
                                //grupoUsuario.Grupo.Mensaje.IdMensaje = int.Parse(row[2].ToString());
                                //grupoUsuario.Grupo.Mensaje.Texto = row[3].ToString();

                                ML.Mensaje mensaje = new ML.Mensaje();
                                mensaje.GrupoUsuario = new ML.GrupoUsuario();

                                mensaje.GrupoUsuario.IdGrupoUsuario = int.Parse(row[0].ToString());

                                mensaje.GrupoUsuario.Grupo = new ML.Grupo();
                                mensaje.GrupoUsuario.Grupo.IdGrupo = int.Parse(row[1].ToString());
                                mensaje.IdMensaje = int.Parse(row[2].ToString());
                                mensaje.Texto = row[3].ToString();
                                mensaje.Usuario = new ML.Usuario();
                                mensaje.Usuario.IdUsuario = int.Parse(row[4].ToString());
                                mensaje.Grupo = new ML.Grupo();
                                mensaje.Grupo.Nombre = row[5].ToString();
                                mensaje.IdUsuario = IdUsuario;

                                result.Objects.Add(mensaje);
                            }

                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result EnviarMensajer(string mensaje, int IdGrupo, int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "EnviarMensajeGrupo";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[3];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[0].Value = IdUsuario;
                        collection[1] = new SqlParameter("IdGrupo", SqlDbType.Int);
                        collection[1].Value = IdGrupo;
                        collection[2] = new SqlParameter("Texto", SqlDbType.VarChar);
                        collection[2].Value = mensaje;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }

                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
    }
}
