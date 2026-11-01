using System.ComponentModel.DataAnnotations;

namespace TiendaUTEC.Models

{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(60)]
        public string Nombre { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Precio invalido")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock invalido")]
        public int Stock { get; set; }
        [Range(1, int.MaxValue,
        ErrorMessage = "Seleccione una categoria")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        [StringLength(400)]
        public string Descripcion { get; set; }
       
        public string? Sku { get; set; }
        public string? NombreCategoria { get; set; }
        public bool Activo { get; set; } = true;
    }
}
