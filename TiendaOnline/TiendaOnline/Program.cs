using System;
using TiendaOnline.GestionUsuario;
using TiendaOnline.Inventario;
using TiendaOnline.Ventas;
using TiendaOnline.Pagos;
using TiendaOnline.Reportes;

class Program
{
    private static GestorUsuarios? gestorUsuarios;
    private static GestorInventario? gestorInventario;
    private static GestorVentas? gestorVentas;
    private static GestorPagos? gestorPagos;
    private static GestorReportes? gestorReportes;
    private static Carrito? carritoActual;
    private static Cliente? clienteActual;
    private static Administrador? administradorActual;

    static void Main(string[] args)
    {
        gestorUsuarios = new GestorUsuarios(100);
        gestorInventario = new GestorInventario(100);
        gestorVentas = new GestorVentas(100);
        gestorPagos = new GestorPagos(100);
        gestorReportes = new GestorReportes(gestorInventario, gestorVentas, gestorPagos);

        gestorInventario.CrearCategoria(1, "Computadoras");
        gestorInventario.CrearCategoria(2, "Tablets");
        gestorInventario.CrearCategoria(3, "Celulares");

        gestorInventario.CrearProducto(101, "Lenovo", 500.00f, 50, 1);
        gestorInventario.CrearProducto(102, "HP", 1200.00f, 30, 1);
        gestorInventario.CrearProducto(201, "iPhone 15", 25.00f, 100, 2);
        gestorInventario.CrearProducto(202, "iPhone 13", 50.00f, 75, 2);
        gestorInventario.CrearProducto(301, "Samsung", 150.00f, 20, 3);
        gestorInventario.CrearProducto(302, "Apple", 80.00f, 40, 3);

        gestorUsuarios.RegistrarCliente(1001, "Juan Pérez", "juan@email.com", "123", "Av. Siempre Viva 123", "76306969");
        gestorUsuarios.RegistrarCliente(1002, "María Rodríguez", "maria@email.com", "456", "Calle Principal 456", "775500932");
        gestorUsuarios.RegistrarAdministrador(2001, "Admin Principal", "admin@tienda.com", "admin", "Administrador General");

        MostrarMenuPrincipal();
    }

