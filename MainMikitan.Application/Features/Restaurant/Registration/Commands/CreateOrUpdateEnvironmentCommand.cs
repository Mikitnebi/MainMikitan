using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Restaurant.Registration.Commands {
    public class CreateOrUpdateEnvironmentCommand(RestaurantRegistrationEnvironmentRequest request, int restaurantId)
        : ICommand
    {
        public List<int> EnvironmentIds { get; set; } = request.EnvironmentIds;
        public int RestaurantId { get; set; } = restaurantId;
    }

    public class CreateOrUpdateEnvironmentCommandHandler(
        IRestaurantEnvCommandRepository restaurantEnvCommandRepository)
        : ICommandHandler<CreateOrUpdateEnvironmentCommand>
    {
        public async Task<ResponseModel<bool>> Handle(CreateOrUpdateEnvironmentCommand command, CancellationToken cancellationToken) {
            var environmentIds = command.EnvironmentIds;
            var restaurantId = command.RestaurantId;
            var response = new ResponseModel<bool>();
            try {
                var deleteResponse = await restaurantEnvCommandRepository.Delete(restaurantId);
                if (!deleteResponse) {
                    response.ErrorType = ErrorType.RestaurantEnvironment.NotDeleted;
                    return response;
                }
                var addResponse = await restaurantEnvCommandRepository.Add(environmentIds, restaurantId);
                if (!addResponse) {
                    response.ErrorType = ErrorType.RestaurantEnvironment.NotAdded;
                    return response;
                }
                if (!await restaurantEnvCommandRepository.SaveChanges()) {
                    response.ErrorType = ErrorType.RestaurantEnvironment.NotDbSaved;
                    return response;
                }
            } catch (Exception ex) {
                response.ErrorType = ErrorType.UnExpectedException;
                response.ErrorMessage = ex.Message;
                return response;
            }
            response.Result = true;
            return response;
        }
    }
}
