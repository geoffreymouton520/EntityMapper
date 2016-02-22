using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Learning;
using Moq;
using NUnit.Framework;

namespace Tests.Learning
{
    [TestFixture]
    public class LearningManagerTests
    {

        [TestCase]
        public void CreateALearningSession()
        {
            //Arrange
            const int sourceSystemId = 1;
            const int destinationSystemId = 2;
            var sut = new LearningSessionDirector();
            var mockRepo = new Mock<IMappingRepository>();
            var trainingData = new List<Mapping>
            {
                new Mapping
                {
                    Source = "Commodity",
                    Destination = "commodity"
                }
            };

            var untrainedData = new UntrainedData
            {
                Sources = new List<string> { "Variety", "Farm" },
                Destinations = new List<string> { "variety", "farm" }
            };
            mockRepo.Setup(t => t.GetMappings(It.IsAny<int>(), It.IsAny<int>())).Returns(new TrainingData(trainingData));
            mockRepo.Setup(t => t.GetUntrainedData(It.IsAny<int>(), It.IsAny<int>())).Returns(untrainedData);
            var repo = mockRepo.Object;
            var sessionBuilder = new EntitySessionBuilder(repo);

            //Act
            sut.Constructor(sessionBuilder, sourceSystemId, destinationSystemId);
            var learningSession = sessionBuilder.GetResult();
            var result = learningSession.StartLearning();

            //Assert
            Assert.NotNull(result);
            var varietyMapping = result.First(t => t.Mapping.Source.Equals("Variety")).Mapping.Destination;
            Assert.AreEqual("variety", varietyMapping);
            var farmMapping = result.First(t => t.Mapping.Source.Equals("Farm")).Mapping.Destination;
            Assert.AreEqual("farm", farmMapping);


        }

    }
}
