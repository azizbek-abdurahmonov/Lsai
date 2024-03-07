using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Application.Common.QASection.Services;
using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lsai.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController(
    IQuestionRepository questionRepository,
    IQuestionService questionService,
    IQuestionOptionService questionOptionService,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] QuestionDto questionDto, CancellationToken cancellationToken)
    {
        var result = await questionRepository.CreateAsync(mapper.Map<QuestionModel>(questionDto), cancellationToken: cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, mapper.Map<QuestionDto>(result));
    }

    [HttpGet]
    public ValueTask<IActionResult> Get([FromQuery] PaginationOptions paginationOptions)
    {
        var result = questionService.Get(paginationOptions);
        return new(result.Any() ? Ok(mapper.Map<List<QuestionDto>>(result)) : NoContent());
    }

    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await questionRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
        return result is not null ? Ok(mapper.Map<QuestionDto>(result)) : NotFound();
    }

    [HttpGet("{id:guid}/options")]
    public ValueTask<IActionResult> GetOptions([FromRoute] Guid id)
    {
        var result = questionOptionService.GetByQuestionId(id);
        return new(result.Any() ? Ok(mapper.Map<List<QuestionOptionDto>>(result)) : NoContent());
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] QuestionDto questionDto, CancellationToken cancellationToken)
    {
        var result = await questionRepository.UpdateAsync(mapper.Map<QuestionModel>(questionDto), cancellationToken: cancellationToken);
        return result is not null ? Ok(mapper.Map<QuestionDto>(result)) : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await questionRepository.DeleteByIdAsync(id, cancellationToken: cancellationToken);
        return result is not null ? Ok(mapper.Map<QuestionDto>(result)) : BadRequest();
    }
}
