using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyStore_Core3.DomainClasses;

namespace MyStore_Core3.Services.SearchContexts
{
   public class SearchResult
    {
        public SearchResult()
        {
            Result=new List<Product>();
        }
        public IEnumerable<Product> Result { get; set; }
        public int TotalCount { get; set; }

        public bool HasValue => Result.Any();
    }
}
