using System.Net.Http.Json;
using BlazorWebFullstackCrud.Shared;

namespace BlazorWebFullstackCrud.Client.Services.SuperHeroService;

public class SuperHeroService : ISuperHeroService
{
    private readonly HttpClient _httpClient;
    public List<SuperHero> Heroes { get; set; } = new List<SuperHero>();
    public List<Comic> Comics { get; set; } = new List<Comic>();

    public SuperHeroService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task GetComics()
    {
        var result = await _httpClient.GetFromJsonAsync<List<Comic>>("api/superhero/comics");
        if (result is not null)
        {
            Comics = result;
        }
    }

    public async Task GetSuperHeroes()
    {
        var result = await _httpClient.GetFromJsonAsync<List<SuperHero>>("api/superhero");
        if (result is not null)
        {
            Heroes = result;
        }
    }

    public async Task<SuperHero> GetSingleHero(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<SuperHero>($"api/superhero/{id}");
        if (result is not null)
        { 
            return result;
        }
        throw new Exception();

    }
}