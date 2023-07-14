namespace dotnet_rpg.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Demage { get; set; }
        public List<Character> Character { get; set; }  
    }
}
