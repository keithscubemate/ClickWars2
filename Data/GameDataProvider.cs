using ClickWars2.Model;

namespace ClickWars2.Data
{
    public interface IGameDataProvider
    {
        Task<IEnumerable<Game>?> GetAllAsync();
    }
    public class GameDataProvider : IGameDataProvider
    {
        public async Task<IEnumerable<Game>?> GetAllAsync()
        {
            await Task.Delay(100);

            return new List<Game>
            {
                new(1, "AKJ", 100),
                new(2, "KAJ", 102),
                new(3, "AJK", 104),
                new(4, "JAK", 106),
            };
        }
    }
}
