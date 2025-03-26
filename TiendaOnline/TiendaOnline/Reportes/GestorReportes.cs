using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Inventario;
using TiendaOnline.Ventas;
using TiendaOnline.Pagos;

namespace TiendaOnline.Reportes
{
    class GestorReportes
    {
        private GestorInventario gestorInventario;
        private GestorVentas gestorVentas;
        private GestorPagos gestorPagos;

        public GestorReportes(GestorInventario inventario, GestorVentas ventas, GestorPagos pagos)
        {
            gestorInventario = inventario;
            gestorVentas = ventas;
            gestorPagos = pagos;
        }

        public void ReporteInventario() 
        {
            Console.WriteLine("--- Reporte de Inventario ---");
            Producto[] productos = gestorInventario.ListarProductos();
            if(productos.Length == 0)
            {
                Console.WriteLine("El Inventario esta vacio.");
                return;
            }
            for (int i = 0; i < productos.Length; i++)
            {
                Producto producto = productos[i];
                string estadoStock;

                if (producto.Stock == 0)
                {
                    estadoStock = "AGOTADO";
                }
                else if (producto.Stock < 10)
                {
                    estadoStock = "Stock Bajo";
                }
                else
                {
                    estadoStock = "Disponible";
                }

                Console.WriteLine($"- {producto.Nombre}: {producto.Stock} unidades ({estadoStock})");
            }
        }

        public void ReporteVentas()
        {
            Console.WriteLine("--- Reporte de Ventas ---");
            float totalVentas = gestorVentas.VentasTotales();
            Console.WriteLine($"Total de Ventas: ${totalVentas}");
            gestorVentas.ListarVentas();
        }

        public void ReporteFacturacion()
        {
            Console.WriteLine("--- Reporte de Facturacion ---");
            float totalFacturado = gestorPagos.TotalFacturado();
            Console.WriteLine($"Total Facturado: ${totalFacturado}");
            gestorPagos.MostrarFacturas();
        }

        public void ReporteCompleto()
        {
            Console.WriteLine("--- Reporte Completo de la Tienda ---");
            ReporteInventario();
            ReporteVentas();
            ReporteFacturacion();
        }
    }
}
