using System.Security.Claims;
using AutoMapper;
using DOTNET_RPG.Data;
using DOTNET_RPG.DTOs.Character;
using DOTNET_RPG.Models;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Services.CharacterService;

public class CharacterService : ICharacterService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    private int GetUserId()
    {
        return int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
        var dbCharacters = await _context.Characters.Where(c => c.User!.Id == GetUserId()).ToListAsync();

        serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
        var dbCharacter = await _context.Characters
            .FirstOrDefaultAsync(c => c.ID == id && c.User!.Id == GetUserId());
        serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(dbCharacter);

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterResponseDTO>> AddCharacter(AddCharacterDTO newCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
        var character = _mapper.Map<Character>(newCharacter);
        character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

        _context.Characters.Add(character);
        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(character);
        serviceResponse.Message = $"Character {character.Name} added correctly.";
        serviceResponse.StatusCode = 201;

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterResponseDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();

        try
        {
            var character =
                await _context.Characters.Include(c => c.User)
                    .FirstOrDefaultAsync(c =>
                        c.ID == updatedCharacter.ID && c.User!.Id == GetUserId());

            if (character is null) throw new Exception($"Character with ID {updatedCharacter.ID} not found.");

            _mapper.Map<Character>(updatedCharacter);

            character.Name = updatedCharacter.Name;
            character.Class = updatedCharacter.Class;
            character.Defense = updatedCharacter.Defense;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Strength = updatedCharacter.Strength;

            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(character);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }


    public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> DeleteCharacter(int id)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();

        try
        {
            var character = _context.Characters.FirstOrDefault(c => c.ID == id && c.User!.Id == GetUserId());

            if (character is null) throw new Exception($"Character with ID {id} not found.");

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            serviceResponse.Message = $"Character with ID {id} deleted correctly.";
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterResponseDTO>> AddCharacterSkill(
        AddCharacterSkillDTO newCharacterSkill)
    {
        var response = new ServiceResponse<GetCharacterResponseDTO>();

        try
        {
            var character = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.ID == newCharacterSkill.CharacterId && c.User!.Id == GetUserId());

            if (character is null)
            {
                response.Success = false;
                response.Message = $"Character with ID {newCharacterSkill.CharacterId} not found.";
                return response;
            }

            var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);

            if (skill is null)
            {
                response.Success = false;
                response.Message = $"Skill with ID {newCharacterSkill.SkillId} not found.";
                return response;
            }

            character.Skills.Add(skill);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetCharacterResponseDTO>(character);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return response;
    }
}