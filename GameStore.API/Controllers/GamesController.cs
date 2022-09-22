using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly ICommentService _commentService;

    public GamesController(IGameService gameService, ICommentService commentService)
    {
        _gameService = gameService;
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameModel>>> Get()
    {
        var games = await _gameService.GetAllAsync();

        return Ok(games);
    }

    [HttpGet("/api/game/{key}")]
    public async Task<ActionResult<GameModel>> GetByKey(string key)
    {
        var game = await _gameService.GetByKeyAsync(key);

        if (game is null)
        {
            return NotFound();
        }

        return Ok(game);
    }

    [HttpPost]
    [Route("new")]
    public async Task<ActionResult<GameModel>> Add(GameCreateModel game)
    {
        await _gameService.AddAsync(game);

        return Ok();
    }

    [HttpPut]
    [Route("update/{gameKey}")]
    public async Task<ActionResult> Update(string gameKey, GameCreateModel game)
    {
        await _gameService.UpdateAsync(gameKey, game);

        return NoContent();
    }

    [HttpDelete]
    [Route("remove/{gameId}")]
    public async Task<ActionResult> Delete(int gameId)
    {
        await _gameService.DeleteAsync(gameId);

        return NoContent();
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

    [HttpGet("/api/game/{gamekey}/download")]
    public ActionResult DownloadGame(string gameKey)
    {
        return File(new MemoryStream(), "application/octet-stream", "game.bin");
    }
}