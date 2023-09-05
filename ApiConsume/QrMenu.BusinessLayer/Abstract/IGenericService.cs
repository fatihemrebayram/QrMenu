namespace BusinessLayer.Abstract;

public interface IGenericService<T>
{
    void AddBL(T t);

    void AddRangeBL(List<T> p);

    List<T> GetListBL();
    IQueryable<T> GetListQueryableBL();
    IQueryable<T> GetListQueryableBL(string filter);

    List<T> GetListFilteredBL(string filter);

    void RemoveBL(T t);

    T TGetByID(int id);

    void UpdateBL(T t);
}