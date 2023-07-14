using dotnet_rpg.Dtosa.Skill;
using dotnet_rpg.Dtosa.Weapon;

namespace dotnet_rpg.Dtosa.Charater
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public GetWeaponDto? Weapon { get; set; }
        public List<GetSkillDto>? Skills { get; set; }
        public int fights { get; set; }
        public int Victoties { get; set; }
        public int Defeats { get; set; }

    }
}
