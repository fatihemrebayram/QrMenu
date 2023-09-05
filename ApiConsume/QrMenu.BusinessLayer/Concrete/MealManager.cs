using Microsoft.EntityFrameworkCore;
using QrMenu.BusinessLayer.Abstract;
using QrMenu.DataAccessLayer.Abstract;
using QrMenu.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.BusinessLayer.Concrete;

    public class MealManager : IMealService
    {
        private readonly IMealDal _mealDal;

        public MealManager(IMealDal mealDal)
        {
            _mealDal = mealDal;
        }

        public void AddBL(Meal t)
        {
           _mealDal.InsertDAL(t);
        }

        public void AddRangeBL(List<Meal> p)
        {
            _mealDal.AddRangeDAL(p);
        }

        public List<Meal> GetListBL()
        {
            return _mealDal.ListDAL();
        }

        public List<Meal> GetListFilteredBL(string filter)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Meal> GetListQueryableBL()
        {
        return _mealDal.ListQueryableBL()
            .Include(x => x.Category);
    }

        public IQueryable<Meal> GetListQueryableBL(string filter)
        {
        return _mealDal.ListQueryableBL(x => x.Name.Contains(filter))
            .Include(x => x.Category);
    }

        public void RemoveBL(Meal t)
        {
          _mealDal.DeleteDAL(t);
        }

        public Meal TGetByID(int id)
        {
            return _mealDal.GetByIdDAL(id);
        }

        public void UpdateBL(Meal t)
        {
           _mealDal.UpdateDAL(t);
        }
    }
