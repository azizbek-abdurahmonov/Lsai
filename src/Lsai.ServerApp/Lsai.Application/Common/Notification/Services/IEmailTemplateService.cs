using Lsai.Domain.Common.Enums;
using Lsai.Domain.Entities;
using System.Linq.Expressions;

namespace Lsai.Application.Common.Notification.Services;

public interface IEmailTemplateService
{
    ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);

    IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default);

    ValueTask<EmailTemplate> GetByTypeAsync(NotificationType notificationType, CancellationToken cancellationToken = default);
}
