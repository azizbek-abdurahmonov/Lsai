﻿using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Application.Common.Documentation.Services;
using Lsai.Application.Common.QASection.Services;
using Lsai.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Lsai.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentationPartsController(
    IDocumentationPartService documentationPartService,
    IQuestionService questionService,
    IMapper mapper) : ControllerBase
{
    [Authorize, HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] DocumentationPartDto documentationPartDto, CancellationToken cancellationToken)
    {
        var result = await documentationPartService.CreateAsync(mapper.Map<DocumentationPart>(documentationPartDto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, mapper.Map<DocumentationPartDto>(result));
    }

    [HttpGet]
    public ValueTask<IActionResult> Get()
    {
        var result = documentationPartService.Get();
        return new(result.Any() ? Ok(mapper.Map<List<DocumentationPartDto>>(result)) : NoContent());
    }

    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await documentationPartService.GetByIdAsync(id, cancellationToken: cancellationToken);
        return result is not null ? Ok(mapper.Map<DocumentationPartDto>(result)) : NotFound();
    }

    [HttpGet("{id:guid}/questions")]
    public ValueTask<IActionResult> GetQuestions([FromRoute] Guid id)
    {
        var result = questionService.GetByDocumentationPartId(id);
        return new(result is not null ? Ok(mapper.Map<List<QuestionDto>>(result)) : NoContent());
    }

    [Authorize, HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] DocumentationPartDto documentationPartDto, CancellationToken cancellationToken)
    {
        var result = await documentationPartService.UpdateAsync(
            mapper.Map<DocumentationPart>(documentationPartDto),
            cancellationToken: cancellationToken);

        return result is not null ? Ok(mapper.Map<DocumentationPartDto>(result)) : BadRequest();
    }

    [Authorize, HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await documentationPartService.DeleteByIdAsync(id, cancellationToken: cancellationToken);
        return result is not null ? Ok(mapper.Map<DocumentationPartDto>(result)) : BadRequest();
    }
}
