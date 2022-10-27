using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
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

        return Ok(game);
    }

    [HttpGet]
    [Route("search")]
    public async Task<ActionResult<GameModel>> Search([FromQuery] GameSearchOptions searchOptions)
    {
        var games = await _gameService.SearchAsync(searchOptions);

        return Ok(games);
    }

    [HttpPost]
    [Route("new")]
    public async Task<ActionResult<GameModel>> Add(GameCreateModel game)
    {
        var created = await _gameService.AddAsync(game);

        return CreatedAtAction(nameof(GetByKey), new {Key = created.Key}, created);
    }

    [HttpPut]
    [Route("update/{gameKey}")]
    public async Task<ActionResult> Update(string gameKey, GameCreateModel game)
    {
        await _gameService.UpdateAsync(gameKey, game);

        return Ok();
    }

    [HttpPost]
    [Route("image/{gameKey}")]
    public async Task<ActionResult<ImageModel>> SetImage(string gameKey, IFormFile image)
    {
        var created = await _gameService.SetImageAsync(gameKey, image);

        return CreatedAtAction(nameof(GetByKey), new {Key = gameKey}, created);
    }

    [HttpDelete]
    [Route("remove/{gameId}")]
    public async Task<ActionResult> Delete(int gameId)
    {
        await _gameService.DeleteAsync(gameId);

        return NoContent();
    }

    [HttpGet("/api/game/{gamekey}/download")]
    public ActionResult DownloadGame(string gameKey)
    {
        return File(new MemoryStream(), "application/octet-stream", "game.bin");
    }

    [HttpGet]
    [Route("genres")]
    public async Task<ActionResult<IEnumerable<GenreModel>>> GetGenres()
    {
        var genres = await _gameService.GetGenresAsync();

        return Ok(genres);
    }
    
    [HttpGet]
    [Route("platforms")]
    public async Task<ActionResult<IEnumerable<GenreModel>>> GetPlatforms()
    {
        var platforms = await _gameService.GetPlatformsAsync();

        return Ok(platforms);
    }
}