using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Domain.Entities;

namespace Lsai.Api.Mappers.Profiles;

public class DocumentationLikeProfile : Profile
{
    public DocumentationLikeProfile()
    {
        CreateMap<DocumentationLike, DocumentationLikeDto>().ReverseMap();
    }
}
