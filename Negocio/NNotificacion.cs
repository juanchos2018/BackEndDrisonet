using Entidad;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
   public class NNotificacion
    {
        public async Task<bool> SetNotificaion(Notificacion o)
        {
            try
            {                
                var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

                var Key = Firebase.Database.FirebaseKeyGenerator.Next();
                //o.estado_producto = "NoPublico";
                await firebase
                  .Child("MisNotificaciones").Child(o.id_usuario).Child(Key)
                  .PutAsync(new Notificacion() { nombre_empresa = o.nombre_empresa, image_empresa=o.image_empresa,fecha = DateTime.Now.ToShortDateString(), mensaje = o.mensaje, detalle = o.detalle, ruta_documento = o.ruta_documento });


            }
            catch (Exception ex)
            {
                // ModelState.AddModelError(string.Empty, ex.Message);
            }
            return true;
        }
    }
}
