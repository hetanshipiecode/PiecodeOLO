using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DishoutOLO.Repo.Interface
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        IQueryable<T> GetAllAsQuerable();
        public IList<T> GetListByPredicate(Func<T, bool> predicate);

        T GetByPredicate(Func<T, bool> predicate);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}
    