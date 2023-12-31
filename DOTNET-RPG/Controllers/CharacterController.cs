using System.Security.Claims;
using DOTNET_RPG.DTOs.Character;
using DOTNET_RPG.Models;
using DOTNET_RPG.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_RPG.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;


    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDTO>>>> GetAll()
    {
        var id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        return Ok(await _characterService.GetAllCharacters(id));
    }

    [HttpGet("GetSingle/{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDTO>>> GetSingle(int id)
    {
        return Ok(await _characterService.GetCharacterById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDTO>>> AddCharacter(
        AddCharacterDTO character)
    {
        return Ok(await _characterService.AddCharacter(character));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDTO>>> UpdateCharacter(
        UpdateCharacterDTO character)
    {
        var response = await _characterService.UpdateCharacter(character);
        if (response.Data is null) return NotFound(response);

        return Ok(await _characterService.UpdateCharacter(character));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDTO>>> DeleteCharacter(int id)
    {
        var response = await _characterService.DeleteCharacter(id);


        return Ok(response);
    }
}