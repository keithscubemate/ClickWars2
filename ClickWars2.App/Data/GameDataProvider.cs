using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using ClickWars2.Model;

namespace ClickWars2.Data
{
    public interface IGameDataProvider
    {
        Task<IEnumerable<Game>?> GetAllAsync();
        Task WriteAllAsync(IEnumerable<Game> games);
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

        public Task WriteAllAsync(IEnumerable<Game> games)
        {
            foreach (var game in games)
            {
                var status = $"{game.Id}:{game.Score}:{game.PlayerName}";

                Console.WriteLine(status);
            }

            return Task.CompletedTask;
        }
    }
    
    public class XmlGameDataProvider : IGameDataProvider
    {
        public async Task<IEnumerable<Game>?> GetAllAsync()
        {
            var serializer = new XmlSerializer(typeof(List<Game>));

            await using FileStream stream = File.OpenRead("Games.xml");
            var games = serializer.Deserialize(stream);
            return (IEnumerable<Game>?)games;
        }

        public async Task WriteAllAsync(IEnumerable<Game> games)
        {
            var serializer = new XmlSerializer(typeof(List<Game>));
            
            var gamesList = games.ToList();

            await using FileStream stream = File.OpenWrite("Games.xml");
            serializer.Serialize(stream, gamesList);
        }
    }
}
