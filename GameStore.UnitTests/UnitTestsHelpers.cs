using AutoMapper;
using GameStore.Infrastructure.Mapping;

namespace GameStore.UnitTests;

public class UnitTestsHelpers
{
    public static IMapper CreateMapperProfile()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperProfile());
        });

        return config.CreateMapper();
    }
}