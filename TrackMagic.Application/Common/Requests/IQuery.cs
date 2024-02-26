using MediatR;

namespace TrackMagic.Application.Common.Requests
{
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}
