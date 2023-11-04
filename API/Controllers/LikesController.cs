﻿using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class LikesController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly ILikesRepository _likesRepository;

    public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
    {
        _userRepository = userRepository;
        _likesRepository = likesRepository;
    }

    [HttpPost("{username}")]
    public async Task<ActionResult> AddLike(string username)
    {
        var sourseUserId = User.GetUserId();
        var likedUser = await _userRepository.GetUserByUsernameAsync(username);
        var sourseUser = await _likesRepository.GetUserWithLikes(sourseUserId);

        if(likedUser == null) return NotFound();

        if(sourseUser.UserName == username) return BadRequest("You cannot like yourself");

        var userLike = await _likesRepository.GetUserLike(sourseUserId, likedUser.Id);

        if(userLike != null) return BadRequest("You already like this user");

        userLike = new UserLike
        {
            SourseUserId = sourseUser.Id,
            TargetUserId = likedUser.Id
        };

        sourseUser.LikedUsers.Add(userLike);

        if(await _userRepository.SaveAllAsync()) return Ok();

        return BadRequest("Failed to like user");
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery]LikesParams likesParams)
    {
        likesParams.UserId = User.GetUserId();

        var users = await _likesRepository.GetUserLikes(likesParams);

        Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage,
            users.PageSize, users.TotalCount, users.TotalPages));

        return Ok(users);
    }
}
