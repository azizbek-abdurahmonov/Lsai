using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Domain.Entities;

namespace Lsai.Api.Mappers.Profiles;

public class DocumentationModelProfile : Profile
{
    public DocumentationModelProfile()
    {
        CreateMap<DocumentationModel, DocumentationModelDto>().ReverseMap();
    }
}
