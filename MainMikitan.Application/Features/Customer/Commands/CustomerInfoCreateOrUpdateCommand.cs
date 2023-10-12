using MainMikitan.Database.Features.Category.Query;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.InternalServicesAdapter.Validations;
using MediatR;

namespace MainMikitan.Application.Features.Customer.Commands
{
    public class CustomerInfoCreateOrUpdateCommand : IRequest<ResponseModel<bool>>
    {
        public List<int> _RequestId { get; set; }
        public int _CustomerId { get; set; }
        public CustomerInfoCreateOrUpdateCommand(FillCustomerInfoRequest command, int customerId)
        {
            _RequestId = command.InfoIds;
            _CustomerId = customerId;
        }
    }
    public class CustomerInfoCreateOrUpdateCommandHandler : IRequestHandler<CustomerInfoCreateOrUpdateCommand, ResponseModel<bool>>
    {
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        private readonly ICustomerQueryRepository _customerCategoryQueryRepositoy;
        public CustomerInfoCreateOrUpdateCommandHandler(ICategoryQueryRepository categoryQueryRepository, 
            ICustomerQueryRepository customerCategoryQueryRepositoy)
        {
            _categoryQueryRepository = categoryQueryRepository;
            _customerCategoryQueryRepositoy = customerCategoryQueryRepositoy;
        }
        public async Task<ResponseModel<bool>> Handle(CustomerInfoCreateOrUpdateCommand request, CancellationToken cancellationToken)
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
