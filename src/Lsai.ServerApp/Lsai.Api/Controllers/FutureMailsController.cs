using AutoMapper;
using Lsai.Api.Dtos;
using Lsai.Application.Common.Notification.Services;
using Lsai.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Lsai.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FutureMailsController(IFutureMailService futureMailService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] FutureMailDto futureMailDto, CancellationToken cancellationToken)
    {
        var result = await futureMailService.CreateAsync(mapper.Map<FutureMail>(futureMailDto), cancellationToken: cancellationToken);
        return result is not null ? Ok(mapper.Map<FutureMailDto>(result)) : BadRequest();
    }
}
