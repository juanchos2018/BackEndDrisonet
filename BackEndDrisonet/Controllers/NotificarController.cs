using Entidad;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BackEndDrisonet.Controllers
{
    public class NotificarController : Controller
    {
        NNotificacion one = new NNotificacion();
        public IActionResult Index()
        {
            return View();
        }

        [Route("Notificacion/Notificar")]
        public  async Task< IActionResult> Notificar([FromBody] Notificacion o)
        {
            await   one.SetNotificaion(o);
            EnviarNotificacion(o.token,o.nombre_empresa, o.mensaje,o.detalle);
           // EnviarNotificacion(o.token);
            return Ok();
        }
        public void EnviarNotificacion(string deviceId,string nombreempresa, string mensaje,string detalle)
        {
            string titulos = nombreempresa;
            string mensajes = mensaje +" "+detalle ;
            string response = "";
            try
            {
                string titulo = titulos;
              
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
                        body = mensajes,
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
