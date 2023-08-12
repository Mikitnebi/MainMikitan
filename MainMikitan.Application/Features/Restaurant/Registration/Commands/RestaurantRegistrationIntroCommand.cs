using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Application.Features.Customer.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MainMikitan.Common.Validations;
using FluentEmail.Core;

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

        public async Task<ResponseModel<bool>> Handle(RestaurantRegistrationIntroCommand command, CancellationToken cancellationToken) {
            var response = new ResponseModel<bool>();
            var registrtationRequest = command._restaurantRegistrationIntroRequest;
            try {
                var validation = RestaurantIntroRequestsValidation.Registration(registrtationRequest);
                if (validation.HasError) return validation;
                
            } catch (Exception ex) { 
            
            }
            throw new NotImplementedException();
        }
    }
}

    
