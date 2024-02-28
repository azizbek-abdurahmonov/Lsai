using Lsai.Application.Common.Notification.Services;
using Lsai.Domain.Common.Enums;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Lsai.Infrastructure.Common.Notification.Services;

public class EmailTemplateService(IEmailTemplateRepository emailTemplateRepository) : IEmailTemplateService
{
    public ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
        => emailTemplateRepository.CreateAsync(emailTemplate, saveChanges, cancellationToken);

    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = null)
        => emailTemplateRepository.Get(predicate);

    public ValueTask<EmailTemplate> GetByTypeAsync(NotificationType notificationType, CancellationToken cancellationToken = default)
    {
        var template = emailTemplateRepository.Get().FirstOrDefault(template => template.Type == notificationType)
            ?? throw new InvalidOperationException("Email template doesn't exist with this type!");

        return new(template);
    }
}
