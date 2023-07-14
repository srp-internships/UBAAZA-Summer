namespace dotnet_rpg.Dtosa.Weapon
{
    public class AddWeaponDto
    {
        public string Attacker { get; set; }=string.Empty;
        public string Opponent { get; set; } = string.Empty;
        public int AttackerHP { get; set; }
        public string OpponentHP { get; set; }

        public int Damage { get; set; }
        public int CharacterId { get; internal set; }
        public string Name { get; internal set; }
    }
}
