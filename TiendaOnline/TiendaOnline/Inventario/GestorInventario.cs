using System;
namespace TiendaOnline.Inventario
{
    class GestorInventario
    {
        private Producto[] productos;
        private Categoria[] categorias;
        private int totalProductos;
        private int totalCategorias;


        public GestorInventario(int capacidad)
        {
            productos = new Producto[capacidad];
            categorias = new Categoria[capacidad];
            totalProductos = 0;
            totalCategorias = 0;
        }

        public Categoria BuscarCategoria(int id)
        {
            for (int i = 0; i < totalCategorias; i++)
            {
                if (categorias[i].Id == id)
                {
                    return categorias[i];
                }
            }
            return null;
        }

        public bool CrearCategoria(int id, string nombre)
        {
            if(BuscarCategoria(id) != null)
            {
                Console.WriteLine("Ya existe una categoria con este ID.");
                return false;
            }

            if(totalCategorias < categorias.Length)
            {
                Categoria nuevaCategoria = new Categoria(id, nombre);
                categorias[totalCategorias] = nuevaCategoria;
                totalCategorias++;
                return true;
            }
            return false;
        }

        public bool EditarCategoria(int id, string nuevoNombre)
        {
            for(int i = 0; i < totalCategorias; i++)
            {
                if (categorias[i].Id == id)
                {
                    categorias[i].Nombre = nuevoNombre;
                    return true;
                }
            }
            Console.WriteLine("Categoria no encontrada o no existe aun!.");
            return false;
        }

        public bool EliminarCategoria(int id)
        {
            for(int i = 0; i < totalCategorias; i++)
            {
                if (categorias[i].Id == id)
                {
                    for(int j = i; j < totalCategorias - 1; j++)
                    {
                        categorias[j] = categorias[j + 1];
                    }
                    totalCategorias--;
                    return true;
                }
            }
            Console.WriteLine("No pudimos eliminar esta categoria porque no existe.");
            return false;
        }

        public void ListarCategoria()
        {
            Console.WriteLine("--- Lista de Categorias ---");
            for(int i = 0; i < totalCategorias; i++)
            {
                categorias[i].MostrarDetalles();
            }
        }

        public Producto BuscarProducto(int codigo)
        {
            for(int i = 0; i < totalProductos; i++)
            {
                if (productos[i].Codigo == codigo)
                {
                    return productos[i];
                }
            }
            return null;
        }

        public bool CrearProducto(int codigo, string nombre, float precio, int stock, int idCategoria)
        {
            if(BuscarProducto(codigo) != null)
            {
                Console.WriteLine("Ya existe un producto con este codigo.");
                return false;
            }
            Categoria categoria = BuscarCategoria(idCategoria);
            if(categoria == null)
            {
                Console.WriteLine("Categoria no encontrada!");
                return false;
            }

            if(totalProductos < productos.Length)
            {
                Producto nuevoProducto = new Producto(codigo, nombre, precio, stock, categoria);
                productos[totalProductos] = nuevoProducto;
                totalProductos++;
                return true;
            }
            return false;
        }

        public bool EditarProducto(int codigo, string nuevoNombre, float nuevoPrecio, int nuevoStock, int nuevaCategoria)
        {
            Producto producto = BuscarProducto(codigo);
            Categoria categoria = BuscarCategoria(nuevaCategoria);

            if(producto == null)
            {
                Console.WriteLine("El producto no fue encontrado.");
                return false;
            }
            if(categoria == null)
            {
                Console.WriteLine("La categoria no fue encontrada.");
                return false;
            }

            producto.Nombre = nuevoNombre;
            producto.Precio = nuevoPrecio;
            producto.Stock = nuevoStock;
            producto.Categoria = categoria;

            return true;
        }

        public bool EliminarProducto(int codigo)
        {
            for(int i = 0; i < totalProductos; i++)
            {
                if (productos[i].Codigo == codigo)
                {
                    for(int j = i; j < totalProductos - 1; j++)
                    {
                        productos[j] = productos[j + 1];
                    }

                    totalProductos--;
                    return true;
                }
            }
            Console.WriteLine("El producto no fue encontrado.");
            return false;
        }

        public Producto[] ListarProductos()
        {
            Console.WriteLine("--- Lista de Productos ---");
            Producto[] listaProductos = new Producto[totalProductos];
            for (int i = 0; i < totalProductos; i++)
            {
                listaProductos[i] = productos[i];
                // Add detailed product information printing
                Console.WriteLine($"Código: {productos[i].Codigo}");
                Console.WriteLine($"Nombre: {productos[i].Nombre}");
                Console.WriteLine($"Precio: ${productos[i].Precio}");
                Console.WriteLine($"Stock: {productos[i].Stock}");
                Console.WriteLine($"Categoría: {productos[i].Categoria.Nombre}");
                Console.WriteLine("------------------------");
            }
            return listaProductos;
        }

        public bool ActualizarStock(int codigoProducto, int cantidad1)
        {
            Producto producto = BuscarProducto(codigoProducto);
            if(producto != null)
            {
                producto.Stock += cantidad1;
                return true;
            }
            Console.WriteLine("El producto no fue encontrado.");
            return false;
        }
    }
}
