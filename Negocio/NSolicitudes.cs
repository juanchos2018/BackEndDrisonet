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
              .Child("MisPeticiones").Child(id_usuario)
              .OnceAsync<Solicitudes>()).Select(item => new Solicitudes
              {
                  nombre = item.Object.nombre,
                  apellido=item.Object.apellido,
                  token=item.Object.token,
                  estado=item.Object.estado,
                  dni=item.Object.dni,

                 
              }).ToList();
        }
    }
}
