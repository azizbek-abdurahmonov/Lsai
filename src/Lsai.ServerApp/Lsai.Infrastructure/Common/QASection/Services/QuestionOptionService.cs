using Lsai.Application.Common.QASection.Services;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;

namespace Lsai.Infrastructure.Common.QASection.Services;

public class QuestionOptionService(IQuestionOptionRepository questionOptionRepository) : IQuestionOptionService
{
    public IQueryable<QuestionOption> GetByQuestionId(Guid id)
    {
        var questionOptions = questionOptionRepository.Get()
            .Where(option => option.QuestionId == id);

        return questionOptions;
    }
}
