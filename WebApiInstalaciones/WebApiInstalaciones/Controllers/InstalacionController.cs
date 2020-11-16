using Entidades;
using Negocio;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApiInstalaciones.Controllers
{

    [RoutePrefix("api/Applus")]
    public class InstalacionController : ApiController
    {

        private static string path = ConfigurationManager.AppSettings["uploadFile"];

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult GetLogin(Query q)
        {
            Usuario u = NegocioDao.GetOne(q);

            if (u != null)
            {
                if (u.mensaje == "Pass")
                    return BadRequest("Contraseña Incorrecta");
                else
                    return Ok(u);
            }
            else return BadRequest("Usuario no existe");

        }

        [HttpGet]
        [Route("Encriptar")]
        public IHttpActionResult GetEncriptar(string nombre, bool activo)
        {
            string login = NegocioDao.EncriptarClave(nombre, activo);
            return Ok(login);
        }

        [HttpGet]
        [Route("Sync")]
        public IHttpActionResult GetSincronizar(int id, string version)
        {
            try
            {
                Sync s = NegocioDao.GetSincronizar(id, version);
                if (s != null)
                    return Ok(s);
                else
                    return BadRequest("Actualizar Versión");

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("SaveTrabajo")]
        public IHttpActionResult SaveRegistro()
        {
            try
            {
                //string path = HttpContext.Current.Server.MapPath("~/Imagen/");
                var files = HttpContext.Current.Request.Files;
                var testValue = HttpContext.Current.Request.Form["data"];
                OtCabecera r = JsonConvert.DeserializeObject<OtCabecera>(testValue);
                Mensaje m = NegocioDao.SaveTrabajo(r);
                if (m != null)
                {

                    for (int i = 0; i < files.Count; i++)
                    {
                        string fileName = Path.GetFileName(files[i].FileName);
                        files[i].SaveAs(path + fileName);
                    }

                    return Ok(m);
                }
                else
                    return BadRequest("Error");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("UpdateOt")]
        public IHttpActionResult UpdateOt(Ot o)
        {
            Mensaje m = NegocioDao.UpdateOt(o);
            if (m != null)
                return Ok(m);
            else
                return BadRequest("Error");
        }



        [HttpPost]
        [Route("SaveParteDiario")]
        public IHttpActionResult SaveParteDiario(ParteDiario p)
        {
            Mensaje m = NegocioDao.SaveParteDiario(p);
            if (m != null)
                return Ok(m);
            else
                return BadRequest("Error");
        }

        [HttpPost]
        [Route("SaveGps")]
        public IHttpActionResult SaveOperarioGps(EstadoOperario o)
        {
            Mensaje m = NegocioDao.SaveOperarioGps(o);
            if (m != null)
                return Ok(m);
            else
                return BadRequest("Error");
        }

        [HttpPost]
        [Route("SaveMovil")]
        public IHttpActionResult SaveEstadoMovil(EstadoMovil e)
        {
            Mensaje m = NegocioDao.SaveEstadoMovil(e);
            if (m != null)
                return Ok(m);
            else
                return BadRequest("Error");
        }

        [HttpPost]
        [Route("UpdateEstadoOt")]
        public IHttpActionResult UpdateEstadoOt(int id, int estado)
        {
            Mensaje m = NegocioDao.UpdateEstadoOt(id, estado);
            if (m != null)
                return Ok(m);
            else
                return BadRequest("Error");
        }



        // NUEVO INSPECCIONES--------------------------------------------------- 

        [HttpPost]
        [Route("SaveInspeccionesPhoto")]
        public IHttpActionResult SaveInspeccionesPhoto()
        {
            try
            {                
                var files = HttpContext.Current.Request.Files;
                Task.Run(() =>
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        string fileName = Path.GetFileName(files[i].FileName);

                        files[i].SaveAs(path + fileName);
                    }
                });

                return Ok("Enviado");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [Route("SaveInspecciones")]
        public IHttpActionResult SaveInspecciones(InspeccionPoste p)
        {
            Mensaje m = NegocioDao.SaveInspeccion(p);
            if (m != null)
                return Ok(m);
            else
                return BadRequest("Error");
        }

    }
}
