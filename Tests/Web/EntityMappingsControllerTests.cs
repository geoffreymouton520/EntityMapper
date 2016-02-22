using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Autofac;
using Data;
using Data.Contracts;
using Data.Models;
using NUnit.Framework;
using Web;
using Web.Controllers;
using Web.Models;

namespace Tests.Web
{
    [TestFixture]
    public class EntityMappingsControllerTests
    {
        [TestCase]
        public void Get()
        {
            //arrange
            //Todo: Use Autofacs config for this initialization
            AutoMapperConfig.RegisterMappings();
            var db = new EntityMapperDbContext();
            var authAdapter = new IdentityAuthAdapter();
            var repo = new EfRepository<EntityMapping>(db, authAdapter);
            var _logger = new InMemoryLogger();
            EfRepository<MappingOrigin> originRepo = new EfRepository<MappingOrigin>(db, authAdapter);
            EfRepository<Entity> entityRepo = new EfRepository<Entity>(db, authAdapter);
            IMappingRepository mappingRepository = new EntityMappingRepository( repo, _logger, null, null);
            var sut = new EntityMappingsController(repo, originRepo, entityRepo, mappingRepository);
            var dbRecordCount = db.EntityMappings.Count();

            //act 
            var actionResult = sut.Get();
            var result = actionResult as OkNegotiatedContentResult<List<EntityMappingViewModel>>;
            //assert
            Assert.NotNull(result);
            var content = result.Content;

            Assert.AreEqual(dbRecordCount, content.Count);
        }

        [TestCase]
        public void Automate()
        {
            //arrange
            //Todo: Use Autofacs config for this initialization
            AutofacConfig.Register();
            AutoMapperConfig.RegisterMappings();
            //var db = new EntityMapperDbContext();
            //var authAdapter = new IdentityAuthAdapter();
            //var repo = new EfRepository<EntityMapping>(db, authAdapter);
            //var _logger = new InMemoryLogger();
            //EfRepository<MappingOrigin> originRepo = new EfRepository<MappingOrigin>(db, authAdapter);
            //EfRepository<Entity> entityRepo = new EfRepository<Entity>(db, authAdapter);
            //IMappingRepository mappingRepository = new EntityMappingRepository(db, repo, _logger);
            //var sut = new EntityMappingsController(repo, originRepo, entityRepo, mappingRepository);

            var DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(AutofacConfig.Container);

            using (var scope = AutofacConfig.Container.BeginLifetimeScope("'AutofacWebRequest"))
            //using (var scope = DependencyResolver.BeginScope())
            {
                var db = scope.Resolve<EntityMapperDbContext>();
                var sut = scope.Resolve<AutomationController>();
                var dbRecordCount = db.EntityMappings.Count();

                //act 
                var actionResult = sut.Automate(new AutomatedEntityMappingViewModel
                {
                    DestinationSystemId = 177,
                    SourceSystemId = 176,
                    DomainId = 98
                });
                var result = actionResult as OkNegotiatedContentResult<List<EntityMappingViewModel>>;
                //assert
                Assert.NotNull(result);
                var content = result.Content;

                Assert.AreEqual(dbRecordCount, content.Count);
            }
        }

    }
}
