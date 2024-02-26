namespace TrackMagic.Shared.Exceptions.Base
{
    public abstract class ApplicationRuntimeException : AppException
    {
        public ApplicationRuntimeException(string title, string message) : base(title, message) { }
    }
}
