using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TrackMagic.Api.Controllers.Base;
using TrackMagic.Application.Dtos;
using TrackMagic.Application.Features.Games.Get;
using TrackMagic.Application.Features.Games.Create;
using TrackMagic.Application.Features.Games.Delete;
using TrackMagic.Application.Features.Games.Update;
using TrackMagic.Infrastructure.ExceptionHandling;

namespace TrackMagic.Api.Controllers
{
    public class GameController : BaseController
    {
        [HttpGet("[action]")]
        [OpenApiOperation("Get a Game by id.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<GameDto> GetAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetGameQuery { Id = id }, cancellationToken);

            return result;
        }

        [HttpPost("[action]")]
        [OpenApiOperation("Create a new Game.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<int> CreateAsync([FromBody] CreateGameCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpPut("[action]")]
        [OpenApiOperation("Updates an existing Game.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<GameDto> UpdateAsync([FromBody] UpdateGameCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpDelete("[action]")]
        [OpenApiOperation("Deletes a Game.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task DeleteAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteGameCommand { Id = id }, cancellationToken);
        }
    }
}
