using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Models;
using NUnit.Framework;

namespace Tests.Learning
{
    public class LearningManualIntegratedTests
    {

        private EntityMapperDbContext _db;
        private IdentityAuthAdapter _authAdapter;
        private int _sourceSystemId;
        private int _destinationSystemId;


        [SetUp]
        public void Setup()
        {
            //Prefill
            _authAdapter = new IdentityAuthAdapter();
            _db = new EntityMapperDbContext();

            var domainCrud = new EfRepository<Domain>(_db, _authAdapter);
            var domain = domainCrud.Add(new Domain
            {
                Name = "Fruit",
                Active = true
            });



            var systemCrud = new EfRepository<global::Data.Models.System>(_db, _authAdapter);
            var sourceSystem = systemCrud.Add(new global::Data.Models.System
            {
                Domain = domain,
                Name = "Export",
                Active = true
            });

            var destinationSystem = systemCrud.Add(new global::Data.Models.System
            {
                Domain = domain,
                Name = "Import",
                Active = true
            });
            _sourceSystemId = sourceSystem.Id;
            _destinationSystemId = destinationSystem.Id;





            var entityCrud = new EfRepository<Entity>(_db, _authAdapter);
            entityCrud.Add(new Entity
            {
                SystemId = _sourceSystemId,
                Name = "Commodity",
                Active = true
            });

            entityCrud.Add(new Entity
            {
                SystemId = _destinationSystemId,
                Name = "commodity",
                Active = true
            });
        }

        [TearDown]
        public void TearDown()
        {
            //EnityMappings
            var existingMappings = _db.EntityMappings.ToList();
            _db.EntityMappings.RemoveRange(existingMappings);
            _db.SaveChanges();

            //Entities
            var existingEntities = _db.Entities.ToList();
            _db.Entities.RemoveRange(existingEntities);
            _db.SaveChanges();

            //Systems
            var existingSystems = _db.Systems.ToList();
            _db.Systems.RemoveRange(existingSystems);
            _db.SaveChanges();


            //Domain
            var existingDomains = _db.Domains.ToList();
            _db.Domains.RemoveRange(existingDomains);
            _db.SaveChanges();
        }

        [TestCase]
        public void CreateManualMappings()
        {


            //Arrange
            var crudRepo = new EfRepository<EntityMapping>(_db, _authAdapter);
            var logger = new InMemoryLogger();
            var repo = new EntityMappingRepository( crudRepo, logger, null, null);



            var trainingData = new List<Mapping>
            {
                new Mapping
                {
                    Source = "Commodity",
                    Destination = "commodity"
                }
            };



            //Act
            var result = repo.CreateMapping(new ManualMapping
            {
                Mappings = trainingData,
                SourceId = _sourceSystemId,
                DestinationId = _destinationSystemId
            });

            //Assert
            Assert.AreEqual(trainingData.Count, result);

        }
    }
}