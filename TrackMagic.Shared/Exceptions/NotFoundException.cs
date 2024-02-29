using System.Net;
using TrackMagic.Shared.Constants;
using TrackMagic.Shared.Exceptions.Base;

namespace TrackMagic.Shared.Exceptions
{
    public class NotFoundException : RequestException
    {
        public NotFoundException(string entityType, List<string>? errors = null)
            : base(DefaultMessages.NotFoundExceptionTitle,
                  DefaultMessages.NotFoundExceptionMessage(entityType),
                  errors, HttpStatusCode.NotFound) { }
    }
}
