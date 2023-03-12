using System.ComponentModel;
namespace DishoutOLO.ViewModel
{
    public class AddArticleModel
    {
        public int Id { get; set; }

        [DisplayName("Article Name")]
        public string ArticleName { get; set; }
        [DisplayName("Article Description")]
        public string ArticleDescription { get; set; }

        public bool IsActive { get; set; }
    }
    public class ListArticleModel
    {
        public int Id { get; set; }

        public string ArticleName { get; set; }
        public string ArticleDescription { get; set; }

        public bool IsActive { get; set; }


        public class UpdateArticleModel
        {
            public int Id { get; set; }

            public string ArticleName { get; set; }
            public string ArticleDescription { get; set; }

            public bool IsActive { get; set; }

        }

        public class DeleteArticleModel
        {
            public int Id { get; set; }
        }
    }
}
