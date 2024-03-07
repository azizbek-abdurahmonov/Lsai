using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Domain.Entities;

namespace Lsai.Api.Mappers.Profiles;

public class QuestionOptionProfile : Profile
{
    public QuestionOptionProfile()
    {
        CreateMap<QuestionOption, QuestionOptionDto>().ReverseMap();
    }
}
