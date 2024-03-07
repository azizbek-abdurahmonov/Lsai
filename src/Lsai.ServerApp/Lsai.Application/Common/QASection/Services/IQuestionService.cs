using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;

namespace Lsai.Application.Common.QASection.Services;

public interface IQuestionService
{
    IQueryable<QuestionModel> Get(PaginationOptions paginationOptions);

    IQueryable<QuestionModel> GetByDocumentationPartId(Guid id);
}
