using System.Collections.Generic;
using System.Linq;

namespace Data.Contracts
{
    public interface IRepository<TModel> where TModel:class
    {
        IQueryable<TModel> GetAll();
        TModel GetById(int id);
        TModel Add(TModel model);
        TModel Update(TModel model);
        void Delete(TModel model);
        void Delete(int id);
    }
}