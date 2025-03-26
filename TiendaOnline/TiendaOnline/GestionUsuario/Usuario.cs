using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.GestionUsuario
{
    class Usuario
    {
        public int Id;
        public string Nombre;
        public string Correo;
        public string Contraseña;

        public Usuario (int id, string nombre, string correo, string contraseña)
        {
            Id = id;
            Nombre = nombre;
            Correo = correo;
            Contraseña = contraseña;
        }
    }
}
