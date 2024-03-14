using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TrackMagic.Api.Controllers.Base;
using TrackMagic.Application.Dtos;
using TrackMagic.Application.Features.ColorIdentities.Create;
using TrackMagic.Application.Features.ColorIdentities.Delete;
using TrackMagic.Application.Features.ColorIdentities.Get;
using TrackMagic.Application.Features.ColorIdentities.Update;
using TrackMagic.Infrastructure.ExceptionHandling;

namespace TrackMagic.Api.Controllers
{
    public class ColorIdentityController : BaseController
    {
        [HttpGet("[action]")]
        [OpenApiOperation("Get a ColorIdentity by id.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<ColorIdentityDto> GetAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetColorIdentityQuery { Id = id }, cancellationToken);

            return result;
        }

        [HttpPost("[action]")]
        [OpenApiOperation("Create a new ColorIdentity.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<int> CreateAsync([FromBody] CreateColorIdentityCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpPut("[action]")]
        [OpenApiOperation("Updates an existing ColorIdentity.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<ColorIdentityDto> UpdateAsync([FromBody] UpdateColorIdentityCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        [HttpDelete("[action]")]
        [OpenApiOperation("Deletes a ColorIdentity.", "")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task DeleteAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteColorIdentityCommand { Id = id }, cancellationToken);
        }
    }
}
