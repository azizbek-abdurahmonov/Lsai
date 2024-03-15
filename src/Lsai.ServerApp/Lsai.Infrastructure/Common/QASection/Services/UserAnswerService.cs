using Lsai.Application.Common.QASection.Models;
using Lsai.Application.Common.QASection.Services;
using Lsai.Domain.Common.Extensions;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lsai.Infrastructure.Common.QASection.Services;

public class UserAnswerService(IUserAnswerRepository userAnswerRepository) : IUserAnswerService
{
    public IQueryable<UserAnswer> GetUserAnswers(UserAnswerFilterModel userAnswerFilterModel)
    {
        var primaryQuery = userAnswerRepository.Get(
            answer => answer.UserId == userAnswerFilterModel.UserId)
            .Include(answer => answer.Question);

        var answers = primaryQuery.Where(answer => answer.Question!.DocumentationPartId == userAnswerFilterModel.DocumentationPartId);

        return answers.ApplyPagination(userAnswerFilterModel);
    }
}
