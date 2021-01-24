using System;
using System.Collections.Generic;
using System.Text;
using MyStore_Core3.DomainClasses;

namespace MyStore_Core3.Services.Repositories
{
   public interface IOrderAppRepository:IBaseRepository<OrderApp>
    {

        ICollection<OrderApp> GetAllOrdersOfOneUserByUserId(string userId);
        // OrderApp GetOrderById(int orderAppId);
        // void InsertOrder(OrderApp orderApp);
        // void UpdateOrder(OrderApp orderApp);
        // void DeleteOrder(OrderApp orderApp);
        // void DeleteOrder(int orderAppId);
        // bool OrderExists(int orderAppId);
        // void Save();
    }
}
