using ClickWars2.Data;
using ClickWars2.Model;
using System.Collections.ObjectModel;
using ClickWars2.Command;

namespace ClickWars2.ViewModel
{

    public class GamesViewModel : ViewModelBase
    {
        private string _newGamePlayerName = "";
        private readonly IGameDataProvider _gameDataProvider;
        private GameItemViewModel? _activeGameItemViewModel;

        private static readonly Comparison<GameItemViewModel> GameComp =
            (g1, g2) => (int)g2.Score - (int)g1.Score;

        public bool NotGaming => this._activeGameItemViewModel is null;
        public bool Gaming => this._activeGameItemViewModel is not null;
        public string NewGamePlayerName
        {
            get => this._newGamePlayerName;
            set
            {
                this._newGamePlayerName = value;
                RaisePropertyChanged();
                this.NewGameCommand.RaiseCanExecuteChanged();
            }
        }

        public TimerViewModel GameTimerViewModel { get; }

        public GameItemViewModel? ActiveGameItemViewModel
        {
            get => this._activeGameItemViewModel;
            set
            {
                this._activeGameItemViewModel = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Gaming));
                RaisePropertyChanged(nameof(NotGaming));
                this.IncrementScoreCommand.RaiseCanExecuteChanged();
                this.SaveGameCommand.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<GameItemViewModel> Games { get; } = [];
        public DelegateCommand NewGameCommand { get; private set; }
        public DelegateCommand IncrementScoreCommand { get; private set; }
        public DelegateCommand SaveGameCommand { get; private set; }

        public GamesViewModel(IGameDataProvider gameDataProvider)
        {
            this._gameDataProvider = gameDataProvider;
            this.GameTimerViewModel = new();
            this.NewGameCommand = new DelegateCommand(this.NewGame, this.CanNewGame);
            this.IncrementScoreCommand = new DelegateCommand(this.IncrementScore, this.CanIncrementScore);
            this.SaveGameCommand = new DelegateCommand(this.SaveGame, this.CanSaveGame);
        }

        public override async Task LoadAsync()
        {
            if (this.Games.Any())
            {
                return;
            }

            var games = await _gameDataProvider.GetAllAsync() ?? Enumerable.Empty<Game>();

            foreach (var g in games)
            {
                this.Games.InsertSorted(new GameItemViewModel(g), GameComp);
            }
        }

        public override async Task WriteAsync()
        {
            var games = this.Games.Select(g => g.ToGame()).AsEnumerable();
            await this._gameDataProvider.WriteAllAsync(games);
        }

        private void NewGame(object? obj)
        {
            var nextId = this.GetNextGameId();
            var playerName = this.NewGamePlayerName;
            const uint newGameScore = 0;

            this.NewGamePlayerName = new string("");
            
            var game = new Game(nextId, playerName, newGameScore);

            this.ActiveGameItemViewModel = new GameItemViewModel(game);
        }
        
        private void IncrementScore(object? obj)
        {
            if (this.ActiveGameItemViewModel is null)
                return;

            if (this.ActiveGameItemViewModel.Score == 0 && this.GameTimerViewModel.StartCommand.CanExecute(null))
            {
                this.GameTimerViewModel.StartCommand.Execute(null);
            }
            this.ActiveGameItemViewModel.Score++;
        }

        private bool CanIncrementScore(object? arg)
        {
            if (this.ActiveGameItemViewModel is null)
            {
                return false;
            }

            return this.ActiveGameItemViewModel.Score == 0 ||
                   this.GameTimerViewModel.IsRunning;
        }

        private bool CanNewGame(object? arg)
        {
            return !string.IsNullOrEmpty(this.NewGamePlayerName);
        }

        private void SaveGame(object? obj)
        {
            if (this.ActiveGameItemViewModel != null) 
                this.Games.InsertSorted(this.ActiveGameItemViewModel, GameComp);
            
            this.ActiveGameItemViewModel = null;
        }

        private bool CanSaveGame(object? arg)
        {
            return this.ActiveGameItemViewModel is not null &&
                   !this.GameTimerViewModel.IsRunning;
        }

        private int GetNextGameId()
        {
            return this.Games.Select(g => g.Id).Max() + 1;
        }
    }
}
