/*using MainMikitan.Database.Features.Restaurant.Command;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Responses.RestaurantResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Application.Features.Restaurant.Registration.Commands {
    public class RestaurantRegistrationFinalCommand : IRequest<ResponseModel<bool>> {
        public RestaurantRegistrationFinalRequest _restaurantRegistrationFinalRequest{ get; set; }
        public RestaurantRegistrationFinalCommand(RestaurantRegistrationFinalRequest request)
        {
            _restaurantRegistrationFinalRequest = request;
        }
    }
    public class RestaurantRegistrationFinalCommandHandler : IRequestHandler<RestaurantRegistrationFinalCommand, ResponseModel<bool>> {
        RestaurantFinalCommandRepository _restaurantFinalCommandRepository;
        public RestaurantRegistrationFinalCommandHandler(RestaurantFinalCommandRepository restaurantFinalCommandRepository) {
            _restaurantFinalCommandRepository = restaurantFinalCommandRepository;
        }
        public async Task<ResponseModel<bool>> Handle(RestaurantRegistrationFinalCommand command, CancellationToken cancellationToken) {
            var response = new ResponseModel<bool>();
            var registrationRequest = command._restaurantRegistrationFinalRequest;
            try {
                var createRestaurantResult = await _restaurantFinalCommandRepository.Create(new RestaurantInfoEntity {
                    UpdateDate = DateTime.Now,
                    Location = registrationRequest.Location,
                    Address = registrationRequest.Address,
                    ManagerId = registrationRequest.ManagerId,
                    ManagerPhone = registrationRequest.ManagerPhone,
                    BusinessTypeId = registrationRequest.BusinessTypeId,
                    CoupeQuantity = registrationRequest.CoupeQuantity,
                    TableQuantity = registrationRequest.TableQuantity,
                    TerrasseQuantity = registrationRequest.TerrasseQuantity,
                    HallStartH = registrationRequest.HallStartH,
                    HallEndH = registrationRequest.HallEndH,
                    KitchenStartH = registrationRequest.KitchenStartH,
                    KitchenEndH = registrationRequest.KitchenEndH,
                    MusicStartH = registrationRequest.MusicStartH,
                    MusicEndH = registrationRequest.MusicEndH,
                    Description = registrationRequest.Description,
                    Active = registrationRequest.Active,
                    ForCorporateEvents = registrationRequest.ForCorporateEvents,
                });
                response.Result = true;
                return response;
            } catch (Exception ex) {
                response.ErrorType = ErrorType.UnExpectedException;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }
    }
}
*/