using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Domain.Entities;

namespace Lsai.Api.Mappers.Profiles;

public class FutureMailProfile : Profile
{
    public FutureMailProfile()
    {
        CreateMap<FutureMail, FutureMailDto>().ReverseMap();
    }
}
