using DOTNET_RPG.DTOs.Character;
using DOTNET_RPG.DTOs.Weapon;
using DOTNET_RPG.Models;
using DOTNET_RPG.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_RPG.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeaponController : ControllerBase
{
    private readonly IWeaponService _weaponService;

    public WeaponController(IWeaponService weaponService)
    {
        _weaponService = weaponService;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDTO>>> AddWeapon(AddWeaponDTO newWeapon)
    {
        return Ok(await _weaponService.AddWeapon(newWeapon));
    }
}