using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Newtonsoft.Json;


namespace MainMikitan.Application.Features.Restaurant.Registration.Commands {
    public class RestaurantRegistrationFinalCommand : IRequest<ResponseModel<bool>> {
        public RestaurantInfoRequest _restaurantRegistrationFinalRequest { get; set; }
        public int _restaurantId { get; set; }
        public RestaurantRegistrationFinalCommand(RestaurantInfoRequest request, int restaurantId) {
            _restaurantRegistrationFinalRequest = request;
            _restaurantId = restaurantId;
        }
    }
    public class RestaurantRegistrationFinalCommandHandler : IRequestHandler<RestaurantRegistrationFinalCommand, ResponseModel<bool>> {
        private readonly IMultifunctionalRepository _multifunctionalRepository;
        public RestaurantRegistrationFinalCommandHandler(IMultifunctionalRepository multifunctionalRepository) { 
            _multifunctionalRepository = multifunctionalRepository;
        }
        public async Task<ResponseModel<bool>> Handle(RestaurantRegistrationFinalCommand command, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<bool>();
            var registrationRequest = command._restaurantRegistrationFinalRequest;
            var restaurantId = command._restaurantId;

            var json = JsonConvert.SerializeObject(registrationRequest);
            var restaurantStarterInfoJson = JsonConvert.DeserializeObject<RestaurantInfoEntity>(json);
            restaurantStarterInfoJson!.CreateAt = DateTime.Now;
            restaurantStarterInfoJson!.RestaurantId = restaurantId;
            await _multifunctionalRepository.AddOrUpdateTableData(restaurantStarterInfoJson!, "MainMikitan", "dbo", "RestaurantInfo");

            return new ResponseModel<bool> { Result = true };
        }
    }
}
