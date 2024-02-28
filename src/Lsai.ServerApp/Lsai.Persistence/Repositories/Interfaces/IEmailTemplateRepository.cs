using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories.Interfaces;

public interface IEmailTemplateRepository
{
    ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate,bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default);
}
