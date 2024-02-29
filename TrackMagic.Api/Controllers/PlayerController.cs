using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TrackMagic.Api.Controllers.Base;
using TrackMagic.Application.Dtos;
using TrackMagic.Application.Features.Players.Get;
using TrackMagic.Infrastructure.ExceptionHandling;

namespace TrackMagic.Api.Controllers
{
    public class PlayerController : BaseController
    {
        [HttpGet]
        [OpenApiOperation("Get a Player by id.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<PlayerDto> GetAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetPlayerQuery { Id = id }, cancellationToken);

            return result;
        }
    }
}
