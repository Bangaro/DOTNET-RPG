using DOTNET_RPG.DTOs.Character;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.CharacterService;

public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters(int userID);
    Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacterById(int id);
    Task<ServiceResponse<GetCharacterResponseDTO>> AddCharacter(AddCharacterDTO newCharacter);
    Task<ServiceResponse<GetCharacterResponseDTO>> UpdateCharacter(UpdateCharacterDTO newCharacter);

    Task<ServiceResponse<List<GetCharacterResponseDTO>>> DeleteCharacter(int id);
}