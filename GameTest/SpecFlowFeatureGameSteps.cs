using System;
using System.Collections.Generic;
using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace GameTest
{
    [Binding]
    public class SpecFlowFeatureGameSteps
    {
        private readonly GameLogic _logic = new GameLogic();
        private readonly GameOptions _options = new GameOptions(8, 8, "");
        private Player _player;
        private int _playerRow;
        private int _playerColumn;

        [When(@"I set the matrix with (.*) rows and (.*) columns")]
        public void WhenISetTheMatrixWithRowsAndColumns(int rows, int columns)
        {
            _logic.SetMatrix(rows, columns);
        }

        [Then(@"the Matrix should be (.*) rows and (.*) columns")]
        public void ThenTheMatrixShouldBeRowsAndColumns(int rows, int columns)
        {
            Assert.AreEqual(rows, _logic.Rows);
            Assert.AreEqual(columns, _logic.Columns);
        }

        [When(@"I add a new player to the game")]
        public void WhenIAddANewPlayerToTheGame()
        {
            _logic.AddPlayer("Rojo");
        }

        [Then(@"the player should appear in row (.*) and column (.*)")]
        public void ThenThePlayerShouldAppearInRowAndColumn(int row, int col)
        {
            Assert.AreEqual(row, _logic.Players[_logic.Players.Count - 1].Position.Row);
            Assert.AreEqual(col, _logic.Players[_logic.Players.Count - 1].Position.Column);
        }

        [When(@"I search the tag of a non-existent player")]
        public void WhenISearchTheTagOfANon_ExistentPlayer()
        {
            _player = _logic.GetPlayer("Prueba");
        }

        [Then(@"I should get a '(.*)' value")]
        public void ThenIShouldGetAValue(string value)
        {
            if(_player == null)
                Assert.AreEqual(value, "null");
        }

        [When(@"I move a new player '(.*)'")]
        public void WhenIMoveANewPlayerDown(string direction)
        {
            _playerRow = _logic.AddPlayer("rojo").Position.Row;
            _playerColumn = _logic.AddPlayer("rojo").Position.Column;
            _logic.SetMatrix(_options.Rows, _options.Columns);
            _logic.Commands = new List<Command>();
            _logic.Commands.Add(new Command("rojo", direction));
            _logic.ExecuteGame();
        }

        [Then(@"the current player should update its position to row (.*) column (.*)")]
        public void ThenTheCurrentPlayerShouldUpdateItsPositionToRowColumn(int row, int col)
        {
            Player player = _logic.GetPlayer("rojo");
            Assert.AreEqual(row, player.Position.Row);
            Assert.AreEqual(col, player.Position.Column);
        }

        [Given(@"I have created a new player")]
        public void GivenIHaveCreatedANewPlayer()
        {
            _logic.AddPlayer("rojo");
            _logic.SetMatrix(_options.Rows, _options.Columns);
        }

        [When(@"I move the new player '(.*)'")]
        public void WhenIMoveTheNewPlayer(string direction)
        {
            _logic.Commands = new List<Command>();
            _logic.Commands.Add(new Command("rojo", direction));
            _logic.ExecuteGame();
        }

        [Then(@"the matrix should update itself")]
        public void ThenTheMatrixShouldUpdateItself()
        {
            Cell cell = new Cell {Player = _logic.GetPlayer("rojo"), CellActive = true};
            Assert.AreEqual(cell.Player, _logic.Matrix[0, 1].Player);
            Assert.AreEqual(cell.CellActive, _logic.Matrix[0, 1].CellActive);
        }

        [When(@"I move a player '(.*)'")]
        public void WhenIMoveAPlayer(string direction)
        {
            _logic.AddPlayer("rojo");
            _logic.SetMatrix(_options.Rows, _options.Columns);
            _logic.Commands = new List<Command>();
            _logic.Commands.Add(new Command("rojo", direction));
            _logic.Commands.Add(new Command("rojo", "derecha"));
            _logic.ExecuteGame();
        }

        [Then(@"the player's old cell should get disable")]
        public void ThenThePlayerSOldCellShouldGetDisable()
        {
            Player player = _logic.GetPlayer("rojo");
            int row = player.Position.Row;
            int col = player.Position.Column;

            Assert.AreEqual(false, _logic.Matrix[row, col - 1].CellActive);
        }

        [Given(@"I have created a new player '(.*)'")]
        public void GivenIHaveCreatedANewPlayer(string tag)
        {
            _logic.AddPlayer(tag);
            _logic.SetMatrix(_options.Rows, _options.Columns);
        }

        [When(@"I crash player '(.*)' with a border")]
        public void WhenICrashPlayerWithABorder(string tag)
        {
            _logic.Commands = new List<Command>();
            _logic.Commands.Add(new Command(tag, "arriba"));
            _logic.ExecuteGame();
        }

        [Then(@"player '(.*)' should die")]
        public void ThenPlayerShouldDie(string tag)
        {
            Player player = _logic.GetPlayer(tag);
            Assert.AreEqual(false, player.IsAlive);
        }

        [Given(@"I have created a player '(.*)'")]
        public void GivenIHaveCreatedAPlayer(string tag)
        {
            _logic.AddPlayer(tag);
            _logic.SetMatrix(_options.Rows, _options.Columns);
        }

        [When(@"I crash player '(.*)' with his own trail")]
        public void WhenICrashPlayerWithHisOwnTrail(string tag)
        {
            _logic.Commands = new List<Command>();
            _logic.Commands.Add(new Command(tag, "abajo"));
            _logic.Commands.Add(new Command(tag, "derecha"));
            _logic.Commands.Add(new Command(tag, "abajo"));
            _logic.Commands.Add(new Command(tag, "izquierda"));
            _logic.Commands.Add(new Command(tag, "arriba"));
            _logic.ExecuteGame();
        }

        [Then(@"player '(.*)' should be dead")]
        public void ThenPlayerShouldBeDead(string tag)
        {
            Player player = _logic.GetPlayer(tag);
            Assert.AreEqual(false, player.IsAlive);
        }

        [Given(@"I have created player '(.*)'")]
        public void GivenIHaveCreatedPlayer(string tag)
        {
            _logic.AddPlayer(tag);
            _logic.SetMatrix(_options.Rows, _options.Columns);
        }

        [Given(@"I have also created player '(.*)'")]
        public void GivenIHaveAlsoCreatedPlayer(string tag)
        {
            _logic.AddPlayer(tag);
        }

        [When(@"I crash player '(.*)' with player '(.*)' trail")]
        public void WhenICrashPlayerWithPlayerTrail(string p1, string p2)
        {
            _logic.Commands = new List<Command>();
            _logic.Commands.Add(new Command(p2, "derecha"));
            _logic.Commands.Add(new Command(p2, "derecha"));
            _logic.Commands.Add(new Command(p2, "derecha"));
            _logic.Commands.Add(new Command(p1, "abajo"));
            _logic.Commands.Add(new Command(p1, "derecha"));
            _logic.Commands.Add(new Command(p1, "derecha"));
            _logic.Commands.Add(new Command(p1, "arriba"));
            _logic.ExecuteGame();
        }

        [Then(@"player '(.*)' must die")]
        public void ThenPlayerMustDie(string tag)
        {
            Player player = _logic.GetPlayer(tag);
            Assert.AreEqual(false, player.IsAlive);
        }

        [When(@"I crash player '(.*)' with player '(.*)'")]
        public void WhenICrashPlayerWithPlayer(string p1, string p2)
        {
            _logic.Commands = new List<Command>();
            _logic.Commands.Add(new Command(p2, "derecha"));
            _logic.Commands.Add(new Command(p2, "derecha"));
            _logic.Commands.Add(new Command(p2, "derecha"));
            _logic.Commands.Add(new Command(p1, "abajo"));
            _logic.Commands.Add(new Command(p1, "derecha"));
            _logic.Commands.Add(new Command(p1, "derecha"));
            _logic.Commands.Add(new Command(p1, "derecha"));
            _logic.Commands.Add(new Command(p1, "arriba"));
            _logic.ExecuteGame();
        }

        [Then(@"both player '(.*)' and player '(.*)' must die")]
        public void ThenBothPlayerAndPlayerMustDie(string p1, string p2)
        {
            Player playerAzul = _logic.GetPlayer(p1);
            Player playerRojo = _logic.GetPlayer(p2);
            Assert.AreEqual(false, playerAzul.IsAlive);
            Assert.AreEqual(false, playerRojo.IsAlive);
        }

    }
}
