using MainMikitan.Database.Features.Category.Query;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Templates;
using MainMikitan.InternalServicesAdapter.Validations;

namespace MainMikitan.Application.Features.Customer.Commands;

public class CreateOrUpdateCustomerInterestCommand(FillCustomerInterestRequest command, int customerId) : ICommand
{
        public List<int> RequestId { get; set; } = command.InfoIds;
        public int CustomerId { get; set; } = customerId;
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
            var activeIds = await categoryQueryRepository.GetAllActive(request.RequestId);
            var validationResponse = CategoryInfoValidation.Validate(request.RequestId, activeIds);
            if (!validationResponse.Result) return validationResponse;
            var deleteResponse = await customerInterestRepository.Delete(request.CustomerId);
            if (!deleteResponse)
                return Fail(ErrorType.CustomerInterest.NotDelete);
            var addResponse = await customerInterestRepository.Add(activeIds, request.CustomerId);
            if (!addResponse)
                return Fail(ErrorType.CustomerInterest.NotAdd);
            if (await customerInterestRepository.SaveChanges())
                return Fail(ErrorType.CustomerInterest.NotDbSave);
            return Success();
        }
        catch (Exception ex)
        {
            return Unexpected(ex.Message);
        }
    }
}