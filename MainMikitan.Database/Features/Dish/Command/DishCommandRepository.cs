using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Menu;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Responses.DishResponses;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Dish.Command;

public class DishCommandRepository : IDishCommandRepository
{
    private readonly MikDbContext _db;

    public DishCommandRepository(MikDbContext db)
    {
        _db = db;
    }

    #region DishCategory

    #region Add

    public async Task AddDishCategory(AddCategoryDishRequest request)
    {
        var categoryDishEntity = new CategoryDishEntity
        {
            NameGeo = request.NameGeo,
            NameEng = request.NameEng,
            CreatedAt = request.CreateAt
        };
        
        await _db.CategoryDish.AddAsync(categoryDishEntity);
        await _db.SaveChangesAsync();
    }

    #endregion
    

    #endregion

    #region DishInfo

    #region Add

    public async Task AddDishInfo(AddDishInfoRequest request)
    {
        var addDishInfoEntity = new DishInfoEntity
        {
            DishId = request.DishId,
            NameGeo = request.NameGeo,
            NameEng = request.NameEng,
            IngredientsGeo = request.IngredientsGeo,
            IngredientsEng = request.IngredientsEng,
            DescriptionGeo = request.DescriptionGeo,
            DescriptionEng = request.DescriptionEng,
            CreateAt = request.CreateAt
        };

        await _db.DishInfo.AddAsync(addDishInfoEntity);
        await _db.SaveChangesAsync();
    }

    #endregion

    #region Update

    public async Task<bool> UpdateDishInfo(UpdateDishInfoRequest request)
    {
        var dish = await _db.DishInfo.FirstOrDefaultAsync(d => d.DishId == request.DishId);
        
        if (dish is null) return false;

        dish.NameGeo = request.NameGeo ?? dish.NameGeo;
        dish.NameEng = request.NameEng ?? dish.NameEng;
        dish.IngredientsGeo = request.IngredientsGeo ?? dish.IngredientsGeo;
        dish.IngredientsEng = request.IngredientsEng ?? dish.IngredientsEng;
        dish.DescriptionGeo = request.DescriptionGeo ?? dish.DescriptionGeo;
        dish.DescriptionEng = request.DescriptionEng ?? dish.DescriptionEng;
        
        var saveResult = await _db.SaveChangesAsync();
        return saveResult > 0;
    }

    #endregion
    
    #endregion

    #region Dish

    #region Add

    public async Task AddDish(AddDishRequest request)
    {
        var dishEntity = new DishEntity
        {
            IsActive = request.IsActive,
            IsDeleted = request.IsDeleted,
            CategoryDishId = request.CategoryDishId,
            ParentDishId = request.ParentDishId,
            IsVerified = request.IsVerified,
            RestaurantId = request.RestaurantId,
            HasDifferentPicture = request.HasDifferentPicture,
            CreateAt = request.CreatedAt,
            CreateUserId = request.CreateUserId
        };

        await _db.Dish.AddAsync(dishEntity);
    }

    #endregion

    #region Get

    public List<GetDishInfoResponse> GetAllDishes(GetAllDishesRequest request)
    {
        var dishes = from dish in _db.Dish
            join dishInfo in _db.DishInfo on dish.Id equals dishInfo.DishId
            where dish.RestaurantId == request.RestaurantId
            select new GetDishInfoResponse
            {
                IngredientsGeo = dishInfo.IngredientsGeo,
                IngredientsEng = dishInfo.IngredientsEng,
                DescriptionGeo = dishInfo.DescriptionGeo,
                DescriptionEng = dishInfo.DescriptionEng,
                NameGeo = dishInfo.NameGeo,
                NameEng = dishInfo.NameEng,
                CreateAt = dishInfo.CreateAt
            };

        return dishes.ToList();
    }

    #endregion

    #region Deactive

    public async Task<bool> DeactiveDish(DeactiveDishRequest request)
    {
        var dish = await _db.Dish.FirstOrDefaultAsync(d => d.Id == request.DishId);
        
        if (dish is null) return false;
        
        dish.IsActive = request.IsActiveStatus;
        dish.UpdateUserId = request.UpdateUserId;
        dish.UpdateAt = DateTime.Now;

        var saveResult = await _db.SaveChangesAsync();
        return saveResult > 0;

    }
    

    #endregion

    #region Delete

    public async Task<bool> DeleteDish(DeleteDishRequest request)
    {
        var dish = await _db.Dish.FirstOrDefaultAsync(d => d.Id == request.DishId);
        if (dish is null) return false;

        dish.IsDeleted = request.IsDeletedStatus;

        var saveResponse = await _db.SaveChangesAsync();

        return saveResponse > 0;
    }

    #endregion

    #region Verify

    public async Task<bool> VerifyDish(VerifyDishRequest request)
    {
        var dish = await _db.Dish.FirstOrDefaultAsync(d => d.Id == request.DishId);
        if (dish is null) return false;

        dish.IsVerified = request.IsVerifiedStatus;

        var saveStatus = await _db.SaveChangesAsync();
        return saveStatus > 0;
    }

    #endregion
    
    #endregion
    
    public async Task<bool> SaveDishChanges()
    {
        var response = await _db.SaveChangesAsync();

        return response > 0;
    }
}