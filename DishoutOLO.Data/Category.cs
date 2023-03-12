
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DishoutOLO.Data
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
    }
}
