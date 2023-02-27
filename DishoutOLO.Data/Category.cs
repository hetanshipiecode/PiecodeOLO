
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DishoutOLO.Data
{
    [Table("Category")]
    public class Category:BaseEntity
    {
        [Key]

        public string CategoryName { get; set; }
       
    }
}
