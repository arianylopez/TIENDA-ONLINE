using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.GestionUsuario;

namespace TiendaOnline.Ventas
{
    class Venta
    {
        public int Id;
        public Cliente Cliente;
        public ProductoCarrito[] Productos;
        public float Total;
        public DateTime Fecha;

        public Venta(int id, Cliente cliente, ProductoCarrito[] productos, float total, DateTime fecha)
        {
            Id = id;
            Cliente = cliente;
            Productos = productos;
            Total = total;
            Fecha = fecha;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"ID Venta: {Id}");
            Console.WriteLine($"Cliente: {Cliente.Nombre}");
            Console.WriteLine("Productos:");
            for (int i = 0; i < Productos.Length; i++)
            {
                Console.WriteLine($"- {Productos[i].Producto.Nombre} x{Productos[i].Cantidad} (${Productos[i].SubTotal})");
            }
            Console.WriteLine($"Total: ${Total}");
            Console.WriteLine($"Fecha: {Fecha}");
        }
    }
}
