using Microsoft.Data.SqlClient; 
using TiendaUTEC.Models;
namespace TiendaUTEC.Datos
{
    public class ProductoRepositorio
    {
        private readonly string _cadena;
        public ProductoRepositorio(IConfiguration configuracion)
        {
            _cadena = configuracion.GetConnectionString("TiendaUTEC");
        }

        //Controller para obtener todos los productos de la base de datos y devolverlos en una lista
        public List<Producto> ObtenerTodos()
        {
            List<Producto> productos = new List<Producto>();

            string sql = @"SELECT Id, Sku, Nombre, Descripcion, 
                                  Precio, Stock, Categoria 
                           FROM   vw_Catalogo 
                           ORDER BY Categoria, Nombre";

            using (var conexion = new SqlConnection(_cadena))
            using (var comando = new SqlCommand(sql, conexion))
            {
                conexion.Open();

                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        productos.Add(new Producto
                        {
                            Id = lector.GetInt32(0),
                            Sku = lector.GetString(1),
                            Nombre = lector.GetString(2),
                            Descripcion = lector.IsDBNull(3)
                                          ? "" : lector.GetString(3),
                            Precio = lector.GetDecimal(4),
                            Stock = lector.GetInt32(5),
                            NombreCategoria = lector.GetString(6)
                        });
                    }
                }
            }
            return productos;
        }


        //Controller para obtener un producto por su id de la base de datos y devolverlo
        public Producto ObtenerPorId(int id)
        {
            Producto producto = null;

            string sql = @"SELECT Id, Nombre, Precio, Stock, 
                                  CategoriaId, Descripcion, Activo 
                           FROM   Productos 
                           WHERE  Id = @Id";

            using (var conexion = new SqlConnection(_cadena))
            using (var comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);
                conexion.Open();

                using (var lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        producto = new Producto
                        {
                            Id = lector.GetInt32(0),
                            Nombre = lector.GetString(1),
                            Precio = lector.GetDecimal(2),
                            Stock = lector.GetInt32(3),
                            CategoriaId = lector.GetInt32(4),
                            Descripcion = lector.IsDBNull(5)
                                          ? "" : lector.GetString(5),
                            Activo = lector.GetBoolean(6)
                        };
                    }
                }
            }
            return producto;
        }

        // Controller para insertar un producto en la base de datos
        public void Insertar(Producto p)
        {
            string sql = @"INSERT INTO Productos 
                           (Nombre, Precio, Stock, CategoriaId, 
                            Descripcion, Activo) 
                           VALUES 
                           (@Nombre, @Precio, @Stock, @CategoriaId, 
                            @Descripcion, @Activo)";

            using (var conexion = new SqlConnection(_cadena))
            using (var comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@Nombre", p.Nombre);
                comando.Parameters.AddWithValue("@Precio", p.Precio);
                comando.Parameters.AddWithValue("@Stock", p.Stock);
                comando.Parameters.AddWithValue("@CategoriaId",
                                                p.CategoriaId);
                comando.Parameters.AddWithValue("@Descripcion",
                    (object)p.Descripcion ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Activo", p.Activo);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // Controller para actualizar un producto en la base de datos
        public void Actualizar(Producto p)
        {
            string sql = @"UPDATE Productos 
                           SET    Nombre      = @Nombre, 
                                  Precio      = @Precio, 
                                  Stock       = @Stock, 
                                  CategoriaId = @CategoriaId, 
                                  Descripcion = @Descripcion, 
                                  Activo      = @Activo 
                           WHERE  Id = @Id";

            using (var conexion = new SqlConnection(_cadena))
            using (var comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@Id", p.Id);
                comando.Parameters.AddWithValue("@Nombre", p.Nombre);
                comando.Parameters.AddWithValue("@Precio", p.Precio);
                comando.Parameters.AddWithValue("@Stock", p.Stock);
                comando.Parameters.AddWithValue("@CategoriaId",
                                                p.CategoriaId);
                comando.Parameters.AddWithValue("@Descripcion",
                    (object)p.Descripcion ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Activo", p.Activo);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // Controller 1.0 para eliminar un producto de la base de datos.
        //Este método no elimina físicamente el registro. Sino solo lo marca como inactivo. (Borrado Logico)
        //Esto se hace para mantener la integridad de los datos y evitar problemas con las relaciones entre tablas.
        public void Eliminar(int id)
        {
            string sql = "UPDATE Productos SET Activo = 0 " +
            "WHERE Id = @Id";
            using (var conexion = new SqlConnection(_cadena))
            using (var comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // Controller para obtener todas las categorías activas de la base de datos y devolverlas en una lista
        public List<Categoria> ObtenerCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            string sql = @"SELECT Id, Nombre 
                           FROM   Categorias 
                           WHERE  Activa = 1 
                           ORDER BY Nombre";

            using (var conexion = new SqlConnection(_cadena))
            using (var comando = new SqlCommand(sql, conexion))
            {
                conexion.Open();
                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        lista.Add(new Categoria
                        {
                            Id = lector.GetInt32(0),
                            Nombre = lector.GetString(1)
                        });
                    }
                }
            }
            return lista;
        }
    }
}
