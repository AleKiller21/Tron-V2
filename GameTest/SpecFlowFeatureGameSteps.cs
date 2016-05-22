using System;
using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Game;

namespace GameTest
{
    [Binding]
    public class SpecFlowFeatureGameSteps
    {
        GameLogic logic = new GameLogic();

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



    }
}
