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
public class CommentsController(
    ICommentService commentService,
    ICommentRepository commentRepository,
    IMapper mapper) : ControllerBase
{
    [Authorize, HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] CommentDto commentDto, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim =>
            claim.Type == ClaimConstants.UserId)!.Value);

        var comment = mapper.Map<Comment>(commentDto);
        comment.UserId = userId;

        var result = await commentService.CreateAsync(
                comment,
                cancellationToken: cancellationToken);

        return result is not null
            ? CreatedAtAction(nameof(GetById), new { commentId = result.Id }, mapper.Map<CommentDto>(result))
            : BadRequest();
    }

    [HttpGet("{commentId:guid}")]
    public async ValueTask<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await commentRepository.GetByIdAsync(id, cancellationToken: cancellationToken);

        return result is not null
            ? Ok(mapper.Map<CommentDto>(result))
            : NotFound();
    }

    [HttpGet]
    public ValueTask<IActionResult> Get([FromQuery] PaginationOptions paginationOptions)
    {
        var result = commentService.Get(paginationOptions);

        return new(result.Any()
            ? Ok(mapper.Map<ICollection<CommentDto>>(result))
            : NoContent());
    }

    [Authorize, HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] CommentDto commentDto, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim =>
           claim.Type == ClaimConstants.UserId)!.Value);

        var comment = mapper.Map<Comment>(commentDto);
        comment.UserId = userId;

        var result = await commentService.UpdateAsync(
                comment,
                cancellationToken: cancellationToken);

        return result is not null
            ? Ok(mapper.Map<CommentDto>(result))
            : BadRequest();
    }


    [Authorize, HttpDelete("{commentId:guid}")]
    public async ValueTask<IActionResult> DeletById(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim =>
            claim.Type == ClaimConstants.UserId)!.Value);

        var result = await commentService.DeletByIdAsync(id, userId, cancellationToken);

        return result is not null
            ? Ok(mapper.Map<CommentDto>(result))
            : BadRequest();
    }
}
