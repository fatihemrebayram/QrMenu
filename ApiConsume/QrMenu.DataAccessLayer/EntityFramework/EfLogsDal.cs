using EntityLayer.Concrete;
using HotelAndTours.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrMenu.DataAccessLayer.Abstract;
using QrMenu.DataAccessLayer.Repositories;

namespace QrMenu.DataAccessLayer.EntityFramewok;

public class EfLogsDal : GenericRepositories<Logs>, ILogDal
    {
        public EfLogsDal(Context context) : base(context)
        {
        }
    }
