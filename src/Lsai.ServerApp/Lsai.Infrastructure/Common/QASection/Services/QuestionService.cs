using Lsai.Application.Common.QASection.Services;
using Lsai.Domain.Common.Extensions;
using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;

namespace Lsai.Infrastructure.Common.QASection.Services;

public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
{
    public IQueryable<QuestionModel> Get(PaginationOptions paginationOptions)
    {
        var questions = questionRepository.Get();
        return questions.ApplyPagination(paginationOptions);
    }

    public IQueryable<QuestionModel> GetByDocumentationPartId(Guid id)
    {
        var questions = questionRepository
            .Get()
            .Where(question => question.DocumentationPartId == id);

        return questions;
    }
}
