using HotelAndTours.DataAccessLayer.Concrete;
using QrMenu.DataAccessLayer.Abstract;
using QrMenu.DataAccessLayer.Repositories;
using QrMenu.EntityLayer.Concrete;

namespace QrMenu.DataAccessLayer.EntityFramework;

public class EfCategoryDal : GenericRepositories<Category>, ICategoryDal
{
    public EfCategoryDal(Context context) : base(context)
    {
    }
}