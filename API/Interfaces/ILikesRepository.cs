using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourseUserId, int targetUserId);
    Task<AppUser> GetUserWithLikes(int userId);
    Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
}
