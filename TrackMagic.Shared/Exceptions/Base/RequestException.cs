using System.Net;

namespace TrackMagic.Shared.Exceptions.Base
{
    public class RequestException : ApplicationRuntimeException
    {
        public List<string>? ErrorMessages { get; }
        public HttpStatusCode StatusCode { get; }

        public RequestException(
            string title,
            string message,
            List<string>? errorMessages = default,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(title, message)
        {
            ErrorMessages = errorMessages;
            StatusCode = statusCode;
        }
    }
}
