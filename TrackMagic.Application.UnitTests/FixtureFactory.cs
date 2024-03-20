using System.Text.Json;

namespace TrackMagic.Application.UnitTests
{
    public class FixtureFactory
    {
        public string Directory { get; set; } = default!;

        public FixtureFactory(string directory)
        {
            Directory = directory;
        }

        public TObject Create<TObject>(string? fileName = null)
        {
            fileName ??= $"{typeof(TObject).Name}Fixture.json";
            var reader = new StreamReader(Directory + fileName);
            var json = reader.ReadToEnd();
            TObject result = JsonSerializer.Deserialize<TObject>(json)!;
            return result != null
                ? result
                : default!;
        }
    }
}
