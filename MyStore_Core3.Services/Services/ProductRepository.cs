using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyStore_Core3.DataLayer.Context;
using MyStore_Core3.DomainClasses;
using MyStore_Core3.Services.Repositories;
using MyStore_Core3.Services.SearchContexts;

namespace MyStore_Core3.Services.Services
{
   public class ProductRepository:IProductRepository
    {
        private MyStore_Core3DbContext _db;

        public ProductRepository(MyStore_Core3DbContext db)
        {
            
            _db = db;
        }

        public ICollection<Product> GetProductsByGroupId(int groupId)
        {
            var result= GetAllEntities().Where(p => p.ProductGroupId == groupId).ToList();
            return result;
        }

        public ICollection<Product> GetLateProducts()
        {

                var result=_db.Products.OrderByDescending(p => p.ProductId).Take(4).ToList();
                return result;

        }

        public IEnumerable<Product> Search(string parameter)
        {
            var search = _db.Products.Where(p =>
                p.ProductName.Contains(parameter) || p.ProductDescription.Contains(parameter)).ToList().Distinct();

            return search;
        }

        public ICollection<Product> GetAllEntities()
        {
            var result = _db.Products.Include(p => p.ProductGroup).ToList();
            return result;


        }

        public Product GetEntityById(int entityId)
        {

            var result = _db.Products.
                Include(p => p.ProductGroup).
                FirstOrDefault(o => o.ProductId == entityId);
            return result;
        }

        public void InsertEntity(Product entity)
        {
            _db.Products.Add(entity);
        }

        public void UpdateEntity(Product entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteEntity(Product entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
        }

        public void DeleteEntity(int entityId)
        {
            var product = GetEntityById(entityId);
            DeleteEntity(product);
        }

        public bool EntityExists(int entityId)
        {
            var Exists= _db.Products.Any(p => p.ProductId == entityId);
            return Exists;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public SearchResult SearchPlus(ProductSearchContext context)
        {
            throw new NotImplementedException();
        }
    }
}
