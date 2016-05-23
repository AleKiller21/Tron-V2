using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace CommandParser.Specs.Parser_Utils
{
    [Binding]
    public class ParserUtilsSteps
    {
        private readonly List<string> _paths = new List<string>();
        private readonly List<string> _extensions = new List<string>();

        [Given(@"I have entered the following paths")]
        public void GivenIHaveEnteredTheFollowingPaths(Table table)
        {
            foreach (var row in table.Rows)
                _paths.Add(row.Values.First());
        }

        [When(@"I call the function")]
        public void WhenICallTheFunction()
        {
            foreach (var path in _paths)
                _extensions.Add(global::CommandParser.ParserUtils.GetPathExtension(path));
        }


        [Then(@"it should return the correct extension")]
        public void ThenItShouldReturnTheCorrectExtension()
        {
            Assert.AreEqual(_extensions[0], "tb");
            Assert.AreEqual(_extensions[1], "tb");
            Assert.AreEqual(_extensions[2], "png");
            Assert.AreEqual(_extensions[3], "py");
            Assert.AreEqual(_extensions[4], "");
            Assert.AreEqual(_extensions[5], "tb");
            Assert.AreEqual(_extensions[6], "");
            Assert.AreEqual(_extensions[7], "");
        }
    }
}
