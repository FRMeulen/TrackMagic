namespace TrackMagic.Shared.Exceptions.Base
{
    public abstract class AppException : Exception
    {
        public AppException(string title, string message) : base(message) => Title = title;

        public string Title { get; private set; }
    }
}
