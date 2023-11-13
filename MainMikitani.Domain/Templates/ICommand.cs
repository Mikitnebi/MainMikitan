using MainMikitan.Domain.Models.Commons;
using MediatR;

namespace MainMikitan.Domain.Templates;

public interface ICommand : IRequest<ResponseModel<bool>>
{
    
}