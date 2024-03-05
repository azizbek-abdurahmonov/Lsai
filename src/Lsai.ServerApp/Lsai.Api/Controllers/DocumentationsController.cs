using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Application.Common.Documentation.Services;
using Lsai.Domain.Common.Constants;
using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lsai.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentationsController(IDocumentationService documentationService, IMapper mapper) : ControllerBase
{
    [HttpPost, Authorize]
    public async ValueTask<IActionResult> Create([FromBody] DocumentationModelDto documentationModelDto, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)!.Value);
        documentationModelDto.UserId = userId;
        var result = await documentationService.CreateAsync(mapper.Map<DocumentationModel>(documentationModelDto), cancellationToken: cancellationToken);

        return CreatedAtAction(nameof(GetById), new { documentationId = result.Id }, mapper.Map<DocumentationModelDto>(result));
    }

    [HttpGet("{documentationId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid documentationId, CancellationToken cancellationToken)
    {
        var result = await documentationService.GetByIdAsync(documentationId, cancellationToken: cancellationToken);

        return result is not null ? Ok(mapper.Map<DocumentationModelDto>(result)) : NotFound();
    }

    [HttpGet]
    public ValueTask<IActionResult> Get([FromQuery] PaginationOptions paginationOptions)
    {
        var result = documentationService.Get(paginationOptions: paginationOptions);

        return new(result.Any() ? Ok(mapper.Map<DocumentationModelDto>(result)) : NoContent());
    }

    [HttpPut, Authorize]
    public async ValueTask<IActionResult> Update([FromBody] DocumentationModelDto documentationModelDto, CancellationToken cancellation)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)!.Value);
        documentationModelDto.UserId = userId;
        var result = await documentationService.UpdateAsync(mapper.Map<DocumentationModel>(documentationModelDto));

        return result is not null ? Ok(mapper.Map<DocumentationModelDto>(result)) : BadRequest();
    }

    [HttpDelete("{documentationId}"), Authorize]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid documentationId, CancellationToken cancellationToken)
    {
        var result = await documentationService.DeleteByIdAsync(documentationId, cancellationToken: cancellationToken);

        return result is not null ? Ok(mapper.Map<DocumentationModelDto>(result)) : BadRequest();
    }
}
