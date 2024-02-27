using System.Windows;
using ClickWars2.Data;
using ClickWars2.View;
using ClickWars2.ViewModel;

namespace ClickWars2
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        
        public MainWindow()
        {
            InitializeComponent();

            this._viewModel = new MainViewModel(new GamesViewModel(new GameDataProvider()));

            this.DataContext = this._viewModel;
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await this._viewModel.LoadAsync();
        }
    }
}