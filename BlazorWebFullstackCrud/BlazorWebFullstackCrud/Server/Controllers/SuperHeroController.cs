using Microsoft.AspNetCore.Mvc;
namespace BlazorWebFullstackCrud.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuperHeroController : ControllerBase
{
    public static List<Comic> Comics = new List<Comic>()
    {
        new Comic() {Id = 1, Name = "Marvel"},
        new Comic() {Id = 2, Name = "DC"}
    };

    public static List<SuperHero> SuperHeroes = new List<SuperHero>()
    {
        new SuperHero(){ Id = 1, FirstName = "Peter", LastName = "Parker", HeroName = "Spiderman", Comic = Comics[0], ComicId=1},
        new SuperHero(){ Id = 2, FirstName = "Bruce", LastName = "Wayne", HeroName = "Batman", Comic = Comics[1], ComicId=2}
    };

    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
    {
        return Ok(SuperHeroes);
    }

    [HttpGet("comics")]
    public async Task<ActionResult<List<Comic>>> GetComics()
    {
        return Ok(Comics);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SuperHero>> GetSuperHero(int id)
    {
        var result = SuperHeroes.FirstOrDefault(x => x.Id == id);
        if (result is null)
        {
            return NotFound("Sorry not hero here");
        }
        return Ok(result);
    }
}