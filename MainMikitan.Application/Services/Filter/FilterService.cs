using MainMikitan.Application.Services.AutoMapper;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Database.Features.ListOfValue.Intefaces;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.ListOfValue;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Responses.Filter;
using Microsoft.IdentityModel.Tokens;

namespace MainMikitan.Application.Services.Filter;

public class FilterService(
    IRestaurantEnvQueryRepository restaurantEnvQueryRepository,
    IRestaurantInfoQueryRepository restaurantInfoQueryRepository,
    ICustomerInterestQueryRepository customerInterestQueryRepository,
    IListOfValueQueryRepository listOfValueQueryRepository,
    IMapperConfig mapperConfig
    ) : IFilterService
{
    public async Task<List<RestaurantInfoResponse>> Search(string ipAddress, int customerId, int size, int page = 1, List<int>? customerInterests = null,
        int? regionId = null, int? businessTypeId = null, bool? hasCoupe = null, bool? hasTerrace = null,
        int? isActiveInSomeHour = null, int? isActiveKitchenInSomeHour = null, int? isActiveMusicInSomeHour = null,
        int? priceTypeId = null, int? rate = null, int? priorityTypeId = null, CancellationToken cancellationToken = default)

    {
        regionId ??= 5;
        var restaurantsInfos = await restaurantInfoQueryRepository.GetRestaurantsByCustom(
            (int)regionId, businessTypeId, hasCoupe, hasTerrace,
            isActiveInSomeHour, isActiveKitchenInSomeHour, isActiveMusicInSomeHour, 
            priceTypeId, rate, cancellationToken);
        var restaurantInfoResponse = new List<RestaurantInfoResponse>();
        customerInterests ??= await customerInterestQueryRepository.GetInterestIdsByCustomerId(customerId, cancellationToken);
        if (!customerInterests.IsNullOrEmpty())
        {
            var interests = await listOfValueQueryRepository.GetDictionariesByIds(customerInterests, cancellationToken);
            restaurantsInfos = await SearchByEnvironment(restaurantsInfos, interests, priorityTypeId, cancellationToken);
        }
        //შევინახოთ ქეშში ყოველ ჯერზე რომ არ ირბინოს restaurantsInfos
        //ყველაფერი გადავაკეთოთ აიენუმებერლად და არა ლისტის
        restaurantsInfos = restaurantsInfos.Skip((page - 1) * size).Take(size).ToList();
        mapperConfig.Map(restaurantsInfos, restaurantInfoResponse);
        return restaurantInfoResponse;
    }

    private async Task<List<RestaurantInfoEntity>> SearchByEnvironment(List<RestaurantInfoEntity> restaurantInfo, List<DictionaryEntity> customerInterests, int? priorityTypeId = 0, CancellationToken cancellationToken = default)
    {
        Dictionary<int, RestaurantInfoEntity> restaurantSearchScore = new(restaurantInfo.Count);
        foreach (var restaurant in restaurantInfo)
        {
            var score = 0;
            var restaurantEnvironments = await restaurantEnvQueryRepository.Get(restaurant.RestaurantId, cancellationToken);
            foreach (var customerInterest in customerInterests)
            {
                var sameInterest = restaurantEnvironments.FirstOrDefault(t => t.EnvironmentId == customerInterest.Id);
                if (sameInterest is not null)
                    score += (customerInterest.SectorId == priorityTypeId ? 3 : 1);
            }
            restaurantSearchScore.Add(score, restaurant);
        }
        return restaurantSearchScore
            .OrderByDescending(t => t.Key)
            .Select(r => r.Value).ToList();
        
    }
    /*private static async Task<List<RestaurantInfoEntity>> GetRestaurantsByPaging(IQueryable<RestaurantInfoEntity> query,
        int page, int size, CancellationToken cancellationToken = default)
    {
        return await query
            .Skip((page - 1) * size)
            .Take(size)
            .OrderByDescending(t => t.CreateAt)
            .ToListAsync(cancellationToken);
    }*/
}