using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishoutOLO.Data
{
    public class MenuItem:BaseEntity
    {
        
            public bool IsVeg { get; set; }
            //ForiegnKey
            public int? CategoryId { get; set; }
            public bool IsTax { get; set; }
            public string TaxName { get; set; }
            public int TaxPercentage { get; set; }
            public bool IsCombo { get; set; }
            public string ItemsAvailable { get; set; }
            public string? AdditionalChoices { get; set; }
        }

}

