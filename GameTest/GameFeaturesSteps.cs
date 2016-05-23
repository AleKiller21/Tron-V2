using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Command = CommandParser.Command;
using PlayerMoves = CommandParser.PlayerMoves;

namespace GameTest
{
    [Binding]
    public class GameFeaturesSteps
    {
        GameLogic logic  = new GameLogic();
        private Player testPlayer;

        [When(@"I set the matrix with (.*) rows and (.*) columns")]
        public void WhenISetTheMatrixWithRowsAndColumns(int p0, int p1)
        {
            logic.SetMatrix(8, 8);
        }

        [Then(@"the Matrix should be (.*) rows and (.*) columns")]
        public void ThenTheMatrixShouldBeRowsAndColumns(int rows, int cols)
        {
            Assert.AreEqual(8, rows);
            Assert.AreEqual(8, cols);
        }

        [When(@"I add a new player to the game")]
        public void WhenIAddANewPlayerToTheGame()
        {
            logic.SetMatrix(8, 8);
            logic.AddFixedPlayer("red", 0, 0);
        }

        [Then(@"the player should appear in a random position like row (.*) and column (.*)")]
        public void ThenThePlayerShouldAppearInARandomPositionLikeRowAndColumn(int row, int col)
        {
            Assert.AreEqual(row, logic.GetPlayer("red").Position.Row);
            Assert.AreEqual(col, logic.GetPlayer("red").Position.Column);
        }

        [When(@"I search the tag of a non-existent player")]
        public void WhenISearchTheTagOfANon_ExistentPlayer()
        {
            testPlayer = logic.GetPlayer("Goku");
        }

        [Then(@"I should get a null value")]
        public void ThenIShouldGetAValue()
        {
            Assert.AreEqual(null, testPlayer);
        }

        [When(@"I move a new player right")]
        public void WhenIMoveANewPlayerRight()
        {
            logic.SetMatrix(8, 8);
            logic.AddFixedPlayer("red", 0, 0);
            logic.Commands = new List<Command>();
            logic.Commands.Add(new Command {Direction = PlayerMoves.Right, Tag = "red"});
            logic.ExecuteGame();
        }

        [Then(@"the current player should update its position to row (.*) column (.*)")]
        public void ThenTheCurrentPlayerShouldUpdateItsPositionToRowColumn(int row, int col)
        {
            Position position = logic.GetPlayer("red").Position;

            Assert.AreEqual(row, position.Row);
            Assert.AreEqual(col, position.Column);
        }

        [Given(@"I have created a new player")]
        public void GivenIHaveCreatedANewPlayer()
        {
            logic.SetMatrix(8, 8);
            logic.AddFixedPlayer("red", 0, 0);
        }

        [When(@"I move the new player right (.*)")]
        public void WhenIMoveTheNewPlayerRight(int p0)
        {
            logic.Commands = new List<Command>();
            logic.Commands.Add(new Command { Direction = PlayerMoves.Right, Tag = "red" });
            logic.ExecuteGame(); ;
        }

        [Then(@"the matrix should update itself")]
        public void ThenTheMatrixShouldUpdateItself()
        {
            Position position = logic.GetPlayer("red").Position;

            Assert.AreEqual("red", logic.Matrix[position.Row, position.Column].Player.Tag);
        }

        [When(@"I move a player down")]
        public void WhenIMoveAPlayerDown()
        {
            logic.SetMatrix(8, 8);
            logic.AddFixedPlayer("red", 0, 0);
            logic.Commands = new List<Command>();
            logic.Commands.Add(new Command { Direction = PlayerMoves.Down, Tag = "red" });
            logic.ExecuteGame();
        }

        [Then(@"the player's old cell should get disable")]
        public void ThenThePlayerSOldCellShouldGetDisable()
        {
            Position position = logic.GetPlayer("red").Position;

            Assert.AreEqual(false, logic.Matrix[position.Row - 1, position.Column].CellActive);
        }

