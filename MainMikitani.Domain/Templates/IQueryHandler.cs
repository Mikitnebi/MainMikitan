using MainMikitan.Domain.Models.Commons;
using MediatR;

namespace MainMikitan.Domain.Templates;

public interface IQueryHandler<in TQuery,TResponse> 
    : IRequestHandler<TQuery,ResponseModel<TResponse>> where TQuery : IQuery<TResponse>
{
}