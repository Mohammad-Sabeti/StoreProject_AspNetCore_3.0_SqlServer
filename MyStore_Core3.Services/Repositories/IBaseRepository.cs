using System;
using System.Collections.Generic;
using System.Text;

namespace MyStore_Core3.Services.Repositories
{
  public interface IBaseRepository<T> where T:class
    {
        ICollection<T> GetAllEntities();
        // IEnumerable<T> GetLateProducts();
        // IEnumerable<T> GetProductsByGroupId(int entitygroupId);
        // IEnumerable<T> Search(string parameter);
        // SearchResult SearchPlus(ProductSearchContext context);
        T GetEntityById(int entityId);
        void InsertEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
        void DeleteEntity(int entityId);
        bool EntityExists(int entityId);
        void Save();
    }
}
