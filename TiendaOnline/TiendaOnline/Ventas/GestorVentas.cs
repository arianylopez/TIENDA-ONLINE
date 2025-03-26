using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.GestionUsuario;

namespace TiendaOnline.Ventas
{
    class GestorVentas
    {
        private Venta[] ventas;
        private int totalVentas;
        private int siguienteId;

        public GestorVentas(int capacidad)
        {
            ventas = new Venta[capacidad];
            totalVentas = 0;
            siguienteId = 1;
        }

        public bool RegistrarVenta(Cliente cliente, Carrito carrito, DateTime fechaEmision)
        {
            if (carrito.ObtenerCantidadItems() == 0)
            {
                Console.WriteLine("El carrito esta vacio, no se puede registrar la venta.");
                return false;
            }

            if (totalVentas < ventas.Length)
            {
                Venta nuevaVenta = new Venta(
                    siguienteId++,
                    cliente,
                    carrito.ObtenerItems(),
                    carrito.CalcularTotal(),
                    fechaEmision);

                ventas[totalVentas] = nuevaVenta;
                totalVentas++;
                return true;
            }
            return false;
        }

        public void ListarVentas()
        {
            Console.WriteLine("--- Listado de Ventas ---");
            for (int i = 0; i < totalVentas; i++)
            {
                ventas[i].MostrarDetalles();
            }
        }

        public float VentasTotales()
        {
            float totalVentas = 0;
            for (int i = 0; i < this.totalVentas; i++)
            {
                totalVentas += ventas[i].Total;
            }
            return totalVentas;
        }

        public Venta ObtenerUltimaVenta()
        {
            if (totalVentas > 0)
            {
                return ventas[totalVentas - 1];
            }
            else
            {
                Console.WriteLine("No se ha registrado ninguna venta.");
                return null;
            }
        }
    }
}
