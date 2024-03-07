using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Application.Common.QASection.Services;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lsai.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionOptionsController(
    IQuestionOptionRepository questionOptionRepository,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] QuestionOptionDto questionOptionDto, CancellationToken cancellationToken)
    {
        var result = await questionOptionRepository.CreateAsync(mapper.Map<QuestionOption>(questionOptionDto), cancellationToken: cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, mapper.Map<QuestionOptionDto>(result));
    }

    [HttpGet]
    public ValueTask<IActionResult> Get()
    {
        var result = questionOptionRepository.Get();
        return new(result.Any() ? Ok(mapper.Map<List<QuestionOptionDto>>(result)) : NoContent());
    }

    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await questionOptionRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        return result is not null ? Ok(mapper.Map<QuestionOptionDto>(result)) : NotFound();
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] QuestionOptionDto questionOptionDto, CancellationToken cancellationToken)
    {
        var result = await questionOptionRepository.UpdateAsync(mapper.Map<QuestionOption>(questionOptionDto), cancellationToken: cancellationToken);
        return result is not null ? Ok(mapper.Map<QuestionOptionDto>(result)) : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await questionOptionRepository.DeleteByIdAsync(id, cancellationToken: cancellationToken);
        return result is not null ? Ok(mapper.Map<QuestionOptionDto>(result)) : NotFound();
    }
}
