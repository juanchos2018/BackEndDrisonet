using Entidad;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
   public class NSolicitudes
    {
        public async Task<List<Solicitudes>> Lista_Solicitudes(string id_usuario)
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
            return (await firebase
              .Child("Solicitudes").Child(id_usuario)
              .OnceAsync<Solicitudes>()).Select(item => new Solicitudes
              {
                  nombre_usuario = item.Object.nombre_usuario,              
                  token=item.Object.token,
                  estado=item.Object.estado,
                  dni_usuario=item.Object.dni_usuario,
                  img_usuario=item.Object.img_usuario,
                  id_usuario=item.Object.id_usuario
              }).ToList();
        }


    }
}
