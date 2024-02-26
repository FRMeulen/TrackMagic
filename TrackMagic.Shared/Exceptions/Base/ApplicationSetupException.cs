namespace TrackMagic.Shared.Exceptions.Base
{
    public abstract class ApplicationSetupException : AppException
    {
        public ApplicationSetupException(string title, string message) : base(title, message) { }
    }
}
