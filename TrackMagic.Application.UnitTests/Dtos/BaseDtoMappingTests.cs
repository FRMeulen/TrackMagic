﻿using AutoMapper;
using TrackMagic.Application.Dtos.Base;

namespace TrackMagic.Application.UnitTests.Dtos
{
    public abstract class BaseDtoMappingTests
    {
        protected readonly IMapper Mapper;
        protected readonly FixtureFactory FixtureFactory;

        public BaseDtoMappingTests()
        {
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(typeof(IDto))));
            FixtureFactory = new FixtureFactory("Dtos/Fixtures/");
        }
    }
}
