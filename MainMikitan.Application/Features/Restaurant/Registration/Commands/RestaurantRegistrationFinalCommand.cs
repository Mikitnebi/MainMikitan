using MainMikitan.Database.Features.Restaurant.Command;
using MainMikitan.Database.Features.Restaurant.Interface;
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
        public RestaurantRegistrationFinalRequest _restaurantRegistrationFinalRequest { get; set; }
        public RestaurantRegistrationFinalCommand(RestaurantRegistrationFinalRequest request) {
            _restaurantRegistrationFinalRequest = request;
        }
    }
    public class RestaurantRegistrationFinalCommandHandler : IRequestHandler<RestaurantRegistrationFinalCommand, ResponseModel<bool>> {
        IRestaurantFinalCommandRepository _restaurantFinalCommandRepository;
        public RestaurantRegistrationFinalCommandHandler(IRestaurantFinalCommandRepository restaurantFinalCommandRepository) {
            _restaurantFinalCommandRepository = restaurantFinalCommandRepository;
        }
        public async Task<ResponseModel<bool>> Handle(RestaurantRegistrationFinalCommand command, CancellationToken cancellationToken) {
            var response = new ResponseModel<bool>();
            var registrationRequest = command._restaurantRegistrationFinalRequest;
            try {
                var createRestaurantResult = await _restaurantFinalCommandRepository.Create(new RestaurantInfoEntity {
                    LocationX = registrationRequest.LocationX,
                    LocationY = registrationRequest.LocationY,
                    Address = registrationRequest.Address,
                    BusinessTypeId = registrationRequest.BusinessTypeId,
                    CoupeQuantity = registrationRequest.CoupeQuantity,
                    TableQuantity = registrationRequest.TableQuantity,
                    TerraceQuantity = registrationRequest.TerraceQuantity,
                    HallStartHour = registrationRequest.HallStartHour,
                    HallEndHour = registrationRequest.HallEndHour,
                    HallStartMinute = registrationRequest.HallStartMinute,
                    HallEndMinute = registrationRequest.HallEndMinute,
                    KitchenStartHour = registrationRequest.KitchenStartHour,
                    KitchenEndHour = registrationRequest.KitchenEndHour,
                    KitchenStartMinute = registrationRequest.KitchenStartMinute,
                    KitchenEndMinute = registrationRequest.KitchenEndMinute,
                    MusicStartHour = registrationRequest.MusicStartHour,
                    MusicEndHour = registrationRequest.MusicEndHour,
                    MusicStartMinute = registrationRequest.MusicStartMinute,
                    MusicEndMinute = registrationRequest.MusicEndMinute,
                    ForCorporateEvents = registrationRequest.ForCorporateEvents,
                    Description = registrationRequest.Description,
                    ActiveStatusId = registrationRequest.ActiveStatusId,
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
