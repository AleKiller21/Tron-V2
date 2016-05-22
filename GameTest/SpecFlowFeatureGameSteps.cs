using System;
using System.Collections.Generic;
using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Game;

namespace GameTest
{
    [Binding]
    public class SpecFlowFeatureGameSteps
    {
        private GameLogic logic = new GameLogic();
        private GameOptions options = new GameOptions(8, 8, "");
        private Player player;
        private int playerRow;
        private int playerColumn;

        [When(@"I set the matrix with (.*) rows and (.*) columns")]
        public void WhenISetTheMatrixWithRowsAndColumns(int rows, int columns)
        {
            logic.SetMatrix(rows, columns);
        }

        [Then(@"the Matrix should be (.*) rows and (.*) columns")]
        public void ThenTheMatrixShouldBeRowsAndColumns(int rows, int columns)
        {
            Assert.AreEqual(rows, logic.Rows);
            Assert.AreEqual(columns, logic.Columns);
        }

        [When(@"I add a new player to the game")]
        public void WhenIAddANewPlayerToTheGame()
        {
            logic.AddPlayer("Rojo");
        }

        [Then(@"the player should appear in row (.*) and column (.*)")]
        public void ThenThePlayerShouldAppearInRowAndColumn(int row, int col)
        {
            Assert.AreEqual(row, logic.Players[logic.Players.Count - 1].Position.Row);
            Assert.AreEqual(col, logic.Players[logic.Players.Count - 1].Position.Column);
        }

        [When(@"I search the tag of a non-existent player")]
        public void WhenISearchTheTagOfANon_ExistentPlayer()
        {
            player = logic.GetPlayer("Prueba");
        }

        [Then(@"I should get a '(.*)' value")]
        public void ThenIShouldGetAValue(string value)
        {
            if(player == null)
                Assert.AreEqual(value, "null");
        }

        [When(@"I move a new player '(.*)'")]
        public void WhenIMoveANewPlayerDown(string direction)
        {
            playerRow = logic.AddPlayer("rojo").Position.Row;
            playerColumn = logic.AddPlayer("rojo").Position.Column;
            logic.SetMatrix(options.Rows, options.Columns);
            logic.Commands = new List<Command>();
            logic.Commands.Add(new Command("rojo", direction));
            logic.ExecuteGame();
        }

        [Then(@"the current player should update its position to row (.*) column (.*)")]
        public void ThenTheCurrentPlayerShouldUpdateItsPositionToRowColumn(int row, int col)
        {
            Player player = logic.GetPlayer("rojo");
            Assert.AreEqual(row, player.Position.Row);
            Assert.AreEqual(col, player.Position.Column);
        }

        [Given(@"I have created a new player")]
        public void GivenIHaveCreatedANewPlayer()
        {
            logic.AddPlayer("rojo");
            logic.SetMatrix(options.Rows, options.Columns);
        }

        [When(@"I move the new player '(.*)'")]
        public void WhenIMoveTheNewPlayer(string direction)
        {
            logic.Commands = new List<Command>();
            logic.Commands.Add(new Command("rojo", direction));
            logic.ExecuteGame();
        }

        [Then(@"the matrix should update itself")]
        public void ThenTheMatrixShouldUpdateItself()
        {
            Cell cell = new Cell {Player = logic.GetPlayer("rojo"), CellActive = true};
            Assert.AreEqual(cell.Player, logic.Matrix[0, 1].Player);
            Assert.AreEqual(cell.CellActive, logic.Matrix[0, 1].CellActive);
        }

        [When(@"I move a player '(.*)'")]
        public void WhenIMoveAPlayer(string direction)
        {
            logic.AddPlayer("rojo");
            logic.SetMatrix(options.Rows, options.Columns);
            logic.Commands = new List<Command>();
            logic.Commands.Add(new Command("rojo", direction));
            logic.Commands.Add(new Command("rojo", "derecha"));
            logic.ExecuteGame();
        }

        [Then(@"the player's old cell should get disable")]
        public void ThenThePlayerSOldCellShouldGetDisable()
        {
            Player player = logic.GetPlayer("rojo");
            int row = player.Position.Row;
            int col = player.Position.Column;

            Assert.AreEqual(false, logic.Matrix[row, col - 1].CellActive);
        }

    }
}
