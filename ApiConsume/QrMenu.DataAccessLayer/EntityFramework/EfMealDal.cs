using HotelAndTours.DataAccessLayer.Concrete;
using QrMenu.DataAccessLayer.Abstract;
using QrMenu.DataAccessLayer.Repositories;
using QrMenu.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrMenu.DataAccessLayer.EntityFramework;

    public class EfMealDal : GenericRepositories<Meal>, IMealDal
{
    public EfMealDal(Context context) : base(context)
    {
    }
}

