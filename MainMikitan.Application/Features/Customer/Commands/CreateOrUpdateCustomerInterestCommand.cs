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
    ICategoryQueryRepository categoryQueryRepository,
    ICustomerInterestRepository customerInterestRepository)
    : ResponseMaker, ICommandHandler<CreateOrUpdateCustomerInterestCommand>
{
    public async Task<ResponseModel<bool>> Handle(CreateOrUpdateCustomerInterestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var customerCurrentInterests = await customerInterestRepository.Get(request.CustomerId, cancellationToken);
            if (customerCurrentInterests.Count != 0)
            {
                var deleteResponse = await customerInterestRepository.Delete(request.CustomerId, cancellationToken);
                if (!deleteResponse)
                    return Fail(ErrorType.CustomerInterest.NotDelete);
            }

            var addResponse = await customerInterestRepository.Add(request.InterestsTypesIds, request.CustomerId, cancellationToken);
            if (!addResponse)
                return Fail(ErrorType.CustomerInterest.NotAdd);
            if (!(await customerInterestRepository.SaveChanges(cancellationToken)))
                return Fail(ErrorType.CustomerInterest.NotDbSave);
            return Success();
        }
        catch (Exception ex)
        {
            return Unexpected(ex);
        }
    }
}