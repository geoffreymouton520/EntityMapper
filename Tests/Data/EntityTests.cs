using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Models;
using NUnit.Framework;

namespace Tests.Data
{
    [TestFixture]
    public class EntityTests
    {
        [TestCase]
        public void AddASingleEntity()
        {
            var context = new EntityMapperDbContext();
            var authAdapter = new IdentityAuthAdapter();
            var sut = new EfRepository<Entity>(context, authAdapter);

            var result = sut.Add(new Entity
            {
                SystemId = 1,
                Active = true,
                Name = "First Entity"
            });

            Assert.NotNull(result);
            Assert.Greater(result.Id, 0);
            var entities = context.Systems.Find(1).Entities.ToList();
            Assert.Greater(entities.Count, 0);
            Assert.NotNull(result.System);
            context.Entities.Remove(result);
            context.SaveChanges();
        }
    }
}
