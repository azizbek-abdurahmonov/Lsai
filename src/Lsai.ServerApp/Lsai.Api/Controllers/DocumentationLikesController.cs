using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Application.Common.Documentation.Services;
using Lsai.Domain.Common.Constants;
using Lsai.Domain.Common.Filters;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lsai.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentationLikesController(
    IDocumentationLikeRepository documentationLikeRepository,
    IDocumentationLikeService documentationLikeService,
    IMapper mapper) : ControllerBase
{
    [Authorize, HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] DocumentationLikeDto documentationLikeDto, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)!.Value);
        DocumentationLike documentationLike = mapper.Map<DocumentationLike>(documentationLikeDto);
        documentationLike.UserId = userId;

        var result = await documentationLikeService.CreateAsync(
            documentationLike, 
            cancellationToken: cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new {documentationLikeId = result.Id},
            mapper.Map<DocumentationLikeDto>(result));
    }

    [HttpGet("{documentationLikeId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid documentationLikeId, CancellationToken cancellationToken)
    {
        var result = await documentationLikeRepository.GetByIdAsync(documentationLikeId, cancellationToken: cancellationToken);
        
        return result is not null 
            ? Ok(mapper.Map<DocumentationLikeDto>(result))
            : NotFound();
    }

    [HttpGet]
    public ValueTask<IActionResult> Get([FromQuery] PaginationOptions paginationOptions)
    {
        var result = documentationLikeService.Get(paginationOptions);
        
        return new (result.Any()
            ? Ok(mapper.Map<ICollection<DocumentationLikeDto>>(result))
            : NoContent());
    }

    [Authorize, HttpDelete("{likeId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid likeId, CancellationToken cancellationToken)
    {
        var result = await documentationLikeRepository.DeleteByIdAsync(likeId, cancellationToken: cancellationToken);
        return result is not null
            ? Ok(mapper.Map<DocumentationLikeDto>(result))
            : BadRequest();
    }
}
