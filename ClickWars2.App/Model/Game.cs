namespace ClickWars2.Model
{
    [Serializable]
    public class Game(int id, string? playerName, uint score)
    {
        public Game() : this(0, "", 0)
        {
        }

        public int Id { get; set; } = id;
        public string? PlayerName { get; set; } = playerName;
        public uint Score { get; set; } = score;
    }
}
