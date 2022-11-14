using System.Security.Claims;
using GameStore.API.Extensions;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost("game/{gameKey}/newComment")]
    public async Task<ActionResult<CommentModel>> AddComment(string gameKey, CommentCreateModel comment)
    {
        var userName = User.GetUserName();

        var created = await _commentService.AddAsync(userName, gameKey, comment);

        return CreatedAtAction(nameof(GetByGameKey), new {GameKey = gameKey}, created);
    }

    [AllowAnonymous]
    [HttpGet("game/{gamekey}/comments")]
    public async Task<ActionResult<IEnumerable<CommentModel>>> GetByGameKey(string gameKey)
    {
        var comments = await _commentService.GetByGameKeyAsync(gameKey);

        return Ok(comments);
    }

    [HttpPut("{commentId}/update")]
    public async Task<ActionResult> UpdateComment(int commentId, CommentCreateModel comment)
    {
        var userName = User.GetUserName();

        await _commentService.UpdateAsync(userName, commentId, comment);

        return Ok();
    }

    [HttpPut("{commentId}/mark")]
    public async Task<ActionResult> MarkForDeletion(int commentId)
    {
        var userName = User.GetUserName();

        await _commentService.MarkForDeletionAsync(userName, commentId);

        return Ok();
    }

    [HttpDelete("game/{gameKey}/deleteMarked")]
    public async Task<ActionResult> Delete(string gameKey)
    {
        var userName = User.GetUserName();

        await _commentService.DeleteMarkedCommentAsync(userName, gameKey);

        return NoContent();
    }

    [HttpPut("{commentId}/restore")]
    public async Task<ActionResult> RestoreAsync(int commentId)
    {
        await _commentService.RestoreAsync(commentId);

        return Ok();
    }
}