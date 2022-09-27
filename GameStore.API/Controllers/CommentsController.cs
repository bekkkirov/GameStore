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

    [HttpPost("game/{gamekey}/newComment")]
    public async Task<ActionResult<CommentModel>> AddComment(string gameKey, CommentCreateModel comment)
    {
        //Later i'll extract username from JWT, but for now i left it hardcoded.
        var created = await _commentService.AddAsync("bekirov", gameKey, comment);

        return CreatedAtAction(nameof(GetByGameKey), new {GameKey = gameKey}, created);
    }

    [HttpGet("game/{gamekey}/comments")]
    public async Task<ActionResult<IEnumerable<CommentModel>>> GetByGameKey(string gameKey)
    {
        var comments = await _commentService.GetByGameKeyAsync(gameKey);

        return Ok(comments);
    }
}