using System;
using System.Collections.Generic;
using System.Text;
using MyStore_Core3.DomainClasses;
using MyStore_Core3.ViewModel;

namespace MyStore_Core3.Services.Repositories
{
   public interface IProductGroupRepository:IBaseRepository<ProductGroup>
    {
        // List<ProductGroup> GetAllProductGroups();
        // ProductGroup GetProductGroupById(int groupId);
        // void InsertProductGroup(ProductGroup productGroup);
        // void UpdateProductGroup(ProductGroup productGroup);
        // void DeleteProductGroup(ProductGroup productGroup);
        // void DeleteProductGroup(int groupId);
        // bool ProductGroupExists(int groupId);
        // void Save();
        List<ShowProductGroupPartialViewModel> GetListGroupsPartialViewModel();

    }
}
