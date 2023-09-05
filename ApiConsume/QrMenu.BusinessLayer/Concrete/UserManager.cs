using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using HotelAndTours.DataAccessLayer.Concrete;
using QrMenu.DataAccessLayer.Abstract;

namespace BusinessLayer.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDAL;

    public UserManager(IUserDal userDAL)
    {
        _userDAL = userDAL;
    }

    public void AddBL(AppUser t)
    {
        _userDAL.InsertDAL(t);
    }

    public void AddRangeBL(List<AppUser> p)
    {
        var context = new Context();
        context.AddRange(p);
        context.SaveChanges();
    }

    public List<AppUser> GetListBL()
    {
        return _userDAL.ListDAL();
    }

    public List<AppUser> GetListFilteredBL(string filter)
    {
        throw new NotImplementedException();
    }

    public IQueryable<AppUser> GetListQueryableBL()
    {
        throw new NotImplementedException();
    }

    public IQueryable<AppUser> GetListQueryableBL(string filter)
    {
        throw new NotImplementedException();
    }

    public void RemoveBL(AppUser t)
    {
        _userDAL.DeleteDAL(t);
    }

    public AppUser TGetByID(int id)
    {
        return _userDAL.GetByIdDAL(id);
    }

    public void UpdateBL(AppUser t)
    {
        _userDAL.UpdateDAL(t);
    }
}