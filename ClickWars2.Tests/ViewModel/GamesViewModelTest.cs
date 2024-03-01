using ClickWars2.Data;
using ClickWars2.ViewModel;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClickWars2.Tests.ViewModel;

[TestClass]
[TestSubject(typeof(GamesViewModel))]
public class GamesViewModelTest
{

    [TestMethod]
    public void NewGameTest()
    {
        var viewModel = new GamesViewModel(new GameDataProvider());
        viewModel.LoadAsync().Wait();
        
        Assert.IsTrue(viewModel.NotGaming);
        Assert.IsFalse(viewModel.NewGameCommand.CanExecute(null));

        viewModel.NewGamePlayerName = "Test";
        
        Assert.IsTrue(viewModel.NewGameCommand.CanExecute(null));
        
        viewModel.NewGameCommand.Execute(null);
        
        Assert.IsTrue(viewModel.Gaming);
        Assert.IsFalse(viewModel.NewGameCommand.CanExecute(null));
        Assert.IsTrue(viewModel.IncrementScoreCommand.CanExecute(null));
    }
}