    static void MostrarMenuPrincipal()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Tienda Online ---");
            Console.WriteLine("Ingresar como:");
            Console.WriteLine("1. Cliente");
            Console.WriteLine("2. Administrador");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MenuCliente();
                    break;
                case "2":
                    MenuAdministrador();
                    break;
                case "3":
                    Console.WriteLine("Gracias por usar la Tienda Online. ¡Hasta luego!");
                    return;
                default:
                    Console.WriteLine("Opción inválida. Presione Enter para continuar.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void MenuCliente()
    {
        Console.Clear();
        Console.WriteLine("--- Inicio de Sesión de Cliente ---");
        Console.Write("Ingrese su ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Ingrese su contraseña: ");
        string contraseña = Console.ReadLine();

        clienteActual = (Cliente)gestorUsuarios.IniciarSesionCliente(id, contraseña);

        if (clienteActual != null)
        {
            carritoActual = new Carrito(50);
            MenuOpcionesCliente();
        }
        else
        {
            Console.WriteLine("Inicio de sesión fallido. Presione Enter para continuar.");
            Console.ReadLine();
        }
    }

    static void MenuOpcionesCliente()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Bienvenido, {clienteActual.Nombre}");
            Console.WriteLine("--- Menú de Cliente ---");
            Console.WriteLine("1. Agregar Producto al Carrito");
            Console.WriteLine("2. Eliminar Producto del Carrito");
            Console.WriteLine("3. Ver Carrito");
            Console.WriteLine("4. Ir a Pagar");
            Console.WriteLine("5. Salir");
            Console.WriteLine("6. Cambiar de Cuenta");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AgregarProductoAlCarrito();
                    break;
                case "2":
                    EliminarProductoDelCarrito();
                    break;
                case "3":
                    MostrarCarrito();
                    break;
                case "4":
                    IrAPagar();
                    break;
                case "5":
                    return;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opción inválida. Presione Enter para continuar.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void AgregarProductoAlCarrito()
    {
        Console.Clear();
        Console.WriteLine("--- Agregar Producto al Carrito ---");
        gestorInventario.ListarProductos();

        Console.Write("Ingrese el código del producto: ");
        int codigo = int.Parse(Console.ReadLine());

        Producto producto = gestorInventario.BuscarProducto(codigo);
        if (producto == null)
        {
            Console.WriteLine("Producto no encontrado.");
            Console.ReadLine();
            return;
        }

        Console.Write("Ingrese la cantidad: ");
        int cantidad = int.Parse(Console.ReadLine());

        if (cantidad > producto.Stock)
        {
            Console.WriteLine("Cantidad solicitada supera el stock disponible.");
            Console.ReadLine();
            return;
        }

        if (carritoActual.AgregarItem(producto, cantidad))
        {
            Console.WriteLine("Producto agregado al carrito exitosamente.");
        }
        else
        {
            Console.WriteLine("No se pudo agregar el producto al carrito.");
        }
        Console.ReadLine();
    }

    static void EliminarProductoDelCarrito()
    {
        if (carritoActual.ObtenerCantidadItems() == 0)
        {
            Console.WriteLine("El carrito está vacío.");
            Console.ReadLine();
            return;
        }

        Console.Clear();
        Console.WriteLine("--- Eliminar Producto del Carrito ---");
        carritoActual.MostrarCarrito();

        Console.Write("Ingrese el código del producto a eliminar: ");
        int codigo = int.Parse(Console.ReadLine());

        if (carritoActual.EliminarItem(codigo))
        {
            Console.WriteLine("Producto eliminado del carrito.");
        }
        Console.ReadLine();
    }

    static void MostrarCarrito()
    {
        Console.Clear();
        carritoActual.MostrarCarrito();
        Console.WriteLine("Presione Enter para continuar.");
        Console.ReadLine();
    }

    static void IrAPagar()
    {
        if (carritoActual.ObtenerCantidadItems() == 0)
        {
            Console.WriteLine("El carrito está vacío. No se puede proceder al pago.");
            Console.ReadLine();
            return;
        }

        Console.Clear();
        Console.WriteLine("--- Pago ---");
        carritoActual.MostrarCarrito();

        Console.WriteLine("Seleccione un método de pago:");
        Console.WriteLine("1. Efectivo");
        Console.WriteLine("2. Tarjeta de Crédito");
        Console.WriteLine("3. Tarjeta de Débito");
        Console.WriteLine("4. QR");
        Console.Write("Opción: ");

        TipoMetodoPago metodoPago;
        switch (Console.ReadLine())
        {
            case "1": metodoPago = TipoMetodoPago.Efectivo; break;
            case "2": metodoPago = TipoMetodoPago.TarjetaCredito; break;
            case "3": metodoPago = TipoMetodoPago.TarjetaDebito; break;
            case "4": metodoPago = TipoMetodoPago.QR; break;
            default:
                Console.WriteLine("Método de pago inválido.");
                return;
        }

        if (gestorVentas.RegistrarVenta(clienteActual, carritoActual, DateTime.Now))
        {
            gestorPagos.GenerarFactura(clienteActual, gestorVentas.ObtenerUltimaVenta(), metodoPago, DateTime.Now);
            Console.WriteLine("Venta realizada con éxito.");
        }
        else
        {
            Console.WriteLine("Error al registrar la venta.");
        }

        Console.ReadLine();
        carritoActual = new Carrito(50);
    }

    static void MenuAdministrador()
    {
        Console.Clear();
        Console.WriteLine("--- Inicio de Sesión de Administrador ---");
        Console.Write("Ingrese su ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Ingrese su contraseña: ");
        string contraseña = Console.ReadLine();

        administradorActual = (Administrador)gestorUsuarios.IniciarSesionAdmin(id, contraseña);

        if (administradorActual != null)
        {
            MenuOpcionesAdministrador();
        }
        else
        {
            Console.WriteLine("Inicio de sesión fallido. Presione Enter para continuar.");
            Console.ReadLine();
        }
    }

    static void MenuOpcionesAdministrador()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Bienvenido, {administradorActual.Nombre}");
            Console.WriteLine("--- Menú de Administrador ---");
            Console.WriteLine("1. Gestionar Productos");
            Console.WriteLine("2. Gestionar Categorías");
            Console.WriteLine("3. Ver Inventario");
            Console.WriteLine("4. Ver Reportes");
            Console.WriteLine("5. Salir");
            Console.WriteLine("6. Cambiar de Cuenta");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    GestionarProductos();
                    break;
                case "2":
                    GestionarCategorias();
                    break;
                case "3":
                    VerInventario();
                    break;
                case "4":
                    VerReportes();
                    break;
                case "5":
                    return;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opción inválida. Presione Enter para continuar.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void GestionarProductos()
    {
        Console.Clear();
        Console.WriteLine("--- Gestión de Productos ---");
        Console.WriteLine("1. Agregar Producto");
        Console.WriteLine("2. Eliminar Producto");
        Console.WriteLine("3. Editar Producto");
        Console.Write("Seleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("Código del Producto: ");
                int codigo = int.Parse(Console.ReadLine());
                Console.Write("Nombre del Producto: ");
                string nombre = Console.ReadLine();
                Console.Write("Precio: ");
                float precio = float.Parse(Console.ReadLine());
                Console.Write("Stock: ");
                int stock = int.Parse(Console.ReadLine());
                Console.Write("ID de Categoría: ");
                int idCategoria = int.Parse(Console.ReadLine());

                if (gestorInventario.CrearProducto(codigo, nombre, precio, stock, idCategoria))
                {
                    Console.WriteLine("Producto agregado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error al agregar producto.");
                }
                break;
            case "2":
                Console.Write("Código del Producto a eliminar: ");
                int codigoEliminar = int.Parse(Console.ReadLine());
                if (gestorInventario.EliminarProducto(codigoEliminar))
                {
                    Console.WriteLine("Producto eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error al eliminar producto.");
                }
                break;
            case "3":
                Console.Write("Código del Producto a editar: ");
                int codigoEditar = int.Parse(Console.ReadLine());
                Console.Write("Nuevo Nombre: ");
                string nuevoNombre = Console.ReadLine();
                Console.Write("Nuevo Precio: ");
                float nuevoPrecio = float.Parse(Console.ReadLine());
                Console.Write("Nuevo Stock: ");
                int nuevoStock = int.Parse(Console.ReadLine());
                Console.Write("Nueva ID de Categoría: ");
                int nuevaCategoria = int.Parse(Console.ReadLine());

                if (gestorInventario.EditarProducto(codigoEditar, nuevoNombre, nuevoPrecio, nuevoStock, nuevaCategoria))
                {
                    Console.WriteLine("Producto editado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error al editar producto.");
                }
                break;
        }
        Console.ReadLine();
    }

    static void GestionarCategorias()
    {
        Console.Clear();
        Console.WriteLine("--- Gestión de Categorías ---");
        Console.WriteLine("1. Agregar Categoría");
        Console.WriteLine("2. Eliminar Categoría");
        Console.WriteLine("3. Editar Categoría");
        Console.Write("Seleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("ID de Categoría: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("Nombre de Categoría: ");
                string nombre = Console.ReadLine();

                if (gestorInventario.CrearCategoria(id, nombre))
                {
                    Console.WriteLine("Categoría agregada exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error al agregar categoría.");
                }
                break;
            case "2":
                Console.Write("ID de Categoría a eliminar: ");
                int idEliminar = int.Parse(Console.ReadLine());
                if (gestorInventario.EliminarCategoria(idEliminar))
                {
                    Console.WriteLine("Categoría eliminada exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error al eliminar categoría.");
                }
                break;
            case "3":
                Console.Write("ID de Categoría a editar: ");
                int idEditar = int.Parse(Console.ReadLine());
                Console.Write("Nuevo Nombre: ");
                string nuevoNombre = Console.ReadLine();

                if (gestorInventario.EditarCategoria(idEditar, nuevoNombre))
                {
                    Console.WriteLine("Categoría editada exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error al editar categoría.");
                }
                break;
        }
        Console.ReadLine();
    }

    static void VerInventario()
    {
        Console.Clear();
        gestorReportes.ReporteInventario();
        Console.WriteLine("Presione Enter para continuar.");
        Console.ReadLine();
    }

    static void VerReportes()
    {
        Console.Clear();
        Console.WriteLine("--- Menú de Reportes ---");
        Console.WriteLine("1. Reporte de Inventario");
        Console.WriteLine("2. Reporte de Ventas");
        Console.WriteLine("3. Reporte de Facturación");
        Console.WriteLine("4. Reporte Completo");
        Console.Write("Seleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                gestorReportes.ReporteInventario();
                break;
            case "2":
                gestorReportes.ReporteVentas();
                break;
            case "3":
                gestorReportes.ReporteFacturacion();
                break;
            case "4":
                gestorReportes.ReporteCompleto();
                break;
        }
        Console.WriteLine("Presione Enter para continuar.");
        Console.ReadLine();
    }
}