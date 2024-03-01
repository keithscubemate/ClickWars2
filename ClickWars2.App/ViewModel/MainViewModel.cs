using ClickWars2.Model;

namespace ClickWars2.ViewModel;

public class MainViewModel : ViewModelBase
{
    public GamesViewModel GamesViewModel { get; }


    public MainViewModel(GamesViewModel gamesViewModel)
    {
        this.GamesViewModel = gamesViewModel;
    }

    public override async Task LoadAsync()
    {
        await this.GamesViewModel.LoadAsync();
    }
    public override async Task WriteAsync()
    {
        await this.GamesViewModel.WriteAsync();
    }
}