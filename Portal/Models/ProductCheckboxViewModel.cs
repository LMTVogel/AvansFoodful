using Core.Domain;

namespace Portal.Models
{
    public class ProductCheckboxViewModel
    {
        public bool IsChecked { get; set; }
        public Product Product { get; set; }
    }
}
