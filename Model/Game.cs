namespace ClickWars2.Model
{
    public class Game(int id, string? playerName, uint score)
    {
        public int Id { get; set; } = id;
        public string? PlayerName { get; set; } = playerName;
        public uint Score { get; set; } = score;
    }
}
