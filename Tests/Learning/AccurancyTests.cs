using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Learning;
using NUnit.Framework;

namespace Tests.Learning
{
    [TestFixture]
    public class AccurancyTests
    {
        private EntityMapperDbContext _db;








        [TestCase]
        public void With5()
        {
            var mappingPercentiles = new List<int>();
            var numMappings = new List<int>();
            for (var i = 0; i < 10; i++)
            {
                AddEntities(5, mappingPercentiles, numMappings);
            }
            var avg = mappingPercentiles.Average();
            var avg1 = numMappings.Average();
            Assert.AreEqual(10, mappingPercentiles.Count);
        }

        [TestCase]
        public void With10()
        {
            var mappingPercentiles = new List<int>();
            var numMappings = new List<int>();
            for (var i = 0; i < 10; i++)
            {
                AddEntities(10, mappingPercentiles, numMappings);
            }
            var avg = mappingPercentiles.Average();
            var avg1 = numMappings.Average();
            Assert.AreEqual(10, mappingPercentiles.Count);
        }

        [TestCase]
        public void With20()
        {
            var mappingPercentiles = new List<int>();
            var numMappings = new List<int>();
            for (var i = 0; i < 10; i++)
            {
                AddEntities(20, mappingPercentiles, numMappings);
            }
            var avg = mappingPercentiles.Average();
            var avg1 = numMappings.Average();
            Assert.AreEqual(10, mappingPercentiles.Count);
        }

        public void AddEntities(int record, List<int> mappingPercentiles, List<int> numMappings)
        {
            try
            {

                _db = new EntityMapperDbContext();
                var testData = _db.TestDatas.Take(record).ToList();
                var learningSession = new LearningSession(new UntrainedData
                {
                    Sources = testData.Select(t => t.Source).ToList(),
                    Destinations = testData.Select(t => t.Destination).ToList()
                });
                var result = learningSession.StartLearning().Select(t => t.Mapping).ToList();
                var resultText = string.Join(@"\n", result.Select(t => t.Source + " - " + t.Destination).ToList());

                var correctMappings = 0;
                foreach (var correctMapping in testData)
                {
                    var calculatedMappings = result.First(t => t.Source.Equals(correctMapping.Source));
                    //var correctMapping = testData.Single(t => t.Source.Equals(calculatedMappings.Source));
                    if (calculatedMappings.Destination.Equals(correctMapping.Destination))
                    {
                        correctMappings++;
                    }
                }
                var percentage = ((decimal)correctMappings / (decimal)testData.Count) * 100.0M;

                mappingPercentiles.Add(Convert.ToInt32(percentage));
                numMappings.Add(result.Count());

                Assert.AreEqual(record, result.Count());
            }
            catch (Exception ex)
            {
            }
        }
        [TestCase]
        public void KristalTest()
        {
            _db = new EntityMapperDbContext();
            _db = new EntityMapperDbContext();
            var sources = _db.TestDatas.Where(t => t.Source.Trim() != "").Select(t => t.Source).ToList();
            var destinations = _db.TestDatas.Where(t => t.Destination.Trim() != "").Select(t => t.Destination).ToList();
            var _authAdapter = new IdentityAuthAdapter();
            var _sourceIds = new List<int>();
            var _destinationIds = new List<int>();

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
                Name = "Functions",
                Active = true
            });

            var destinationSystem = systemCrud.Add(new global::Data.Models.System
            {
                Domain = domain,
                Name = "Tables",
                Active = true
            });
            var _sourceSystemId = sourceSystem.Id;
            var _destinationSystemId = destinationSystem.Id;
            foreach (var source in sources)
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
            foreach (var destination in destinations)
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
            var _logger = new InMemoryLogger();
            var _entityMappingCrud = new EfRepository<EntityMapping>(_db, _authAdapter);
            var _repo = new EntityMappingRepository(_entityMappingCrud, _logger, null, null);

            var learningSession = new LearningSession(new UntrainedData
            {
                Sources = _db.Entities.Where(t => _sourceIds.Contains(t.Id)).Select(t => t.Name).ToList(),
                Destinations = _db.Entities.Where(t => _destinationIds.Contains(t.Id)).Select(t => t.Name).ToList()
            });
            var result = learningSession.StartLearning();




            _repo.CreateMapping(new AutomatedMapping
            {
                LearningResult = result,
                SourceId = _sourceSystemId,
                DestinationId = _destinationSystemId
            });

            //var learningSession = new LearningSession(new UntrainedData
            //{
            //    Sources = sources,
            //    Destinations = destinations
            //});
            //var result = learningSession.StartLearning().Select(t => t.Mapping).ToList();
            var resultText = string.Join(@"\n", result.Select(t => t.Mapping).ToList().Select(t => t.Source + " - " + t.Destination).ToList());


            Assert.NotNull(resultText);
        }


    }
}
