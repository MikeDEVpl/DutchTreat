using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string ProductCategory { get; set; } //Gdy jest poprzedzone słowem Product(nazwą entity) to bierze te wartości automatycznie
        public string ProductSize { get; set; } //Niie trzeba było dodawać mapy automappera
        public string ProductTitle { get; set; }
        public string ProductArtist { get; set; }
        public string ProductArtId { get; set; }
    }
}