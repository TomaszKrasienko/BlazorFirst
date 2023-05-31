using BlazorWebFullstackCrud.Shared;

namespace BlazorWebFullstackCrud.Client.Services.SuperHeroService;

public interface ISuperHeroService
{
    List<SuperHero> Heroes { get; set; }
    List<Comic> Comics { get; set; }
    Task GetComics();
    Task GetSuperHeroes();
    Task<SuperHero> GetSingleHero(int id);
    Task CreateHero(SuperHero superHero);
    Task UpdateHero(int id, SuperHero superHero);
    Task DeleteHero(int id);
}