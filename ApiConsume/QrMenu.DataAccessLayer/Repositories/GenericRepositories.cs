using EntityLayer.Concrete;
using HotelAndTours.DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QrMenu.DataAccessLayer.Abstract;

namespace QrMenu.DataAccessLayer.Repositories
{
    public class GenericRepositories<T> : IGenericDal<T> where T : class
    {
        //  private DbSet<T> _object;
        //private Context _context = new Context();
        private readonly Context _context;

        public GenericRepositories(Context context)
        {
            _context = context;
        }

        /*
        public GenericRepositories()
        {
            _object = _context.Set<T>();
        }
        */

        public void AddRangeDAL(List<T> p)
        {
            _context.AddRange(p);
            _context.SaveChanges();
        }

        public void DeleteDAL(T p)
        {
            var deletedEntity = _context.Entry(p);
            deletedEntity.State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public T GetByIdDAL(int id)
        {
            using var _context = new Context();
            return _context.Set<T>().Find(id);
        }

        public T GetDAL(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().SingleOrDefault(filter);
        }

        public void InsertDAL(T p)
        {
            var addedEntity = _context.Entry(p);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();
        }

        public List<T> ListDAL()
        {
            return _context.Set<T>().ToList();
        }

        public List<T> ListDAL(Expression<Func<T, bool>> filter)
        {
            using var c = new Context();
            return c.Set<T>().Where(filter).ToList();
        }

        public IQueryable<T> ListQueryableBL()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> ListQueryableBL(Expression<Func<T, bool>> filter)
        {
            using var c = new Context();
            return c.Set<T>().Where(filter);
        }

        public void UpdateDAL(T p)
        {
            var updatedEntity = _context.Entry(p);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}