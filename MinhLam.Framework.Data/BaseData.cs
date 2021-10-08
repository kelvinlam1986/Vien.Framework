using System.Collections.Generic;

namespace MinhLam.Framework.Data
{
    public abstract class BaseData<T> : IBaseData<T> where T: BaseEntity, new()
    {
        public BaseData()
        {
            ConnectionString = AppConnectionString.ConnectionString;
        }

        public BaseData(string connectionString)
        {
            ConnectionString = connectionString;
        }


        protected string ConnectionString { get; set; }

        public abstract int Insert(T entity);
        public abstract int Update(T entity);
        public abstract int Remove(T entity);
    }
}
