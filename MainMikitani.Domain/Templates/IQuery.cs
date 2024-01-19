using MainMikitan.Domain.Models.Commons;
using MediatR;

namespace MainMikitan.Domain.Templates;

public interface IQuery<TResponse> : IRequest<ResponseModel<TResponse>>;
