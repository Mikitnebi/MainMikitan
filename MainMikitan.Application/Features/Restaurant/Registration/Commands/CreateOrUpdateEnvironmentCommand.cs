using Amazon.Runtime.Internal;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Application.Features.Restaurant.Registration.Commands {
    public class CreateOrUpdateEnvironmentCommand : IRequest<ResponseModel<bool>> {
        public List<int> EnvironmentIds { get; set; }
        public int RestaurantId { get; set; }
        public CreateOrUpdateEnvironmentCommand(RestaurantRegistrationEnvironmentRequest request, int restaurantId)
        {
            EnvironmentIds = request.EnvironmentIds;
            RestaurantId = restaurantId;
        }
    }

    public class CreateOrUpdateEnvironmentCommandHandler {
        public CreateOrUpdateEnvironmentCommandHandler()
        {
            
        }
        public async Task<ResponseModel<bool>> Handle(CreateOrUpdateEnvironmentCommand command, CancellationToken cancellationToken) {
            var environmentIds = command.EnvironmentIds;
            var restaurantId = command.RestaurantId;
            var response = new ResponseModel<bool>();

            return null;
        }
    }
}
