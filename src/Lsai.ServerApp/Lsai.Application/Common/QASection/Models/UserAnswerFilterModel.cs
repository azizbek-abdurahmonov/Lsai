using Lsai.Domain.Common.Filters;

namespace Lsai.Application.Common.QASection.Models;

public class UserAnswerFilterModel : PaginationOptions
{
    public Guid UserId { get; set; }

    public Guid DocumentationPartId { get; set; }
}
