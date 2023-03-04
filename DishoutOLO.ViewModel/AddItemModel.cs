using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace DishoutOLO.ViewModel
{
    public class AddItemModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public int? CategoryId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string ItemImage { get; set; }
        public IFormFile File { get; set; }
        public bool IsCombo { get; set; }
        public bool IsVeg { get; set; }
        public bool IsTax { get; set; }


    }
    public class ListItemModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }
        [DisplayName("Category Name")]

        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        //public IFormFile File { get; set; }
        public bool IsCombo { get; set; }
        public bool IsActive { get; set; }
        public bool IsVeg { get; set; }
        public bool IsTax { get; set; }

  

        public class UpdateItemModel
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }

            public int? CategoryId { get; set; }
            [Required]
            public string ItemName { get; set; }
            [Required]
            public string ItemImage { get; set; }
            public IFormFile File { get; set; }
            public bool IsCombo { get; set; }

        }

        public class DeleteItemModel
        {

            public int Id { get; set; }

        }
    }
}
