using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Domain.Entities;

namespace Lsai.Api.Mappers.Profiles;

public class UserAnswerProfile : Profile
{
    public UserAnswerProfile()
    {
        CreateMap<UserAnswer, UserAnswerDto>().ReverseMap();
    }
}
