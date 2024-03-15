using Lsai.Application.Common.QASection.Models;
using Lsai.Domain.Entities;

namespace Lsai.Application.Common.QASection.Services;

public interface IUserAnswerService
{
    IQueryable<UserAnswer> GetUserAnswers(UserAnswerFilterModel userAnswerFilterModel);
}
