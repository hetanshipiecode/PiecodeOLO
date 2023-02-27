
using System.ComponentModel.DataAnnotations;

namespace DishoutOLO.Data
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }     
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
