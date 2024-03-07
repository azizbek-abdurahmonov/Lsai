using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Domain.Entities;

namespace Lsai.Api.Mappers.Profiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<QuestionModel, QuestionDto>().ReverseMap();
    }
}
