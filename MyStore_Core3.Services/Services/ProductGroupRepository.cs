using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyStore_Core3.DataLayer.Context;
using MyStore_Core3.DomainClasses;
using MyStore_Core3.Services.Repositories;
using MyStore_Core3.ViewModel;

namespace MyStore_Core3.Services.Services
{
   public class ProductGroupRepository: IProductGroupRepository
    {
        private MyStore_Core3DbContext _db;

        public ProductGroupRepository(MyStore_Core3DbContext db)
        {
            _db = db;
        }


        // public List<ShowProductGroupsViewModel> GetListGroups()
        // {
        //     return _db.ProductGroups.Select(g => new ShowProductGroupsViewModel()
        //     {
        //
        //         ProductGroupId = g.ProductGroupId,
        //         ProductGroupTitle = g.ProductGroupTitle,
        //         ProductCount = g.RelatedProducts.Count
        //     }).ToList();
        // }

        
        public ICollection<ProductGroup> GetAllEntities()
        {
            var result = _db.ProductGroups.ToList();
            return result;
        }
        
        public ProductGroup GetEntityById(int entityId)
        {
            var result = _db.ProductGroups.Find(entityId);
            return result;

        }
        
        public void InsertEntity(ProductGroup entity)
        {
            _db.ProductGroups.Add(entity);
        }
        
        public void UpdateEntity(ProductGroup entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }
        
        public void DeleteEntity(ProductGroup entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
        }
        
        public void DeleteEntity(int entityId)
        {
            var group = GetEntityById(entityId);
            DeleteEntity(group);
        }
        
        public bool EntityExists(int entityId)
        {
            var result = _db.ProductGroups.Any(p => p.ProductGroupId == entityId);
            return result;

        }
        
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
