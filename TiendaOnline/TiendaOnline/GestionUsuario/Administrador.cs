using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.GestionUsuario
{
    class Administrador : Usuario
    {
        public string Cargo;

        public Administrador(int id, string nombre, string correo, string contraseña, string cargo) : base(id, nombre, correo, contraseña)
        {
            Id = id;
            Nombre = nombre;
            Correo = correo;
            Contraseña = contraseña;
            Cargo = cargo;
        }
    }
}
