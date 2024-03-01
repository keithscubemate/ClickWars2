using ClickWars2.Model;

namespace ClickWars2.ViewModel
{
    public class GameItemViewModel : ViewModelBase
    {
        // Private fields
        private readonly Game _model;

        // Properties
        public int Id
        {
            get => this._model.Id;
            set
            {
                this._model.Id = value;
                this.RaisePropertyChanged();
            }
        }

        public string? PlayerName
        {
            get => this._model.PlayerName;
            set
            {
                this._model.PlayerName = value;
                this.RaisePropertyChanged();
            }
        }

        public uint Score
        {
            get => this._model.Score;
            set
            {
                this._model.Score = value;
                this.RaisePropertyChanged();
            }
        }

        // Constructor
        public GameItemViewModel(Game model)
        {
            this._model = model;
        }

        public Game ToGame()
        {
            return _model;
        }
    }
}
