﻿using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio
{
    public class Conexion
    {
        public IFirebaseConfig conec()
        {
            IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
            {
                AuthSecret = "4OUpc4rGJTetaxG8IBiE3uoXdSVNuBeRdJoo8Uju",
                BasePath = "https://fir-app-cf755.firebaseio.com/"

            };
            return config;
        }

        public String Firekey()
        {
            string key = "AIzaSyASBVEzU8ZqFHtMgdYW7-66ZQjrZGf-lAc";

            return key;
        }

        public string AthEmail()
        {
            string AthEmail = "storage@gmail.com";
            return AthEmail;
        }
        public string AthPassword()
        {
            string AuthPassword = "2014049452";
            return AuthPassword;
        }
        public string Storge()
        {
            string store = "fir-app-cf755.appspot.com";
            return store;
        }
    }
}
