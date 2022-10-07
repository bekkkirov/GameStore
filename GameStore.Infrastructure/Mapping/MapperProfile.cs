using AutoMapper;
using GameStore.Application.Models;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Mapping;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Genre, GenreModel>().ReverseMap();

        CreateMap<PlatformType, PlatformTypeModel>().ReverseMap();

        CreateMap<Comment, CommentModel>()
            .ForMember(d => d.Name,
                opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"));
        CreateMap<CommentCreateModel, Comment>();

        CreateMap<Game, GameModel>();
        CreateMap<GameCreateModel, Game>();

        CreateMap<Image, ImageModel>();
    }
}