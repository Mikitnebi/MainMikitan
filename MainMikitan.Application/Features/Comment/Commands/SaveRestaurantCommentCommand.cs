using AutoMapper;
using MainMikitan.Database.Features.Comment.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Comments;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Comment;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Comment.Commands;

public class SaveRestaurantCommentCommand(CreateRestaurantCommentRequest request, int userId) : ICommand<bool>
{
    public int UserId { get; set; } = userId;
    public CreateRestaurantCommentRequest Request { get; set; } = request;
}

public class SaveRestaurantCommentCommandHandler(IRestaurantCommentCommandRepository commentCommandRepository,
    IMapper mapper) 
    : ResponseMaker, ICommandHandler<SaveRestaurantCommentCommand, bool>
{
    public async Task<ResponseModel<bool>> Handle(SaveRestaurantCommentCommand command,
        CancellationToken cancellationToken)
    {
        var commentEntity = mapper.Map<RestaurantCommentEntity>(command.Request);
        commentEntity.CreatedAt = DateTime.Now;
        commentEntity.UserId = command.UserId;
        
        await commentCommandRepository.SaveRestaurantComment(commentEntity);
        var result = await commentCommandRepository.SaveChangesAsync();

        return result ? Success() : Fail(ErrorResponseType.Comment.CommentSaveFail);
    }
}