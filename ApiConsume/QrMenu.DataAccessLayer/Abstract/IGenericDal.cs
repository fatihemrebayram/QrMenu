using System.Linq.Expressions;

namespace QrMenu.DataAccessLayer.Abstract;

public interface IGenericDal<T>
{
    void AddRangeDAL(List<T> p);

    void DeleteDAL(T p);

    T GetByIdDAL(int id);

    T GetDAL(Expression<Func<T, bool>> filter);

    void InsertDAL(T p);

    List<T> ListDAL();
    IQueryable<T> ListQueryableBL();


    List<T> ListDAL(Expression<Func<T, bool>> filter);
    IQueryable<T> ListQueryableBL(Expression<Func<T, bool>> filter);

    void UpdateDAL(T p);
}