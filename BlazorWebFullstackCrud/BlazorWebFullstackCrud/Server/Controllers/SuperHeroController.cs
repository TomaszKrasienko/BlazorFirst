using BlazorWebFullstackCrud.Client.Pages;
using BlazorWebFullstackCrud.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebFullstackCrud.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuperHeroController : ControllerBase
{


    private readonly DataContext _context;
    public SuperHeroController(DataContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
    {
        var heroes = await _context
            .SuperHeroes
            .Include(x => x.Comic)
            .ToListAsync();
        return Ok(heroes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SuperHero>> GetSuperHero([FromRoute] int id)
    {
        var result = await _context.SuperHeroes
            .Include(x => x.Comic).FirstOrDefaultAsync(x => x.Id == id);
        if (result is null)
        {
            return NotFound("Sorry not hero here");
        }
        return Ok(result);
    }

    [HttpGet("comics")]
    public async Task<ActionResult<List<Comic>>> GetComics()
    {
        var comics = await _context.Comics.ToListAsync();
        return Ok(comics);
    }

    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero superHero)
    {
        superHero.Comic = null;
        await _context.SuperHeroes.AddAsync(superHero);
        await _context.SaveChangesAsync();
        return Ok(await GetDbHeroes());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero([FromRoute] int id, SuperHero superHero)
    {
        var dbHero = await _context.SuperHeroes.Include(x => x.Comic).FirstOrDefaultAsync(x => x.Id == id);
        if (dbHero is null)
            return NotFound("Sorry, but no hero for you");
        dbHero.FirstName = superHero.FirstName;
        dbHero.LastName = superHero.LastName;
        dbHero.HeroName = superHero.HeroName;
        dbHero.ComicId = superHero.ComicId;
        await _context.SaveChangesAsync();
        return Ok(await GetDbHeroes());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero([FromRoute] int id)
    {
        var dbHero = await _context.SuperHeroes.Include(x => x.Comic).FirstOrDefaultAsync(x => x.Id == id);
        if (dbHero is null)
            return NotFound("Sorry, but no hero for you");
        _context.Remove(dbHero);
        await _context.SaveChangesAsync();
        return Ok(await GetDbHeroes());
    }

    private async Task<List<SuperHero>> GetDbHeroes()
    {
        return await _context.SuperHeroes.Include(x => x.Comic).ToListAsync();
    }
}