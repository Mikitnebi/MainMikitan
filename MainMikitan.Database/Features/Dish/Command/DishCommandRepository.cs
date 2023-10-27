using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Menu;
using MainMikitan.Domain.Requests;

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


    #endregion

    #endregion
    
    public async Task<bool> SaveDishChanges()
    {
        var response = await _db.SaveChangesAsync();

        return response > 0;
    }
}