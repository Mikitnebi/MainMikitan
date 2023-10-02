using MainMikitan.Database.Features.Category.Query;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.InternalServicesAdapter.Validations;
using MediatR;
using NPOI.OpenXmlFormats.Dml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public CustomerInfoCreateOrUpdateCommandHandler(ICategoryQueryRepository categoryQueryRepository)
        {
            _categoryQueryRepository = categoryQueryRepository;
        }
        public async Task<ResponseModel<bool>> Handle(CustomerInfoCreateOrUpdateCommand request, CancellationToken cancellationToken)
        {
            var ids = request._RequestId;
            var customerId = request._CustomerId;
            var response = new ResponseModel<bool>();
            var maxIdInCategoryDb = await _categoryQueryRepository.GetMaxIndex();
            var validationResponse = CategoryInfoValidation.Validate(ids, maxIdInCategoryDb);
            if (!validationResponse.Result) return validationResponse;
            //var customerCategoryInfoGetResponse = await _customerCategoryQueryRepositoy.ContainsId(customerId);
            return null;
        }
    }
}
