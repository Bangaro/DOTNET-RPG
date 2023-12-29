using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.CharacterService;

public class CharacterService: ICharacterService
{
    private static List<Character> characters = new List<Character>()
    {
        new Character(),
        new Character {ID = 1, Name = "Sam"}
    };
    
    public async Task<List<Character>> GetAllCharacters()
    {
        return characters;
    }

    public async Task<Character> GetCharacterById(int id)
    {
        var character = characters.FirstOrDefault(c => c.ID == id);
        if (character is not null)
        {
            return character;
        }

        throw new Exception("Character not found");
    }

    public async Task<List<Character>> AddCharacter(Character newCharacter)
    {
        characters.Add(newCharacter);
        return characters;
    }
}