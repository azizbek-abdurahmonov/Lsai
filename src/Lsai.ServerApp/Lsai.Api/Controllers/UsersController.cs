using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Application.Common.Documentation.Services;
using Lsai.Domain.Common.Constants;
using Lsai.Domain.Common.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lsai.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(
    IDocumentationLikeService documentationLikeService, 
    IMapper mapper) : ControllerBase
{
    [Authorize, HttpGet("likes")]
    public ValueTask<IActionResult> GetLikes([FromQuery] PaginationOptions paginationOptions)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)!.Value);
        var result = documentationLikeService.GetUserLikes(userId, paginationOptions);

        return new(result.Any()
            ? Ok(mapper.Map<ICollection<DocumentationLikeDto>>(result))
            : NoContent());
    }
}
