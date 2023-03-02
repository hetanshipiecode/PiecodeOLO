using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishoutOLO.ViewModel
{
    public class AddItemModel
    {
        public int Id { get; set; }

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

        public int? CategoryId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string ItemImage { get; set; }
        public IFormFile File { get; set; }
        public bool IsCombo { get; set; }

        public class UpdateItmeModel
        {
            public int Id { get; set; }

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
