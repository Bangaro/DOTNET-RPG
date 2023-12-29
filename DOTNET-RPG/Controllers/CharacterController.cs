using DOTNET_RPG.Models;
using DOTNET_RPG.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_RPG.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController: ControllerBase
{
    private readonly ICharacterService _characterService;
    
    
    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<List<Character>>> GetAll()
    {
        return Ok(await _characterService.GetAllCharacters());
    }
    
    [HttpGet("GetSingle/{id}")]
    public async Task<ActionResult<List<Character>>> GetSingle(int id)
    {
        return Ok(await _characterService.GetCharacterById(id));
    }

    [HttpPost]
    public async Task<ActionResult<List<Character>>> AddCharacter(Character character)
    {
        return Ok(await _characterService.AddCharacter(character));
    }
    
    
}