        [Given(@"I have created a new player '(.*)'")]
        public void GivenIHaveCreatedANewPlayer(string player)
        {
            logic.SetMatrix(8, 8);
            logic.AddFixedPlayer(player, 0, 0);
        }

        [When(@"I crash player '(.*)' with a border")]
        public void WhenICrashPlayerWithABorder(string player)
        {
            logic.Commands = new List<Command>();
            logic.Commands.Add(new Command { Direction = PlayerMoves.Up, Tag = player });
            logic.ExecuteGame();
        }

        [Then(@"player '(.*)' should die")]
        public void ThenPlayerShouldDie(string player)
        {
            Assert.AreEqual(false, logic.GetPlayer(player).IsAlive);
        }

        [Given(@"I have created a player '(.*)'")]
        public void GivenIHaveCreatedAPlayer(string player)
        {
            logic.SetMatrix(8, 8);
            logic.AddFixedPlayer(player, 0, 0);
        }

        [When(@"I crash player '(.*)' with his own trail")]
        public void WhenICrashPlayerWithHisOwnTrail(string player)
        {
            logic.Commands = new List<Command>();
            logic.Commands.Add(new Command { Direction = PlayerMoves.Right, Tag = player });
            logic.Commands.Add(new Command { Direction = PlayerMoves.Down, Tag = player });
            logic.Commands.Add(new Command { Direction = PlayerMoves.Left, Tag = player });
            logic.Commands.Add(new Command { Direction = PlayerMoves.Up, Tag = player });
            logic.ExecuteGame();
        }

        [Then(@"player '(.*)' should be dead")]
        public void ThenPlayerShouldBeDead(string player)
        {
            Assert.AreEqual(false, logic.GetPlayer(player).IsAlive);
        }

        [Given(@"I have created player '(.*)'")]
        public void GivenIHaveCreatedPlayer(string red)
        {
            logic.SetMatrix(8, 8);
            logic.AddFixedPlayer(red, 0, 0);
        }

        [Given(@"I have also created player '(.*)'")]
        public void GivenIHaveAlsoCreatedPlayer(string blue)
        {
            logic.SetMatrix(8, 8);
            logic.AddFixedPlayer(blue, 1, 0);
        }

        [When(@"I crash player '(.*)' with player '(.*)' trail")]
        public void WhenICrashPlayerWithPlayerTrail(string blue, string red)
        {
            logic.Commands = new List<Command>();
            logic.Commands.Add(new Command { Direction = PlayerMoves.Right, Tag = red });
            logic.Commands.Add(new Command { Direction = PlayerMoves.Right, Tag = red });
            logic.Commands.Add(new Command { Direction = PlayerMoves.Right, Tag = blue });
            logic.Commands.Add(new Command { Direction = PlayerMoves.Up, Tag = blue });
            logic.ExecuteGame();
        }

        [Then(@"player '(.*)' must die")]
        public void ThenPlayerMustDie(string blue)
        {
            Assert.AreEqual(false, logic.GetPlayer(blue).IsAlive);
        }

        [When(@"I crash player '(.*)' with player '(.*)'")]
        public void WhenICrashPlayerWithPlayer(string blue, string red)
        {
            logic.Commands = new List<Command>();
            logic.Commands.Add(new Command { Direction = PlayerMoves.Right, Tag = red });
            logic.Commands.Add(new Command { Direction = PlayerMoves.Right, Tag = blue });
            logic.Commands.Add(new Command { Direction = PlayerMoves.Up, Tag = blue });
            logic.ExecuteGame();
        }

        [Then(@"both player '(.*)' and player '(.*)' must die")]
        public void ThenBothPlayerAndPlayerMustDie(string blue, string red)
        {
            Assert.AreEqual(false, logic.GetPlayer(blue).IsAlive);
            Assert.AreEqual(false, logic.GetPlayer(red).IsAlive);
        }


    }
}
