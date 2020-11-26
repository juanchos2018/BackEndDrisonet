using Entidad;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
   public class NSubChat
    {
        public async Task<bool> SetMensaje(SubChat o)
        {
            try
            {
                var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

                   await firebase
                  .Child("SubChat2").Child(o.id_usuario).Child(o.id_empresa)
                  .PutAsync(new SubChat() {id_empresa=o.id_empresa, nombre_usuario=o.nombre_usuario,mensaje=o.mensaje,id_usuario=o.id_usuario,fecha=DateTime.Now.ToLongDateString(),image_usuario=o.image_usuario });


            }
            catch (Exception ex)
            {
                // ModelState.AddModelError(string.Empty, ex.Message);
            }
            return true;
        }
    }
}
