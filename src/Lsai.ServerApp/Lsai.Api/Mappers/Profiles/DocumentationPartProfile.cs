using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Domain.Entities;

namespace Lsai.Api.Mappers.Profiles;

public class DocumentationPartProfile : Profile
{
    public DocumentationPartProfile()
    {
        CreateMap<DocumentationPart, DocumentationPartDto>().ReverseMap();
    }
}
