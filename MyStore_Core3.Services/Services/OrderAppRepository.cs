using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyStore_Core3.DataLayer.Context;
using MyStore_Core3.DomainClasses;
using MyStore_Core3.Services.Repositories;

namespace MyStore_Core3.Services.Services
{
   public class OrderAppRepository:IOrderAppRepository
    {
        private MyStore_Core3DbContext _db;

        public OrderAppRepository(MyStore_Core3DbContext db)
        {
            _db = db;
        }

        public ICollection<OrderApp> GetAllEntities()
        {
            var result = _db.OrderApps.Include(c=>c.Customer).
                Include(p=>p.Product).
                ToList();
            return result;

        }

        public OrderApp GetEntityById(int entityId)
        {
            var result = _db.OrderApps.Include(c => c.Customer).
                Include(p => p.Product).
               FirstOrDefault(o=>o.OrderAppId==entityId);
            return result;

        }

        public void InsertEntity(OrderApp entity)
        {
            _db.OrderApps.Add(entity);
        }

        public void UpdateEntity(OrderApp entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteEntity(OrderApp entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
        }

        public void DeleteEntity(int entityId)
        {
            var orderApp = GetEntityById(entityId);
            DeleteEntity(orderApp);
        }

        public bool EntityExists(int entityId)
        {
            var result = _db.OrderApps.Any(p => p.OrderAppId == entityId);
            return result;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public ICollection<OrderApp> GetAllOrdersOfOneUserByUserId(string userId)
        {
            var orderList = _db.OrderApps.Include(c => c.Customer).Include(p => p.Product)
                .Where(o => o.CustomerId == userId).ToList();
            return orderList;
        }
    }
}
