using AutoMapper;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Events;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Restaurant.TableManagement;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Requests.RestaurantRequests.Event;
using MainMikitan.Domain.Requests.TableRequests;
using MainMikitan.Domain.Responses.Filter;
using MainMikitan.Domain.Responses.RestaurantResponses;

namespace MainMikitan.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RestaurantInfoRequest, RestaurantInfoEntity>();
        CreateMap<RestaurantInfoEntity, RestaurantInfoRequest>();
        
        CreateMap<RestaurantInfoModel, RestaurantInfoEntity>();
        CreateMap<RestaurantInfoEntity, RestaurantInfoModel>();

        CreateMap<TableInfoEntity, AddTableRequest>();
        CreateMap<AddTableRequest, TableInfoEntity>();
        
        CreateMap<CreateOrUpdateEventRequest, EventEntity>();
        CreateMap<EventEntity, CreateOrUpdateEventRequest>();
    }
}