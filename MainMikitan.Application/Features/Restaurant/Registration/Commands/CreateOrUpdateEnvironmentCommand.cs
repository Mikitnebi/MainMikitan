using Amazon.Runtime.Internal;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Templates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Application.Features.Restaurant.Registration.Commands {
    public class CreateOrUpdateEnvironmentCommand : ICommand {
        public List<int> EnvironmentIds { get; set; }
        public int RestaurantId { get; set; }                      
        public CreateOrUpdateEnvironmentCommand(RestaurantRegistrationEnvironmentRequest request, int restaurantId)
        {
            EnvironmentIds = request.EnvironmentIds;
            RestaurantId = restaurantId;
        }
    }

    public class CreateOrUpdateEnvironmentCommandHandler : ICommandHandler<CreateOrUpdateEnvironmentCommand> {
        private readonly IRestaurantEnvironmentRepository _restaurantEnvironmentRepository;
        public CreateOrUpdateEnvironmentCommandHandler(IRestaurantEnvironmentRepository restaurantEnvironmentRepository)
        {
            _restaurantEnvironmentRepository = restaurantEnvironmentRepository;            
        }
        public async Task<ResponseModel<bool>> Handle(CreateOrUpdateEnvironmentCommand command, CancellationToken cancellationToken) {
            var environmentIds = command.EnvironmentIds;
            var restaurantId = command.RestaurantId;
            var response = new ResponseModel<bool>();
            try {
                var deleteResponse = await _restaurantEnvironmentRepository.Delete(restaurantId);
                if (!deleteResponse) {
                    response.ErrorType = ErrorType.RestaurantEnvironment.NotDeleted;
                    return response;
                }
                var addResponse = await _restaurantEnvironmentRepository.Add(environmentIds, restaurantId);
                if (!addResponse) {
                    response.ErrorType = ErrorType.RestaurantEnvironment.NotAdded;
                    return response;
                }
                if (!await _restaurantEnvironmentRepository.SaveChanges()) {
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
