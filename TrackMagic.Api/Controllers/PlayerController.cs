﻿using Microsoft.AspNetCore.Mvc;
using TrackMagic.Api.Controllers.Base;
using TrackMagic.Application.Dtos;
using TrackMagic.Application.Features.Players.Get;

namespace TrackMagic.Api.Controllers
{
    public class PlayerController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<PlayerDto> GetPlayerAsync(int id)
        {
            var result = await Mediator.Send(new GetPlayerQuery { Id = id });

            return result;
        }
    }
}
