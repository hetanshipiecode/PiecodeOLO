

using System.ComponentModel;

namespace DishoutOLO.ViewModel
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

    }

    public class AddCategoryModel
    {
        public int Id { get; set; }

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        public bool IsActive { get; set; }
    }
    public class ListCategoryModel
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }



    }



}
