using System.Linq.Expressions;
using TrackMagic.Application.Features.Cards;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.UnitTests.Validators.FakeServices
{
    public class FakeCardsService : ICardsService
    {
        /// <summary>
        /// Fake entries representing Card entities in the db.
        /// Only Id, Name, and CardTypes are validated against.
        /// </summary>
        private readonly IQueryable<Card> _fakeCards = new List<Card>
        {
            new Card { Id = 1, Name = "Portal to Phyrexia", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 2, Name = "Invasion of Segovia", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 3, Name = "Scute Swarm", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 4, Name = "Sterling Grove", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 5, Name = "Lightning Helix", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 6, Name = "Command Tower", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 7, Name = "Chandra, Torch of Defiance", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 8, Name = "Time Wipe", CardTypes = new List<CardTypes> { CardTypes.Sorcery } },
            new Card { Id = 9, Name = "Azorius Signet", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 10, Name = "Dimir Signet", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 11, Name = "Rakdos Signet", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 12, Name = "Gruul Signet", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 13, Name = "Selesnya Signet", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 14, Name = "Orzhov Signet", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 15, Name = "Izzet Signet", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 16, Name = "Golgari Signet", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 17, Name = "Boros Signet", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 18, Name = "Simic Signet", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 19, Name = "Invasion of Amonkhet", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 20, Name = "Invasion of Theros", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 21, Name = "Invasion of Innistrad", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 22, Name = "Invasion of Fiora", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 23, Name = "Invasion of Ergamon", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 24, Name = "Invasion of Ixalan", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 25, Name = "Invasion of New Phyrexia", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 26, Name = "Invasion of Alara", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 27, Name = "Invasion of Zendikar", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 28, Name = "Invasion of Ikoria", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 29, Name = "Phyrexian Obliterator", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 30, Name = "Monastery Swiftspear", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 31, Name = "Third Path Iconoclast", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 32, Name = "Tireless Tracker", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 33, Name = "Gravecrawler", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 34, Name = "Etali, Primal Conqueror", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 35, Name = "Atraxa, Grand Unifier", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 36, Name = "Elvish Mystic", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 37, Name = "Vorinclex, Voice of Hunger", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 38, Name = "Ledger Shredder", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 39, Name = "Unnatural Growth", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 40, Name = "Anointed Procession", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 41, Name = "Virtue of Persistence", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 42, Name = "Planar Disruption", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 43, Name = "Curse of Surveillance", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 44, Name = "Ossification", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 45, Name = "Buried in the Garden", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 46, Name = "Teferi's Ageless Insight", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 47, Name = "Oath of Kaya", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 48, Name = "Mechanized Warfare", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 49, Name = "Serum Snare", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 50, Name = "Cut Down", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 51, Name = "Path to Exile", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 52, Name = "Heroic Intervention", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 53, Name = "Negate", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 54, Name = "Village Rites", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 55, Name = "Brought Back", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 56, Name = "Not Dead After All", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 57, Name = "Teferi's Protection", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 58, Name = "Cyclonic Rift", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 59, Name = "Overgrown Farmland", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 60, Name = "Ketria Triome", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 61, Name = "Command Tower", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 62, Name = "Swamp", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 63, Name = "Snow-Covered Forest", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 64, Name = "Fabled Passage", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 65, Name = "Scalding Tarn", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 66, Name = "Ziatora's Proving Grounds", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 67, Name = "Mirrex", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 68, Name = "Sundown Pass", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 69, Name = "Chandra, Hope's Beacon", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 70, Name = "Elspeth, Sun's Champion", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 71, Name = "Nissa, Who Shakes the World", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 72, Name = "Jace, Mirror Mage", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 73, Name = "Kaya, Intangible Slayer", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 74, Name = "Ajani, Inspiring Leader", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 75, Name = "Aminatou, the Fateshifter", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 76, Name = "Ashiok, Dream Render", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 77, Name = "Domri, Anarch of Bolas", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 78, Name = "Nicol Bolas, Dragon-God", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 79, Name = "Abundant Harvest", CardTypes = new List<CardTypes> { CardTypes.Sorcery } },
            new Card { Id = 80, Name = "Aether Helix", CardTypes = new List<CardTypes> { CardTypes.Sorcery } },
            new Card { Id = 81, Name = "Faithless Looting", CardTypes = new List<CardTypes> { CardTypes.Sorcery } },
            new Card { Id = 82, Name = "All Is Dust", CardTypes = new List<CardTypes> { CardTypes.Sorcery } },
            new Card { Id = 83, Name = "Armageddon", CardTypes = new List<CardTypes> { CardTypes.Sorcery } },
            new Card { Id = 84, Name = "Banishing Slash", CardTypes = new List<CardTypes> { CardTypes.Sorcery } },
            new Card { Id = 85, Name = "The Ring Goes South", CardTypes = new List<CardTypes> { CardTypes.Sorcery } },
            new Card { Id = 86, Name = "Kodama's Reach", CardTypes = new List<CardTypes> { CardTypes.Sorcery } },
            new Card { Id = 87, Name = "Blasphemous Act", CardTypes = new List<CardTypes> { CardTypes.Sorcery } },
            new Card { Id = 88, Name = "Rise of the Dark Realms", CardTypes = new List<CardTypes> { CardTypes.Sorcery } },
            new Card { Id = 89, Name = "Esper Sentinel", CardTypes = new List<CardTypes> { CardTypes.Artifact, CardTypes.Creature } },
            new Card { Id = 90, Name = "Steel Overseer", CardTypes = new List<CardTypes> { CardTypes.Artifact, CardTypes.Creature } },
            new Card { Id = 91, Name = "Baleful Strix", CardTypes = new List<CardTypes> { CardTypes.Artifact, CardTypes.Creature } },
            new Card { Id = 92, Name = "Darksteel Colossus", CardTypes = new List<CardTypes> { CardTypes.Artifact, CardTypes.Creature } },
            new Card { Id = 93, Name = "Phyrexian Metamorph", CardTypes = new List<CardTypes> { CardTypes.Artifact, CardTypes.Creature } },
            new Card { Id = 94, Name = "Noxious Gearhulk", CardTypes = new List<CardTypes> { CardTypes.Artifact, CardTypes.Creature } },
            new Card { Id = 95, Name = "Jukai Naturalist", CardTypes = new List<CardTypes> { CardTypes.Creature, CardTypes.Enchantment } },
            new Card { Id = 96, Name = "Thassa, Deep-Dwelling", CardTypes = new List<CardTypes> { CardTypes.Creature, CardTypes.Enchantment } },
            new Card { Id = 97, Name = "Erebos, God of the Dead", CardTypes = new List<CardTypes> { CardTypes.Creature, CardTypes.Enchantment } },
            new Card { Id = 98, Name = "Iroas, God of Victory", CardTypes = new List<CardTypes> { CardTypes.Creature, CardTypes.Enchantment } },
            new Card { Id = 99, Name = "Spirited Companion", CardTypes = new List<CardTypes> { CardTypes.Creature, CardTypes.Enchantment } },
            new Card { Id = 100, Name = "Demon of Fate's Design", CardTypes = new List<CardTypes> { CardTypes.Creature, CardTypes.Enchantment } }
        }.AsQueryable();

        public Task<bool> ExistsAsync(Expression<Func<Card, bool>> expression, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_fakeCards.Any(expression));
        }
    }
}
