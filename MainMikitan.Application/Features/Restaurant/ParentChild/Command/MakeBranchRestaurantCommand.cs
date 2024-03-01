using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Templates;
using MainMikitan.InternalServicesAdapter.Util;

namespace MainMikitan.Application.Features.Restaurant.ParentChild.Command;

public class MakeBranchRestaurantCommand(int restaurantId) : ICommand<string>
{
    public int RestaurantId { get; set; } = restaurantId;
}

public class MakeBranchRestaurantCommandHandler(
    IRestaurantBranchingCodeLogRepository restaurantBranchingCodeLogRepository)
    : ResponseMaker<string>, ICommandHandler<MakeBranchRestaurantCommand, string>
{
    public async Task<ResponseModel<string>> Handle(MakeBranchRestaurantCommand request, CancellationToken cancellationToken)
    {
        var generateCode = UtilHelper.GenerateCode();
        var addResponse = await restaurantBranchingCodeLogRepository.Create(
            new RestaurantBranchingCodeLogEntity
            {
                Code = generateCode,
                CreatedAt = DateTime.Now, 
                ValidateTime = 3, 
                NumberOfTrials = 3, 
                ParentRestaurantId = request.RestaurantId
            });
        return addResponse > 0 ? Success(generateCode) : Fail(ErrorResponseType.Restaurant.NotUpdated);
    }
}
