using AutoMapper;
using TrackMagic.Application.Dtos.Base;

namespace TrackMagic.Application.UnitTests.Dtos
{
    public abstract class BaseDtoMappingTests
    {
        protected readonly IMapper Mapper;

        public BaseDtoMappingTests()
            => Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(typeof(IDto))));
    }
}
