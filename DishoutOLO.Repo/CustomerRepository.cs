using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishoutOLO.Repo
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DishoutOLOContext Context) : base(Context)
        {

        }
    }
}
