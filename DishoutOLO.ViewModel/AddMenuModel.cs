
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DishoutOLO.ViewModel
{



    public class AddMenuModel
    {

        public int Id { get; set; }

        [DisplayName("Menu Name")]
        public string MenuName { get; set; }

        [DisplayName("Menu Price")]
        public int MenuPrice { get; set; }
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }
        [DisplayName("CategorY")]

        public IFormFile File { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }

    }

    public class ListMenuModel
    {
        public int Id { get; set; }

        public string MenuName { get; set; }
        
        public string CategoryName { get; set; }

        public IFormFile File { get; set; }

        public int MenuPrice { get; set; }

        public int CategoryId { get; set; }
        [DisplayName("CategorY")]
        public string Image { get; set; }

        public bool IsActive { get; set; }
    }

    public class UpdateMenuModel
    {
        public int Id { get; set; }

        public string MenuName { get; set; }
        public IFormFile File { get; set; }

        public int MenuPrice { get; set; }

        public int CategoryId { get; set; }

        public string Image { get; set; }

    }

    public class DeleteMenuModel
    {
        public int Id { get; set; }
    }

}
