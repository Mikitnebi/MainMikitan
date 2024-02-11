using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Templates;
using MediatR;
using Newtonsoft.Json;


namespace MainMikitan.Application.Features.Restaurant.Registration.Commands {
    public class RestaurantInfoUpdateCommand(RestaurantInfoRequest request, int restaurantId)
        : ICommand
    {
        public RestaurantInfoRequest RestaurantRegistrationFinalRequest { get; set; } = request;
        public int RestaurantId { get; set; } = restaurantId;
    }
    public class RestaurantInfoUpdateCommandHandler(IMultifunctionalRepository multifunctionalRepository)
        : ResponseMaker, ICommandHandler<RestaurantInfoUpdateCommand>
    {
        public async Task<ResponseModel<bool>> Handle(RestaurantInfoUpdateCommand updateCommand, CancellationToken cancellationToken)
        {
            var registrationRequest = updateCommand.RestaurantRegistrationFinalRequest;
            var restaurantId = updateCommand.RestaurantId;

            var json = JsonConvert.SerializeObject(registrationRequest);
            var restaurantStarterInfoJson = JsonConvert.DeserializeObject<RestaurantInfoEntity>(json);
            restaurantStarterInfoJson!.CreateAt = DateTime.Now;
            restaurantStarterInfoJson!.RestaurantId = restaurantId;
            await multifunctionalRepository.AddOrUpdateTableData(restaurantStarterInfoJson!, "MainMikitan", "dbo", "RestaurantInfo");

            return new ResponseModel<bool> { Result = true };
        }
    }
}
