using System.Net.Http.Json;
using BlazorWebFullstackCrud.Client.Pages;
using BlazorWebFullstackCrud.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorWebFullstackCrud.Client.Services.SuperHeroService;

public class SuperHeroService : ISuperHeroService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    public List<SuperHero> Heroes { get; set; } = new List<SuperHero>();
    public List<Comic> Comics { get; set; } = new List<Comic>();

    public SuperHeroService(HttpClient httpClient, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
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

    public async Task CreateHero(SuperHero superHero)
    {
        var result = await _httpClient.PostAsJsonAsync("api/superhero", superHero);
        var response = await result.Content.ReadFromJsonAsync<List<SuperHero>>();
        Heroes = response;
        _navigationManager.NavigateTo("superheroes");
    }

    public async Task UpdateHero(int id, SuperHero superHero)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/superhero/{id}", superHero);
        var response = await result.Content.ReadFromJsonAsync<List<SuperHero>>();
        Heroes = response;
        _navigationManager.NavigateTo("superheroes");
    }

    public async Task DeleteHero(int id)
    {
        var result = await _httpClient.DeleteAsync($"api/superhero/{id}");
        var response = await result.Content.ReadFromJsonAsync<List<SuperHero>>();
        Heroes = response;
        _navigationManager.NavigateTo("superheroes");
    }
}