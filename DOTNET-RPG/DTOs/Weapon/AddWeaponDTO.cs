namespace DOTNET_RPG.DTOs.Weapon;

public class AddWeaponDTO
{
    public string Name { get; set; } = null!;
    public int Damage { get; set; }
    public int CharacterId { get; set; }
}