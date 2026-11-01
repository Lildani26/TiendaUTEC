using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TiendaUTEC.Datos;
using TiendaUTEC.Models;
namespace TiendaUTEC.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductoRepositorio _repositorio;
        public ProductosController(ProductoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public IActionResult Index()
        {
            return View(_repositorio.ObtenerTodos());
        }

        public IActionResult Detalle(int id)
        {
            var producto = _repositorio.ObtenerPorId(id);
            if (producto == null) return NotFound();

            return View(producto);
        }

        public IActionResult Crear()
        {
            CargarCategorias();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                CargarCategorias();
                return View(producto);
            }

            _repositorio.Insertar(producto);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var producto = _repositorio.ObtenerPorId(id);
            if (producto == null) return NotFound();

            CargarCategorias();
            return View(producto);
        }

        [HttpPost]
        public IActionResult Editar(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                CargarCategorias();
                return View(producto);
            }

            _repositorio.Actualizar(producto);
            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            _repositorio.Eliminar(id);
            return RedirectToAction("Index");
        }
        private void CargarCategorias()
        {
            ViewBag.Categorias = new SelectList(
            _repositorio.ObtenerCategorias(), "Id", "Nombre");
        }
    }
}

