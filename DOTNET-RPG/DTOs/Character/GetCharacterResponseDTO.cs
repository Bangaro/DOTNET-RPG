using DOTNET_RPG.DTOs.Skill;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.DTOs.Character;

public class GetCharacterResponseDTO
{
    public int ID { get; set; }
    public string Name { get; set; } = "Frodo";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Knight;
    public GetWeaponDTO Weapon { get; set; } = null!;
    public List<GetSkillDTO> Skills { get; set; } = null!;
    public int Fights { get; set; }
    public int Victories { get; set; }
    public int Defeats { get; set; }
}