using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Category.Query;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.InternalServicesAdapter.Validations;
using MediatR;

namespace MainMikitan.Application.Features.Customer.Commands;

public class CreateOrUpdateCustomerInterestCommand : IRequest<ResponseModel<bool>>
{
        public List<int> RequestId { get; set; }
        public int CustomerId { get; set; }
        public CreateOrUpdateCustomerInterestCommand(FillCustomerInterestRequest command, int customerId)
        {
            RequestId = command.InfoIds;
            CustomerId = customerId;
        }
    }

public class
    CreateOrUpdateCustomerInterestCommandHandler : IRequestHandler<CreateOrUpdateCustomerInterestCommand,
        ResponseModel<bool>>
{
    private readonly ICategoryQueryRepository _categoryQueryRepository;
    private readonly ICustomerQueryRepository _customerCategoryQueryRepository;
    private readonly ICustomerInterestRepository _customerInterestRepository;

    public CreateOrUpdateCustomerInterestCommandHandler(
        ICategoryQueryRepository categoryQueryRepository,
        ICustomerQueryRepository customerCategoryQueryRepository,
        ICustomerInterestRepository customerInterestRepository)
    {
        _categoryQueryRepository = categoryQueryRepository;
        _customerCategoryQueryRepository = customerCategoryQueryRepository;
        _customerInterestRepository = customerInterestRepository;
    }

    public async Task<ResponseModel<bool>> Handle(CreateOrUpdateCustomerInterestCommand request,
        CancellationToken cancellationToken)
    {
        var ids = request.RequestId;
        var customerId = request.CustomerId;
        var response = new ResponseModel<bool>();
        var activeIds = await _categoryQueryRepository.GetAllActive(ids);
        var validationResponse = CategoryInfoValidation.Validate(ids, activeIds);
        if (!validationResponse.Result) return validationResponse;
        var deleteResponse = await _customerInterestRepository.Delete(customerId);
        if (!deleteResponse)
        {
            response.ErrorType = ErrorType.CustomerInterest.NotDelete;
            return response;
        }

        var addResponse = await _customerInterestRepository.Add(activeIds, customerId);
        if (!addResponse)
        {
            response.ErrorType = ErrorType.CustomerInterest.NotAdd;
            return response;
        }

        await _customerInterestRepository.SaveChanges();
        response.Result = true;
        return response;
    }
}