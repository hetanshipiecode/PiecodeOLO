using DishoutOLO.Data;
using DishoutOLO.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace DishoutOLO.Repo
{
    public class Repository<T> : IRepository<T> where T  : BaseEntity
    {
        private readonly DishoutOLOContext context;
        private DbSet<T> entities;

        public Repository(DishoutOLOContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IList<T> GetAll()

        {
            return entities.ToList();
        }


        public IQueryable<T> GetAllAsQuerable()
        {
            return entities.AsQueryable();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            var local = context.Set<T>()
               .Local
               .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }

            context.Set<T>().Attach(entity);
           context.Entry(entity).State = EntityState.Modified;
           context.SaveChanges();

        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public T GetByPredicate(Func<T, bool> predicate)
        {
            return entities.FirstOrDefault(predicate);
        }
        public IList<T> GetListByPredicate(Func<T, bool> predicate)
        {
            return entities.Where(predicate).ToList();
        }

    }
}
