using System;
using System.Collections.Generic;
using System.Text;
using MyStore_Core3.DomainClasses;


namespace MyStore_Core3.Services.Repositories
{
   public interface IProductRepository: IBaseRepository<Product>
    {
        ICollection<Product> GetLateProducts();
        ICollection<Product> GetProductsByGroupId(int groupId);
        IEnumerable<Product> Search(string parameter);
        void UpdateStockProduct(int productId,int sellCount);

        // IEnumerable<Product> GetAllProducts();
        // SearchResult SearchPlus(ProductSearchContext context);
        // Product GetProductById(int productId);
        // void InsertProduct(Product product);

        // void DeleteProduct(Product product);
        // void DeleteProduct(int productId);
        // bool ProductExists(int productId);
        // void Save();








    }
}
