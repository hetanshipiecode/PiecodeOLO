
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DishoutOLO.Data
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        public string? Menuname { get; set; }

        public int? CategoryId { get; set; }

        public int? MenuPrice { get; set; }

        public string? Image { get; set; }
    }
}
