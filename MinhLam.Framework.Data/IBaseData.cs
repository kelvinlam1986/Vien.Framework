namespace MinhLam.Framework.Data
{
    public interface IBaseData<T> where T: BaseEntity
    {
        int Insert(T entity);
        int Update(T entity);
        int Remove(T entity);
    }
}
