using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TrackMagic.Api.Controllers.Base;
using TrackMagic.Application.Dtos;
using TrackMagic.Application.Features.Decks.Create;
using TrackMagic.Application.Features.Decks.Delete;
using TrackMagic.Application.Features.Decks.Get;
using TrackMagic.Application.Features.Decks.Update;
using TrackMagic.Infrastructure.ExceptionHandling;

namespace TrackMagic.Api.Controllers
{
    public class DeckController : BaseController
    {
        [HttpGet("[action]")]
        [OpenApiOperation("Get a Deck by id.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<DeckDto> GetAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetDeckQuery { Id = id }, cancellationToken);

            return result;
        }

        [HttpPost("[action]")]
        [OpenApiOperation("Create a new Deck.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<int> CreateAsync([FromBody] CreateDeckCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpPut("[action]")]
        [OpenApiOperation("Updates an existing Deck.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<DeckDto> UpdateAsync([FromBody] UpdateDeckCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpDelete("[action]")]
        [OpenApiOperation("Deletes a Deck.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task DeleteAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteDeckCommand { Id = id }, cancellationToken);
        }
    }
}
