﻿using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TrackMagic.Api.Controllers.Base;
using TrackMagic.Application.Dtos;
using TrackMagic.Application.Features.Players.Get;
using TrackMagic.Infrastructure.Middleware;

namespace TrackMagic.Api.Controllers
{
    public class PlayerController : BaseController
    {
        [HttpGet("{id:int}")]
        [OpenApiOperation("Get a Player by id.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ErrorResult))]
        public async Task<PlayerDto> GetAsync(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetPlayerQuery { Id = id }, cancellationToken);

            return result;
        }
    }
}
