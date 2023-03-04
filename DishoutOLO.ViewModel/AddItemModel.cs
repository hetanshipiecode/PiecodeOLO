using Microsoft.AspNetCore.Http;

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


    }
    public class ListItmeModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public int? CategoryId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string ItemImage { get; set; }
        //public IFormFile File { get; set; }
        public bool IsCombo { get; set; }
        public bool IsActive { get; set; }

        public class UpdateItmeModel
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

        public class DeleteItmeModel
        {

            public int Id { get; set; }

        }
    }
}
