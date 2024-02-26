using MediatR;

namespace TrackMagic.Application.Common.Requests
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse> { }
}
