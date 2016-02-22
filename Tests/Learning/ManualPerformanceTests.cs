using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Models;
using NUnit.Framework;

namespace Tests.Learning
{
    [TestFixture]
    public class ManualPerformanceTests
    {
        private IdentityAuthAdapter _authAdapter;
        private EntityMapperDbContext _db;
        private int _sourceSystemId;
        private int _destinationSystemId;
        private List<int> _sourceIds;
        private List<int> _destinationIds;

        //[SetUp]
        public void SetUp(int record)
        {
            _db = new EntityMapperDbContext();
            var testData = _db.TestDatas.Take(record).ToList();
            _authAdapter = new IdentityAuthAdapter();
            _sourceIds = new List<int>();
            _destinationIds = new List<int>();

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
                Name = "QualityControl",
                Active = true
            });

            var destinationSystem = systemCrud.Add(new global::Data.Models.System
            {
                Domain = domain,
                Name = "Labour",
                Active = true
            });
            _sourceSystemId = sourceSystem.Id;
            _destinationSystemId = destinationSystem.Id;
            foreach (var source in testData.Select(t => t.Source))
            {
                var entityCrud = new EfRepository<Entity>(_db, _authAdapter);
                var entity = entityCrud.Add(new Entity
                {
                    SystemId = _sourceSystemId,
                    Name = source.Trim(),
                    Active = true
                });
                _sourceIds.Add(entity.Id);
            }
            foreach (var destination in testData.Select(t => t.Destination))
            {
                var entityCrud = new EfRepository<Entity>(_db, _authAdapter);
                var entity = entityCrud.Add(new Entity
                {
                    SystemId = _destinationSystemId,
                    Name = destination.Trim(),
                    Active = true
                });
                _destinationIds.Add(entity.Id);
            }

        }


        //[TearDown]
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
        public void With5()
        {
            var times = new List<long>();
            for (var i = 0; i < 10; i++)
            {
                AddEntities(5, times); 
            }
            var avg = times.Average();
            Assert.AreEqual(10,times.Count);
        }

        [TestCase]
        public void With10()
        {
            var times = new List<long>();
            for (var i = 0; i < 10; i++)
            {
                AddEntities(10, times);
            }
            var avg = times.Average();
            Assert.AreEqual(10, times.Count);
        }

        [TestCase]
        public void With20()
        {
            var times = new List<long>();
            for (var i = 0; i < 10; i++)
            {
                AddEntities(20, times);
            }
            var avg = times.Average();
            Assert.AreEqual(10, times.Count);
        }

        public void AddEntities(int record, List<long> times)
        {
            SetUp(record);
            try
            {
                var timer = new Stopwatch();
                timer.Start();
                var entityMappingCrud = new EfRepository<EntityMapping>(_db, _authAdapter);
                for (var i = 0; i < _sourceIds.Count; i++)
                {
                    entityMappingCrud.Add(new EntityMapping
                    {
                        MappingOriginId = 1,
                        SourceId = _sourceIds[i],
                        DestinationId = _destinationIds[i],
                        Confirmed = false
                    });
                }
                timer.Stop();
                times.Add(timer.ElapsedMilliseconds);
                var mappings = _db.EntityMappings.Take(record).ToList();
                Assert.AreEqual(record, mappings.Count);
            }
            catch (Exception)
            {
            }
            TearDown();
        }
    }
}
