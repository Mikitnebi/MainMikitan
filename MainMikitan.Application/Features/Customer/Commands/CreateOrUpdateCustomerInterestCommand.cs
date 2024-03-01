using MainMikitan.Database.Features.Category.Query;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Templates;
using MainMikitan.InternalServicesAdapter.Validations;

namespace MainMikitan.Application.Features.Customer.Commands;

public class CreateOrUpdateCustomerInterestCommand : ICommand
{
    public int CustomerId { get; }
    public List<int> InterestsTypesIds { get; set; } = [];

    public CreateOrUpdateCustomerInterestCommand(FillCustomerInterestRequest command, int customerId)
    {
        CustomerId = customerId;
        InterestsTypesIds.AddRange(command.EnvironmentTypeIds);
        InterestsTypesIds.AddRange(command.CousinsTypeIds);
        InterestsTypesIds.AddRange(command.MusicsTypeIds);
    }
}

public class CreateOrUpdateCustomerInterestCommandHandler(
    ICustomerInterestCommandRepository customerInterestCommandRepository,
    ICustomerInterestQueryRepository customerInterestQueryRepository)
    : ResponseMaker, ICommandHandler<CreateOrUpdateCustomerInterestCommand>
{
    public async Task<ResponseModel<bool>> Handle(CreateOrUpdateCustomerInterestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var customerCurrentInterests = await customerInterestQueryRepository.GetByCustomerId(request.CustomerId, cancellationToken);
            if (customerCurrentInterests.Count != 0)
            {
                var deleteResponse = await customerInterestCommandRepository.Delete(request.CustomerId, cancellationToken);
                if (!deleteResponse)
                    return Fail(ErrorResponseType.CustomerInterest.NotDelete);
            }

            var addResponse = await customerInterestCommandRepository.Add(request.InterestsTypesIds, request.CustomerId, cancellationToken);
            if (!addResponse)
                return Fail(ErrorResponseType.CustomerInterest.NotAdd);
            if (!(await customerInterestCommandRepository.SaveChanges(cancellationToken)))
                return Fail(ErrorResponseType.CustomerInterest.NotDbSave);
            return Success();
        }
        catch (Exception ex)
        {
            return Unexpected(ex);
        }
    }
}