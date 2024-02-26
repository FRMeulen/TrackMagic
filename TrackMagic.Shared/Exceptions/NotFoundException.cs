using System.Net;
using TrackMagic.Shared.Exceptions.Base;

namespace TrackMagic.Shared.Exceptions
{
    public class NotFoundException : RequestException
    {
        public NotFoundException(string title, string message, List<string>? errors = null) : base(title, message, errors, HttpStatusCode.NotFound) { }
    }
}
