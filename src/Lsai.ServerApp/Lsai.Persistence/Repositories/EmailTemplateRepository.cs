using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Persistence.Repositories;

public class EmailTemplateRepository(AppDbContext dbContext)
    : EntityRepositoryBase<EmailTemplate>(dbContext), IEmailTemplateRepository
{
    public new ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(emailTemplate, saveChanges, cancellationToken);

    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = null)
        => base.Get(predicate, true);
}
