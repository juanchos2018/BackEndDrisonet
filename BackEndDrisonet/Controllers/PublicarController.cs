using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Negocio;
using Newtonsoft.Json;
namespace BackEndDrisonet.Controllers
{
    public class PublicarController : Controller
    {

        private readonly IHostingEnvironment _env;

        NPublicacion publicar = new NPublicacion();
        public PublicarController(IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Route("Publicar/SubirPublicacion")]
        [HttpPost("[action]")]
        public IActionResult SubirArchivos(IFormCollection UploadedFiles)
        {
            string filePath = "";
            Publicacion publica = new Publicacion();
            publica.nombre_usuario = UploadedFiles["nombre_usuario"].ToString();
            publica.descripcion_noticia = UploadedFiles["descripcion_noticia"].ToString();
            publica.img_usuario = UploadedFiles["image_empresa"].ToString();
            publica.key_usuario = UploadedFiles["key_usuario"].ToString();
            long size = UploadedFiles.Files.Sum(f => f.Length);
            string ruta = Path.Combine(_env.ContentRootPath, "/Content/Images/");          
            string fileName = "";

            if (size==0)
            {

            }
            else
            {
                foreach (var formFile in UploadedFiles.Files)
                {
                    if (formFile.Length > 0)
                    {
                        fileName = formFile.FileName;
                        filePath = Path.Combine(ruta, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyTo(fileStream);
                        }
                    }
                }
            }
            if (size==0)
            {                
                Task task = Task.Run(() => publicar.Upload(null, fileName, publica));
                return Ok(new { archivos = UploadedFiles.Files.Count, size, ruta });
            }
            else
            {
                System.IO.FileStream stream1;
                stream1 = new FileStream(Path.Combine(filePath), FileMode.Open);
                Task task = Task.Run(() => publicar.Upload(stream1, fileName, publica));
                return Ok(new { archivos = UploadedFiles.Files.Count, size, ruta });
            }          
           
        }

        [Route("Publicar/GetPublicaciones")]
        public async Task<IActionResult> Get_Publicaciones([FromBody] Usuario o)
        {   
            var ListaPublicaciones = await publicar.Lista_Publicacion();
            var lista = ListaPublicaciones.Where(x => x.key_usuario == o.key_usuario).ToList();
            return Json(lista);
        }

        [Route("Publicar/Set_Notificar")]
        public  IActionResult Set_Notificar()
        {
            EnviarNotificacion("token");
            return Ok();
        }
        public void EnviarNotificacion(string deviceId)
        {
            string titulos = "Enviar paquete";
            deviceId = "c2Oe5XpHRJ6isxwmhARfgB:APA91bE3BBgJcupCGvCrKNd7FLwht316_SlJ1t_kEff_ss00a3UkHluHc33M15UXkdE-Ow9z5WRd2IRGgYNcyoPalN43EH1LP78b048XUgA6pGgZIOIv7WM9mXFz9jInbbD0FLPJKBP2";
            string mensajes = "tienes una nueva entrga que hacer";
            string response = "";
            try
            {
                string titulo = titulos;
                string mensaje = mensajes;
                string applicationID = "AAAAFOcMi-k:APA91bFtOu1xbHUqAtmstTQlI-VRi6Nkx-s2vBFtxIgrXsP2TXKP8S0EqVWub70qL73BWNDtypCkPhv2NP_2CzGTxMjV93q9tiICF3xJzztKc8n5Dq39VUhzF_HctlPc1-E5IVjQj3JU";
                string senderId = "89775705065";

                //token de usuario
                //   string deviceId = "dPy0QisHT_Ksm3snK-GonO:APA91bG8j2n4rJvB8INUoaYhRnb8vSXrmRRS7p6rgmP7KZ9PVORB1Cx0fusVzWtu2_J7g0wZnCIif1Qsx9bqwOTu_Cytu2gMI1vDyWuLfxlFDBtpKi87JeQQHfx3ERnw_dC9zSfTY41C";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = mensaje,
                        title = titulo,
                        sound = "Enabled"
                    }
                };
             
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                                response = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }

        }
    }
}
