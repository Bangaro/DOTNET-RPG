using DOTNET_RPG.DTOs.Fight;
using DOTNET_RPG.Models;
using DOTNET_RPG.Services.FightService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_RPG.Controllers;

[ApiController]
[Route("[controller]")]
public class FightController : ControllerBase
{
    private readonly IFightService _fightService;

    public FightController(IFightService fightService)
    {
        _fightService = fightService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<AttackResultsDTO>>> WeaponAttack(WeaponAttackDTO request)
    {
        return StatusCode(200, await _fightService.WeaponAttack(request));
    }
}