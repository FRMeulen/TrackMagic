using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TrackMagic.Api.Controllers.Base;
using TrackMagic.Application.Dtos;
using TrackMagic.Application.Features.Decklists.Create;
using TrackMagic.Application.Features.Decklists.Delete;
using TrackMagic.Application.Features.Decklists.Get;
using TrackMagic.Application.Features.Decklists.Update;
using TrackMagic.Infrastructure.ExceptionHandling;

namespace TrackMagic.Api.Controllers
{
    public class DecklistController : BaseController
    {
        [HttpGet("[action]")]
        [OpenApiOperation("Get a Decklist by id.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<DecklistDto> GetAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetDecklistQuery { Id = id }, cancellationToken);

            return result;
        }

        [HttpPost("[action]")]
        [OpenApiOperation("Create a new Decklist.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<int> CreateAsync([FromBody] CreateDecklistCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpPut("[action]")]
        [OpenApiOperation("Updates an existing Decklist.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<DecklistDto> UpdateAsync([FromBody] UpdateDecklistCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpDelete("[action]")]
        [OpenApiOperation("Deletes a Decklist.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task DeleteAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteDecklistCommand { Id = id }, cancellationToken);
        }
    }
}
