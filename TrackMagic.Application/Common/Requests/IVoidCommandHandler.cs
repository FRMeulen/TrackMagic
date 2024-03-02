using MediatR;

namespace TrackMagic.Application.Common.Requests
{
    public interface IVoidCommandHandler<TRequest> : IRequestHandler<TRequest>
        where TRequest : IVoidCommand { }
}
