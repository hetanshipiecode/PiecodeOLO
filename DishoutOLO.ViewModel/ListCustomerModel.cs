using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishoutOLO.ViewModel
{
    public class ListCustomerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public bool IsActive { get; set; }  
        public bool Status { get; set; }
    }
}
