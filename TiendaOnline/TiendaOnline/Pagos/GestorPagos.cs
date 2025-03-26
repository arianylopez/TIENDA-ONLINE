using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.GestionUsuario;
using TiendaOnline.Ventas;

namespace TiendaOnline.Pagos
{
    class GestorPagos
    {
        private Factura[] facturas;
        private int totalFacturas;
        private int siguienteFactura;

        public GestorPagos(int capacidad)
        {
            facturas = new Factura[capacidad];
            totalFacturas = 0;
            siguienteFactura = 1000;
        }

        public Factura GenerarFactura(Cliente cliente, Venta venta, TipoMetodoPago metodoPago, DateTime fechaEmision)
        {
            if(totalFacturas < facturas.Length)
            {
                MetodoPago metodo = new MetodoPago(metodoPago);
                Factura nuevaFactura = new Factura(
                    siguienteFactura++,
                    cliente,
                    venta,
                    metodo,
                    fechaEmision);
                facturas[totalFacturas] = nuevaFactura;
                totalFacturas++;

                return nuevaFactura;
            }
            return null;
        }

        public void MostrarFacturas()
        {
            Console.WriteLine("--- Listado de Facturas ---");
            for(int i = 0; i < totalFacturas; i++)
            {
                facturas[i].MostrarDetalles();
            }
        }

        public float TotalFacturado()
        {
            float totalFacturado = 0;
            for(int i = 0; i < totalFacturas; i++)
            {
                totalFacturado += facturas[i].Venta.Total;
            }
            return totalFacturado;
        }
    }
}
