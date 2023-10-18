using MainMikitan.Database.Features.Category.Query;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.InternalServicesAdapter.Validations;
using MediatR;

namespace MainMikitan.Application.Features.Customer.Commands
{
    public class CustomerInterestCreateOrUpdateCommand : IRequest<ResponseModel<bool>>
    {
        public List<int> _RequestId { get; set; }
        public int _CustomerId { get; set; }
        public CustomerInterestCreateOrUpdateCommand(FillCustomerInterestRequest command, int customerId)
        {
            _RequestId = command.InfoIds;
            _CustomerId = customerId;
        }
    }
    public class CustomerInterestCreateOrUpdateCommandHandler : IRequestHandler<CustomerInterestCreateOrUpdateCommand, ResponseModel<bool>>
    {
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        private readonly ICustomerQueryRepository _customerCategoryQueryRepository;
        public CustomerInterestCreateOrUpdateCommandHandler(ICategoryQueryRepository categoryQueryRepository, 
            ICustomerQueryRepository customerCategoryQueryRepository)
        {
            _categoryQueryRepository = categoryQueryRepository;
            _customerCategoryQueryRepository = customerCategoryQueryRepository;
        }
        public async Task<ResponseModel<bool>> Handle(CustomerInterestCreateOrUpdateCommand request, CancellationToken cancellationToken)
        {
            var ids = request._RequestId;
            var customerId = request._CustomerId;
            var response = new ResponseModel<bool>();
            var activeIds = await _categoryQueryRepository.GetAllActive(ids);
            var validationResponse = CategoryInfoValidation.Validate(ids, activeIds);
            if (!validationResponse.Result) return validationResponse;
            // var customerCategoryInfoGetResponse = await _customerCategoryQueryRepositoy.AddInterestTagId(customerId);
            return null;
        }
    }
}
