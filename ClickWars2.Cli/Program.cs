using ClickWars2.Data;
using ClickWars2.ViewModel;

namespace ClickWars2.Cli;

static class Program
{
    static void Main(string[] args)
    {
        var dataProv = new GameDataProvider();
        var gamesViewModel = new GamesViewModel(dataProv);
        gamesViewModel.LoadAsync().Wait();

        foreach (var game in gamesViewModel.Games)  
        {
            Console.WriteLine($"{game.PlayerName}:{game.Id}");
        }
    }
}