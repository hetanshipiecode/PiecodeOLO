using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DishoutOLO.Data
{
    [Table("Menus")]    
    public class Menu:BaseEntity
    {
        public string? MenuName { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int MenuPrice { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }    
    }
}
