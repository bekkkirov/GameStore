using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost("/api/game/{gamekey}/newComment")]
    public async Task<ActionResult> AddComment(string gameKey, CommentCreateModel comment)
    {
        //Later i'll extract username from JWT, but for now i left it hardcoded.
        await _commentService.AddAsync("bekirov", gameKey, comment);

        return Ok();
    }

    [HttpGet("/api/game/{gamekey}/comments")]
    public async Task<ActionResult<IEnumerable<CommentModel>>> GetCommentsByGameKey(string gameKey)
    {
        var comments = await _commentService.GetByGameKeyAsync(gameKey);

        return Ok(comments);
    }
}