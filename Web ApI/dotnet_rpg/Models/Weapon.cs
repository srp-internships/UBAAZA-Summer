namespace dotnet_rpg.Models
{
    public class Weapon
    {
        public int id { get; set; }
        public string name { get; set; }=string.Empty;
        public int Damage { get; set; }
        public Character? Character { get; set; }
        public int CharacterId { get ; set; }
    }
}
