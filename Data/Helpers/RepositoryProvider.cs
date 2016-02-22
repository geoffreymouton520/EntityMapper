using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;

namespace Data.Helpers
{
    public class RepositoryProvider:IRepositoryProvider
    {
        public DbContext DbContext { get; set; }
        public IRepository<TModel> GetRepository<TModel>() where TModel : class
        {
            throw new NotImplementedException();
        }
    }
}
