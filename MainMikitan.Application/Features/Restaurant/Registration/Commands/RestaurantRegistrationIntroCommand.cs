using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Application.Features.Restaurant.Registration.Commands {
    public class RestaurantRegistrationIntroCommand : IRequest<ResponseModel<bool>> {
        public RestaurantRegistrationIntroRequest _restaurantRegistrationIntroRequest { get; set; }     
        public RestaurantRegistrationIntroCommand(RestaurantRegistrationIntroRequest request) {
            _restaurantRegistrationIntroRequest = request;
        }
    }
    public class RestaurantRegistrationIntroCommandHandler : IRequestHandler<RestaurantRegistrationIntroCommand, ResponseModel<bool>> {
        public RestaurantRegistrationIntroCommandHandler()
        {
            
        }

        public Task<ResponseModel<bool>> Handle(RestaurantRegistrationIntroCommand request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }
}

    
