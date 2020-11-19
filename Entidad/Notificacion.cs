using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad
{
   public class Notificacion
    {
       
        public string id_usuario { get; set; }
        public string nombre_empresa { get; set; }
        public string image_empresa { get; set; }
        public string token { get; set; }
        public string fecha { get; set; }
     
    
        public string  mensaje { get; set; }
        public string detalle { get; set; }
        public string  ruta_documento { get; set; }

    }
}
