using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;

namespace Data
{
    public class EfRepository<TModel>:IRepository<TModel> where TModel:class 
    {
        protected DbContext DbContext;
        protected readonly IAuthenticationAdapter AuthenticationAdapter;
        protected DbSet<TModel> DbSet;

        public EfRepository(DbContext dbContext, IAuthenticationAdapter authenticationAdapter)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            DbContext = dbContext;
            AuthenticationAdapter = authenticationAdapter;
            DbSet = dbContext.Set<TModel>();
        }

        public virtual IQueryable<TModel> GetAll()
        {
            return DbSet;
        }

        public virtual TModel GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual TModel Add(TModel model)
        {
            var creatable = model as ICreatable;
            if (creatable != null)
            {
                var createble = creatable;
                createble.CreatedBy = AuthenticationAdapter.Username;
                createble.CreatedOn = DateTime.Now;
            }
            DbEntityEntry dbEntityEntry = DbContext.Entry(model);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(model);
            }
            DbContext.SaveChanges();
            return model;
        }

        public virtual TModel Update(TModel model)
        {
            var modifible = model as IModifiable;
            if (modifible != null)
            {
                var createble = modifible;
                createble.ModifiedBy = AuthenticationAdapter.Username;
                createble.ModifiedOn = DateTime.Now;
            }

            DbEntityEntry dbEntityEntry = DbContext.Entry(model);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(model);
            }
            dbEntityEntry.State = EntityState.Modified;
            DbContext.SaveChanges();
            return model;
        }

        public virtual void Delete(TModel model)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(model);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(model);
                DbSet.Remove(model);
            }
            DbContext.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var model = GetById(id);
            if (model==null)
            {
                return;
            }
            Delete(model);
            DbContext.SaveChanges();
        }
    }
}
