using System.ComponentModel;

namespace DishoutOLO.ViewModel
{
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


        public class UpdateCategoryModel
        {
            public int Id { get; set; }

            public string CategoryName { get; set; }

            public bool IsActive { get; set; }

        }

        public class DeleteCategoryModel
        {
            public int Id { get; set; }
        }
    }
}

