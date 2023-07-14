namespace dotnet_rpg.Dtosa.Weapon
{
    public class GetWeaponDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HirPosts { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public GetCharacterDto Weapon { get; set; }
    }
}
