using AutoMapper;
using DOTNET_RPG.DTOs;
using DOTNET_RPG.DTOs.Character;
using DOTNET_RPG.DTOs.Skill;
using DOTNET_RPG.Models;

namespace DOTNET_RPG;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterResponseDTO>();
        CreateMap<AddCharacterDTO, Character>();
        CreateMap<UpdateCharacterDTO, Character>();
        CreateMap<Weapon, GetWeaponDTO>();
        CreateMap<Skill, GetSkillDTO>();
    }
}