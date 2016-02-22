using Data.Models;
using Web.Models;
using AutoMapper;

namespace Web
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Domain, DomainViewModel>()
                .ForMember(dest => dest.Status,
                       opts => opts.MapFrom(src => src.Active ? "Active" : "Inactive"))
                .ForMember(dest => dest.Label,
                       opts => opts.MapFrom(src => src.Active ? "success" : "warning"))
                .ReverseMap();

            Mapper.CreateMap<Data.Models.System, SystemViewModel>()
                .ForMember(dest => dest.Status,
                       opts => opts.MapFrom(src => src.Active ? "Active" : "Inactive"))
                .ForMember(dest => dest.Label,
                       opts => opts.MapFrom(src => src.Active ? "success" : "warning"))
                .ForMember(dest => dest.Domain,
                        opts => opts.MapFrom(src => src.Domain.Name))
                .ReverseMap();

            Mapper.CreateMap<Entity, EntityViewModel>()
                .ForMember(dest => dest.Status,
                       opts => opts.MapFrom(src => src.Active ? "Active" : "Inactive"))
                .ForMember(dest => dest.Label,
                       opts => opts.MapFrom(src => src.Active ? "success" : "warning"))
                .ForMember(dest => dest.System,
                        opts => opts.MapFrom(src => src.System.Name))
                .ReverseMap();

            Mapper.CreateMap<Property, PropertyViewModel>()
                .ForMember(dest => dest.Status,
                       opts => opts.MapFrom(src => src.Active ? "Active" : "Inactive"))
                .ForMember(dest => dest.Label,
                       opts => opts.MapFrom(src => src.Active ? "success" : "warning"))
                .ForMember(dest => dest.Entity,
                        opts => opts.MapFrom(src => src.Entity.Name))
                .ReverseMap();

            Mapper.CreateMap<EntityMapping, EntityMappingViewModel>()
                .ForMember(dest => dest.SourceEntity,
                    opts => opts.MapFrom(src => src.Source.Name))
                .ForMember(dest => dest.DestinationEntity,
                    opts => opts.MapFrom(src => src.Destination.Name))
                .ForMember(dest => dest.DomainId,
                    opts => opts.MapFrom(src => src.Source.System.DomainId))
                .ForMember(dest => dest.SourceSystemId,
                    opts => opts.MapFrom(src => src.Source.SystemId))
                .ForMember(dest => dest.DestinationSystemId,
                    opts => opts.MapFrom(src => src.Destination.SystemId))
                .ForMember(dest => dest.SourceEntityId,
                    opts => opts.MapFrom(src => src.SourceId))
                .ForMember(dest => dest.DestinationEntityId,
                    opts => opts.MapFrom(src => src.DestinationId))
                .ForMember(dest => dest.MappingOrigin,
                    opts => opts.MapFrom(src => src.MappingOrigin.Name))
                .ForMember(dest => dest.ConfirmedStatus,
                    opts => opts.MapFrom(src => src.Confirmed ? "Confirmed" : "Unconfirmed"))
                .ForMember(dest => dest.ConfirmedLabel,
                    opts => opts.MapFrom(src => src.Confirmed ? "success" : "warning"))
                .ForMember(dest => dest.CorrectStatus,
                    opts => opts.MapFrom(src => src.Correct.HasValue ? (src.Correct.Value ? "Correct" : "Incorrect") : ""))
                .ForMember(dest => dest.CorrectLabel,
                    opts => opts.MapFrom(src => src.Correct.HasValue ? (src.Correct.Value ? "success" : "warning") : ""));


            Mapper.CreateMap<EntityMappingViewModel, EntityMapping>()
                .ForMember(dest => dest.SourceId,
                    opts => opts.MapFrom(src => src.SourceEntityId))
                .ForMember(dest => dest.DestinationId,
                    opts => opts.MapFrom(src => src.DestinationEntityId))
                .ForMember(dest => dest.Source,
                    opts => opts.Ignore())
                .ForMember(dest => dest.MappingOrigin, opts => opts.Ignore());

            Mapper.CreateMap<MappingOrigin, MappingOriginViewModel>()
                .ReverseMap();
        }


    }
}
