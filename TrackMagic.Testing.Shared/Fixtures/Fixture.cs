using System.Text.Json;

namespace TrackMagic.Testing.Shared.Fixtures
{
    public class Fixture<TObject> where TObject : class
    {
        public string File { get; set; } = default!;

        public Fixture()
        {
            File = $"{typeof(TObject).Name}Fixture.json";
        }

        public TObject Create()
        {
            // Does it feel elegant? No.
            // Did I find a better way to cross-use json files? Also no.
            var solutionDir = Directory.GetParent(Directory.GetCurrentDirectory())!
                .Parent!
                .Parent!
                .Parent;
            var jsonDir = $"{solutionDir}/TrackMagic.Testing.Shared/Fixtures/Json/";

            var reader = new StreamReader(jsonDir + File);
            var json = reader.ReadToEnd();
            TObject result = JsonSerializer.Deserialize<TObject>(json)!;
            return result != null
                ? result
                : default!;
        }
    }
}
