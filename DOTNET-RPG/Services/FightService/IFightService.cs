using DOTNET_RPG.DTOs.Fight;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.FightService;

public interface IFightService
{
    Task<ServiceResponse<AttackResultsDTO>> WeaponAttack(WeaponAttackDTO request);
}