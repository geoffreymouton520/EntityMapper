using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Models;
using NUnit.Framework;
using Web;
using Web.Models;

namespace Tests.Web.AutoMappings
{
    [TestFixture]
    public class EntityMappingTests
    {
        [TestCase]
        public void ViewModelToDbModel()
        {
            AutoMapperConfig.RegisterMappings();
            var viewModel = new EntityMappingViewModel
            {
                SourceEntityId = 1,
                SourceEntity = "Labour",
                DestinationEntityId = 2,
                DestinationEntity = "Quality Control",
                MappingOriginId = 1,
                MappingOrigin = "Manual",
                Confirmed = true,
                ConfirmedStatus = "Confirmed",
                ConfirmedLabel = "success",
                Correct = true,
                CorrectStatus = "Correct",
                CorrectLabel = "success",
                Id = 1
            };

            //act
            var dbModel = Mapper.Map<EntityMapping>(viewModel);

            //Assert
            Assert.AreEqual(viewModel.SourceEntityId, dbModel.SourceId);
            Assert.AreEqual(viewModel.DestinationEntityId, dbModel.DestinationId);

        }

        [TestCase]
        public void DbModelToViewModel()
        {
            AutoMapperConfig.RegisterMappings();
            var mappingOrigin = new MappingOrigin {
                Name = "Manual",
                Id = 1
            };
            var destination = new Entity
            {
                Name = "Quality Control",
                Active = true,
                Id = 2
            };
            var source = new Entity
            {
                Name = "Labour",
                Active = true,
                Id = 1
            };
            var dbModel = new EntityMapping
            {
                SourceId = source.Id,
                Source = source,
                DestinationId = destination.Id,
                Destination = destination,
                MappingOriginId = mappingOrigin.Id,
                MappingOrigin = mappingOrigin,
                Confirmed = true,
                Correct = true,
                Id = 1
            };

            //act
            var viewModel = Mapper.Map<EntityMappingViewModel>(dbModel);

            //Assert
            Assert.AreEqual(viewModel.SourceEntityId, dbModel.SourceId);
            Assert.AreEqual(viewModel.DestinationEntityId, dbModel.DestinationId);
        }
    }
}
