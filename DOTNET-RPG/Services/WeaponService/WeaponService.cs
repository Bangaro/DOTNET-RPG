using System.Security.Claims;
using AutoMapper;
using DOTNET_RPG.Data;
using DOTNET_RPG.DTOs.Character;
using DOTNET_RPG.DTOs.Weapon;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.WeaponService;

public class WeaponService : IWeaponService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<GetCharacterResponseDTO>> AddWeapon(AddWeaponDTO newWeapon)
    {
        var response = new ServiceResponse<GetCharacterResponseDTO>();
        try
        {
            var character = _context.Characters.FirstOrDefault(c =>
                c.ID == newWeapon.CharacterId && c.User!.Id ==
                int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!));

            if (character is null)
            {
                response.Success = false;
                response.Message = "Character not found.";
                return response;
            }

            var weapon = new Weapon
            {
                Name = newWeapon.Name,
                Damage = newWeapon.Damage,
                Character = character
            };

            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetCharacterResponseDTO>(character);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return response;
    }
}