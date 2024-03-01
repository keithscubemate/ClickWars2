using ClickWars2.Model;
using ClickWars2.ViewModel;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClickWars2.Tests.ViewModel;

[TestClass]
[TestSubject(typeof(GameItemViewModel))]
public class GameItemViewModelTest
{

    [TestMethod]
    public void IdTest()
    {
        const int newId = 456;
        var game = new Game(0, "Name", 0);
        var viewModel = new GameItemViewModel(game);

        viewModel.PropertyChanged += (_, e) =>
        {
            Assert.AreEqual("Id", e.PropertyName);
            Assert.AreEqual(newId, viewModel.Id);
            Assert.AreEqual(newId, game.Id);
        };

        viewModel.Id = newId;
    }
    
    [TestMethod]
    public void PlayerNameTest()
    {
        const string newPlayerName = "Austin";
        var game = new Game(0, "Name", 0);
        var viewModel = new GameItemViewModel(game);

        viewModel.PropertyChanged += (_, e) =>
        {
            Assert.AreEqual("PlayerName", e.PropertyName);
            Assert.AreEqual(newPlayerName, viewModel.PlayerName);
            Assert.AreEqual(newPlayerName, game.PlayerName);
        };

        viewModel.PlayerName = newPlayerName;
    }
    
    [TestMethod]
    public void ScoreTest()
    {
        const uint newScore = 100;
        var game = new Game(0, "Name", 0);
        var viewModel = new GameItemViewModel(game);

        viewModel.PropertyChanged += (_, e) =>
        {
            Assert.AreEqual("Score", e.PropertyName);
            Assert.AreEqual(newScore, viewModel.Score);
            Assert.AreEqual(newScore, game.Score);
        };

        viewModel.Score = newScore;
    }
    
    [TestMethod]
    public void ToGameTest()
    {
        var game = new Game(0, "Name", 0);
        var viewModel = new GameItemViewModel(game);

        Assert.AreEqual(game, viewModel.ToGame());
    }
}