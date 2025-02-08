using MediatR;
using PersonDirectory.Domain.Abstractions;

namespace PersonDirectory.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
