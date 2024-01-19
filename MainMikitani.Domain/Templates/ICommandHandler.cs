using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Templates;
using MediatR;

namespace MainMikitan.Domain.Templates;

public interface ICommandHandler<in TCommand>
    : IRequestHandler<TCommand, ResponseModel<bool>> where TCommand : ICommand;
public interface ICommandHandler<in TCommand,TResponse> 
    : IRequestHandler<TCommand,ResponseModel<TResponse>> where TCommand : ICommand<TResponse>;