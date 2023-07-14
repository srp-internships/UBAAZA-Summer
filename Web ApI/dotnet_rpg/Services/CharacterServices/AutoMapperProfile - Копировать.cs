using dotnet_rpg.Dtosa.Skill;

namespace dotnet_rpg.Services.CharacterServices
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, Character>();
            CreateMap<Skill, GetSkillDto>();
            

        }
    }
}
