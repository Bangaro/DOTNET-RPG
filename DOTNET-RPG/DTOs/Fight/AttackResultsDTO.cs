namespace DOTNET_RPG.DTOs.Fight;

public class AttackResultsDTO
{
    public string Attacker { get; set; } = null!;
    public string Opponent { get; set; } = null!;
    public int AttackerHP { get; set; }
    public int OpponentHP { get; set; }
    public int Damage { get; set; }
}