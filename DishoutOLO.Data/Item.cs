
using System.ComponentModel.DataAnnotations;


namespace DishoutOLO.Data
{


    public class Item : BaseEntity
    {
        public bool IsVeg { get; set; }
        //ForiegnKey
        public int CategoryId { get; set; }

        public string ItemName { get; set; }
        public string? ItemDescription { get; set; }

        public string ItemImage { get; set; }
        public bool IsTax { get; set; }
        public string?   TaxName { get; set; }
        public int TaxPercentage { get; set; }
        public bool IsCombo { get; set; }
        public string?   ItemsAvailable { get; set; }
        public string? AdditionalChoices { get; set; }
    }

}