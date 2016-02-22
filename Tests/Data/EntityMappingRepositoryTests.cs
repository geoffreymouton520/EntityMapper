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
    public class EntityMappingRepositoryTests
    {
        [TestCase]
        public void AutomatedMapping()
        {
            var db = new EntityMapperDbContext();
            var auth = new IdentityAuthAdapter();
            var repository = new EfRepository<EntityMapping>(db,auth);
            var logger= new InMemoryLogger();
            var sut = new EntityMappingRepository(repository,logger,null,null);
            var automatedMapping = new AutomatedMapping
            {
                DestinationId = 177,
                SourceId = 176,
                LearningResult = new LearningResult(new List<LearntMapping>
                {
                    new LearntMapping
                    {
                        Mapping = new Mapping
                        {
                            Source = "qmob_commodity",
                            Destination = "qcommodity"
                        }
                    }
                })
            };

            //act
            var result = sut.CreateMapping(automatedMapping);

            //assert
            Assert.AreEqual(1,result.Count());
        }
    }
}
