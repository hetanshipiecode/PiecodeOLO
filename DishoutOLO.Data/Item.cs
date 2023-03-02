using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishoutOLO.Data
{
    public class Item:BaseEntity
    {

        //ForiegnKey
        public int? CategoryId { get; set; }
            [Required]
            public string ItemName { get; set; }
            [Required]
            public string ItemImage { get; set; }
            public bool IsCombo { get; set; }
    }
}
