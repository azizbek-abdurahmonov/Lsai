using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Domain.Entities;

namespace Lsai.Api.Mappers.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>().ReverseMap();
    }
}
