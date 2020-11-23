using Entidad;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
   public class NPapeleta
    {
        public async Task<List<Papeleta>> Lista_Papeletas(string id_usuario)
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
            return (await firebase
              .Child("MisPapeletas").Child(id_usuario)
              .OnceAsync<Papeleta>()).Select(item => new Papeleta
              {
                  estado_deuda=item.Object.estado_deuda,
                  fecha=item.Object.fecha,
                  importe=item.Object.importe,
                  propietario=item.Object.propietario,
                  conductor=item.Object.conductor
                 
              }).ToList();
        }
    }
}
