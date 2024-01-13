using DOTNET_RPG.Data;
using DOTNET_RPG.DTOs.Fight;
using DOTNET_RPG.Models;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Services.FightService;

public class FightService : IFightService
{
    private readonly DataContext _context;

    public FightService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<AttackResultsDTO>> WeaponAttack(WeaponAttackDTO request)
    {
        var response = new ServiceResponse<AttackResultsDTO>();
        try
        {
            var attacker = await _context.Characters.Include(c => c.Weapon)
                .FirstOrDefaultAsync(c => c.ID == request.AttackerId);
            var opponent = await _context.Characters
                .FirstOrDefaultAsync(c => c.ID == request.AttackerId);

            if (attacker is null || opponent is null || attacker.Weapon is null)
                throw new Exception("Attacker or Opponent or Weapon is null");

            var damage = attacker.Weapon.Damage + new Random().Next(attacker.Strength);
            damage -= new Random().Next(opponent.Defense);

            if (damage > 0)
                opponent.HitPoints -= damage;
            if (opponent.HitPoints <= 0)
                response.Message = $"{opponent.Name} has been defeated!";

            await _context.SaveChangesAsync();

            response.Data = new AttackResultsDTO
            {
                Attacker = attacker.Name,
                Opponent = opponent.Name,
                AttackerHP = attacker.HitPoints,
                OpponentHP = opponent.HitPoints,
                Damage = damage
            };
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return response;
    }
}