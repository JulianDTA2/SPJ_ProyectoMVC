using System.ComponentModel.DataAnnotations;

namespace SPJ_ProyectoMVC.Models
{
    public class Catalogo
    {
        public int CatalogoId { get; set; }

        [Required]
        public string? Marca { get; set; }
        
        public string? Modelo { get; set; }
        
        public bool Usado { get; set; }
        [Range(5000.00, 50000.00)]
        public decimal Precio { get; set; }
        public decimal IVA { get; set; }
        

    }
}
