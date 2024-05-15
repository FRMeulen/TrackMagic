using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TrackMagic.Api.Controllers.Base;
using TrackMagic.Application.Common.Searching;
using TrackMagic.Application.Dtos;
using TrackMagic.Application.Features.Players.Create;
using TrackMagic.Application.Features.Players.Delete;
using TrackMagic.Application.Features.Players.Get;
using TrackMagic.Application.Features.Players.Search;
using TrackMagic.Application.Features.Players.Update;
using TrackMagic.Infrastructure.ExceptionHandling;

namespace TrackMagic.Api.Controllers
{
    public class PlayerController : BaseController
    {
        [HttpGet("[action]")]
        [OpenApiOperation("Get a Player by id.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<PlayerDto> GetAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetPlayerQuery { Id = id }, cancellationToken);

            return result;
        }

        [HttpGet("[action]")]
        [OpenApiOperation("Search a Player by criteria.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<SearchResult<PlayerDto>> SearchAsync([FromQuery] SearchPlayerQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);

            return result;
        }

        [HttpPost("[action]")]
        [OpenApiOperation("Create a new Player.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<int> CreateAsync([FromBody] CreatePlayerCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpPut("[action]")]
        [OpenApiOperation("Updates an existing Player.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<PlayerDto> UpdateAsync([FromBody] UpdatePlayerCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpDelete("[action]")]
        [OpenApiOperation("Deletes a Player.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task DeleteAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeletePlayerCommand { Id = id }, cancellationToken);
        }
    }
}
