using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Application.Common.QASection.Models;
using Lsai.Application.Common.QASection.Services;
using Lsai.Domain.Common.Constants;
using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lsai.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserAnswersController(
    IUserAnswerRepository userAnswerRepository,
    IUserAnswerService userAnswerService,
    IMapper mapper) : ControllerBase
{
    [Authorize, HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] UserAnswerDto userAnswerDto, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)!.Value);

        var userAnswer = mapper.Map<UserAnswer>(userAnswerDto);
        userAnswer.UserId = userId;

        var result = await userAnswerRepository.CreateAsync(userAnswer, cancellationToken: cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { answerId = result.Id },
            mapper.Map<UserAnswerDto>(result));
    }

    [HttpGet("{answerId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid answerId, CancellationToken cancellationToken)
    {
        var result = await userAnswerRepository.GetByIdAsync(answerId, cancellationToken: cancellationToken);

        return result is not null
            ? Ok(mapper.Map<UserAnswerDto>(result))
            : NotFound();
    }

    [HttpGet("{userId:guid}/{documentationPartId:guid}")]
    public ValueTask<IActionResult> Get([FromRoute] Guid userId, [FromRoute] Guid documentationPartId, [FromQuery] PaginationOptions paginationOptions)
    {
        var filterModel = new UserAnswerFilterModel
        {
            UserId = userId,
            DocumentationPartId = documentationPartId,
            PageSize = paginationOptions.PageSize,
            PageToken = paginationOptions.PageToken,
        };

        var result = userAnswerService.GetUserAnswers(filterModel);
        
        return new(result.Any()
            ? Ok(mapper.Map<ICollection<UserAnswerDto>>(result))
            : NoContent());
    }
}
