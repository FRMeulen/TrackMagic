using MediatR;

namespace TrackMagic.Application.Common.Requests
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}
