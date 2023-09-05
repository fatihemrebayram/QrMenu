using EntityLayer.Concrete;
using HotelAndTours.DataAccessLayer.Concrete;
using QrMenu.DataAccessLayer.Abstract;
using QrMenu.DataAccessLayer.Repositories;

namespace QrMenu.DataAccessLayer.EntityFramework;

public class EfUserDal : GenericRepositories<AppUser>, IUserDal
{
    public EfUserDal(Context context) : base(context)
    {
    }
}