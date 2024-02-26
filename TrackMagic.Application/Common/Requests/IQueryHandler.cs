using MediatR;

namespace TrackMagic.Application.Common.Requests
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse> { }
}
