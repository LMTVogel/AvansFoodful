using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

namespace Portal.Models
{
    public class CreatePackageViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Naam is niet ingevuld")]
        public string Name { get; set; }
        [Required]
        public int CanteenId { get; set; }
        [Required(ErrorMessage = "Ophaaltijd is niet ingevuld")]
        public DateTime MaxPickupTime { get; set; }
        [Required(ErrorMessage = "Prijs is niet ingevuld")]
        [Column(TypeName = "smallmoney")]
        public double Price { get; set; }
        public bool IsMealHot { get; set; }
        public List<ProductCheckboxViewModel> ProductCheckboxes { get; set; }
        [Required(ErrorMessage = "Producten zijn niet ingevuld")]
        public List<string> SelectedProductsList { get; set; }
        public bool ContainsAlcohol { get; set; }
        public Dictionary<string, string> Errors { get; set; }
    }
}
