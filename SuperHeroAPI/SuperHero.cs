using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI;

public class SuperHero
{
    public int Id { get; set; }
    [BindProperty]
    public string Name { get; set; } = string.Empty;
    [BindProperty]
    public string FirstName { get; set; } = string.Empty;
    [BindProperty]
    public string LastName { get; set; } = string.Empty;
    [BindProperty]
    public string PlaceOfBirth { get; set; } = string.Empty;

}