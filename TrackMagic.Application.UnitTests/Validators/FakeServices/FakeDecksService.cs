﻿using System.Linq.Expressions;
using TrackMagic.Application.Features.Decks;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Validators.FakeServices
{
    public class FakeDecksService : IDecksService
    {
        /// <summary>
        /// Fake entries representing ColorIdentity entities in the db.
        /// Only the Id and Name are validated against.
        /// </summary>
        private readonly IQueryable<Deck> _fakeDecks = new List<Deck>
        {
            new Deck { Id = 1, Name = "Saprecursion" }
        }.AsQueryable();

        public Task<bool> ExistsAsync(Expression<Func<Deck, bool>> expression, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_fakeDecks.Any(expression));
        }
    }
}
