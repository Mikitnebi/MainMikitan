using AutoMapper;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Comments;
using MainMikitan.Domain.Models.Events;
using MainMikitan.Domain.Models.Rating;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Restaurant.TableManagement;
using MainMikitan.Domain.Requests.Comment;
using MainMikitan.Domain.Requests.Rating;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Requests.RestaurantRequests.Event;
using MainMikitan.Domain.Requests.TableRequests;
using MainMikitan.Domain.Responses.Event;
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
        
        CreateMap<CreateOrUpdateEventDetailsRequest, EventDetailsEntity>();
        CreateMap<EventDetailsEntity, CreateOrUpdateEventDetailsRequest>();

        CreateMap<EventEntity, GetEventInfoResponse>();
        CreateMap<GetEventInfoResponse, EventEntity>();
        CreateMap<EventDetailsEntity, GetEventInfoResponse>();
        CreateMap<GetEventInfoResponse, EventDetailsEntity>();
        
        CreateMap<CreateRestaurantCommentRequest, RestaurantCommentEntity>();
        CreateMap<RestaurantCommentEntity, CreateRestaurantCommentRequest>();
        
        CreateMap<SaveCustomerRatingRequest, CustomerRatingEntity>();
        CreateMap<CustomerRatingEntity, SaveCustomerRatingRequest>();
        
        CreateMap<SaveRestaurantRatingRequest, RestaurantRatingEntity>();
        CreateMap<RestaurantRatingEntity, SaveRestaurantRatingRequest>();

        CreateMap<RestaurantSubscriptionInfoEntity, RestaurantSubscriptionsEntity>();
        CreateMap<RestaurantSubscriptionsEntity, RestaurantSubscriptionInfoEntity>();
    }
}