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
public class DocumentationsController(
    IDocumentationService documentationService,
    IDocumentationPartService documentationPartService,
    IDocumentationLikeService documentationLikeService,
    ICommentService commentService,
    IMapper mapper) : ControllerBase
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

        return new(result.Any() ? Ok(mapper.Map<List<DocumentationModelDto>>(result)) : NoContent());
    }

    [HttpGet("{id:guid}/parts")]
    public ValueTask<IActionResult> GetDocumentationParts([FromRoute] Guid id)
    {
        var result = documentationPartService.GetByDocumentationId(id);
        return new(result.Any() ? Ok(mapper.Map<List<DocumentationPartDto>>(result)) : NoContent());
    }

    [HttpGet("{documentationId:guid}/likesCount")]
    public async ValueTask<IActionResult> GetLikesCount([FromRoute] Guid documentationId)
    {
        var result = await documentationLikeService.GetLikeCountByIdAsync(documentationId);

        return Ok(result);
    }

    [HttpGet("{documentationId:guid}/likes")]
    public ValueTask<IActionResult> GetLikes([FromRoute] Guid documentationId, [FromQuery] PaginationOptions paginationOptions)
    {
        var result = documentationLikeService.GetDocumentationLikes(documentationId, paginationOptions);

        return new(result.Any()
            ? Ok(mapper.Map<ICollection<DocumentationLikeDto>>(result))
            : NoContent());
    }

    [HttpGet("{documentationId:guid}/commentsCount")]
    public async ValueTask<IActionResult> GetCommentsCount([FromRoute] Guid documentationId, CancellationToken cancellationToken)
    {
        var result = await commentService.GetCommentsCountByDocumentationIdAsync(documentationId, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("{documentationId:guid}/comments")]
    public ValueTask<IActionResult> GetComments([FromRoute] Guid documentationId, [FromQuery]PaginationOptions paginationOptions)
    {
        var result = commentService.GetCommentsByDocumentationId(documentationId, paginationOptions);

        return new(result.Any()
            ? Ok(mapper.Map<ICollection<CommentDto>>(result))
            : NoContent());
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
