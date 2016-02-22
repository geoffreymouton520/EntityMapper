using System.Data.Entity;
using Data.Contracts;

namespace Data.Helpers
{
    public interface IRepositoryProvider
    {
        DbContext DbContext { get; set; }
        IRepository<TModel> GetRepository<TModel>() where TModel:class;
    }
}