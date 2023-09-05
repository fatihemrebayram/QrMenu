using Microsoft.EntityFrameworkCore;
using QrMenu.BusinessLayer.Abstract;
using QrMenu.DataAccessLayer.Abstract;
using QrMenu.EntityLayer.Concrete;

namespace QrMenu.BusinessLayer.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;

    public CategoryManager(ICategoryDal categoryDal)
    {
        _categoryDal = categoryDal;
    }

    public void AddBL(Category t)
    {
        _categoryDal.InsertDAL(t);
    }

    public void AddRangeBL(List<Category> p)
    {
        _categoryDal.AddRangeDAL(p);
    }

    public List<Category> GetListBL()
    {
        return _categoryDal.ListDAL();
    }

    public List<Category> GetListFilteredBL(string filter)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Category> GetListQueryableBL()
    {
        return _categoryDal.ListQueryableBL()
            .Include(x => x.Meals);
    }

    public IQueryable<Category> GetListQueryableBL(string filter)
    {
        return _categoryDal.ListQueryableBL(x => x.Name.Contains(filter))
            .Include(x => x.Meals);
    }

    public void RemoveBL(Category t)
    {
        _categoryDal.DeleteDAL(t);
    }

    public Category TGetByID(int id)
    {
        return _categoryDal.GetByIdDAL(id);
    }

    public void UpdateBL(Category t)
    {
        _categoryDal.UpdateDAL(t);
    }
}