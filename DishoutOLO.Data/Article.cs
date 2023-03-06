using System.ComponentModel.DataAnnotations.Schema;

namespace DishoutOLO.Data
{
    [Table("Articles")]
    public class Article :BaseEntity
    {
        public string ArticleName { get; set; }
        public string ArticleDescription { get; set; }

    }
}
