using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace CommandParser.Specs.Parser_Utils
{
    [Binding]
    public class ParserUtilsSteps
    {
        private List<string> _paths;
        private List<string> _invalidPaths;
        private List<string> _validPaths; 
        private readonly List<string> _extensions = new List<string>();
        private bool _invalidPathDidNotThrowExpception;
        private bool _validPathThrewException;

        [Given(@"I have entered the following paths")]
        public void GivenIHaveEnteredTheFollowingPaths(Table table)
        {
            _paths = CreateSet(table);
        }

        [When(@"I submit a match file")]
        public void WhenISubmitAMatchFile()
        {
            foreach (var path in _paths)
                _extensions.Add(global::CommandParser.ParserUtils.GetPathExtension(path));

            foreach (var invalidPath in _invalidPaths)
            {
                try
                {
                    global::CommandParser.ParserUtils.IsAValidFile(invalidPath);
                    _invalidPathDidNotThrowExpception = true;
                    break;
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            foreach (var validPath in _validPaths)
            {
                try
                {
                    global::CommandParser.ParserUtils.IsAValidFile(validPath);
                }
                catch (Exception)
                {
                    _validPathThrewException = true;
                    break;
                }
            }
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

        [Given(@"I have entered the following invalid paths")]
        public void GivenIHaveEnteredTheFollowingInvalidPaths(Table table)
        {
            _invalidPaths = CreateSet(table);
        }

        [Then(@"it should display an error message")]
        public void ThenItShouldDisplayAnErrorMessage()
        {
            Assert.IsFalse(_invalidPathDidNotThrowExpception);
        }

        [Given(@"I have entered the following valid paths")]
        public void GivenIHaveEnteredTheFollowingValidPaths(Table table)
        {
            _validPaths = CreateSet(table);
        }

        [Then(@"it should accept parse the file")]
        public void ThenItShouldAcceptParseTheFile()
        {
            Assert.IsFalse(_validPathThrewException);
        }

        private List<string> CreateSet(Table table)
        {
            var rows = table.Rows;

            return rows.Select(row => row.Values.First()).ToList();
        }
    }
}
