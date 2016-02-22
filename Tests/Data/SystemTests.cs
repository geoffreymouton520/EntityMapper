using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using NUnit.Framework;

namespace Tests.Data
{
    [TestFixture]
    public class SystemTests
    {
        [TestCase]
        public void AddSystem()
        {
            var context = new EntityMapperDbContext();
            var authAdapter = new IdentityAuthAdapter();
            var sut = new EfRepository<global::Data.Models.System>(context, authAdapter);

            var result = sut.Add(new global::Data.Models.System
            {
                DomainId = 1,
                Active = true,
                Name = "First System"
            });

            Assert.NotNull(result);
            Assert.Greater(result.Id,0);
            var systems = context.Domains.Find(1).Systems.ToList();
            Assert.Greater(systems.Count,0);
            Assert.NotNull(result.Domain);
            context.Systems.Remove(result);
            context.SaveChanges();

        }
    }
}
