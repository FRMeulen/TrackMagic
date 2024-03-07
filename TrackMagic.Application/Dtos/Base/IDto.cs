namespace TrackMagic.Application.Dtos.Base
{
    public interface IDto
    {
        int Id { get; set; }
    }

    public interface IShallowDto : IDto
    {
        string Name { get; set; }
    }
}
