using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace CommandParser.Specs.CommandParser
{
    [Binding]
    public class CommandParserSteps
    {
        private readonly global::CommandParser.CommandParser _parser = new global::CommandParser.CommandParser();
        private string _nonExistentPath, _wrongExtensionPath, _validPath, _brokenPath, _invalidMovePath;
        private bool _threwFileNotFound, _threwInvalidExtension, _parseFailed, _threwUnexpectedToken, _threwInvalidMove;
        private List<Command> _commands;

        private const string MockDirectory =
            @"C:\Users\Juan\Desktop\";

        [Given(@"I have entered ""(.*)"", a path of a non-existent file")]
        public void GivenIHaveEnteredAPathOfANon_ExistentFile(string p0)
        {
            _nonExistentPath = p0;
        }

        [Given(@"I have entered ""(.*)"", a path with the wrong extension")]
        public void GivenIHaveEnteredAPathWithTheWrongExtension(string p0)
        {
            _wrongExtensionPath = p0;
        }

        [Given(@"I have entered ""(.*)"", the path of a valid and existent file")]
        public void GivenIHaveEnteredThePathOfAValidAndExistentFile(string p0)
        {
            _validPath = MockDirectory + p0;
        }

        [Given(@"I have entered ""(.*)"", the valid path with an unexpected token")]
        public void GivenIHaveEnteredTheValidPathWithAnUnexpectedToken(string p0)
        {
            _brokenPath = MockDirectory +  p0;
        }

        [Given(@"I have entered ""(.*)"", the valid path with an invalid player move")]
        public void GivenIHaveEnteredTheValidPathWithAnInvalidPlayerMove(string p0)
        {
            _invalidMovePath = MockDirectory + p0;
        }
        
        [Then(@"the result be a file not found error")]
        public void ThenTheResultBeAFileNotFoundError()
        {
            try
            {
                _parser.Parse(_nonExistentPath);
            }
            catch (FileNotFoundException)
            {
                _threwFileNotFound = true;
            }

            Assert.IsTrue(_threwFileNotFound);
        }
        
        [Then(@"the result be an invalid file extension error")]
        public void ThenTheResultBeAnInvalidFileExtensionError()
        {
            try
            {
                _parser.Parse(_wrongExtensionPath);
            }
            catch (InvalidFileExtensionException)
            {
                _threwInvalidExtension = true;
            }

            Assert.IsTrue(_threwInvalidExtension);
        }
        
        [Then(@"the result be the correct list of commands")]
        public void ThenTheResultBeTheCorrectListOfCommands()
        {
            try
            {
                _commands = _parser.Parse(_validPath);

                var correctCommands = new List<Command>()
                {
                    new Command()
                    {
                        Tag = "red",
                        Direction = PlayerMoves.Up
                    },
                    new Command()
                    {
                        Tag = "blue",
                        Direction = PlayerMoves.Up
                    },
                    new Command()
                    {
                        Tag = "red",
                        Direction = PlayerMoves.Down
                    },
                    new Command()
                    {
                        Tag = "red",
                        Direction = PlayerMoves.Down
                    },
                    new Command()
                    {
                        Tag = "blue",
                        Direction = PlayerMoves.Left
                    },
                    new Command()
                    {
                        Tag = "blue",
                        Direction = PlayerMoves.Right
                    },
                    new Command()
                    {
                        Tag = "red",
                        Direction = PlayerMoves.Left
                    },
                    new Command()
                    {
                        Tag = "red",
                        Direction = PlayerMoves.Left
                    },
                    new Command()
                    {
                        Tag = "red",
                        Direction = PlayerMoves.Down
                    },
                    new Command()
                    {
                        Tag = "red",
                        Direction = PlayerMoves.Right
                    },
                    new Command()
                    {
                        Tag = "red",
                        Direction = PlayerMoves.Right
                    },
                    new Command()
                    {
                        Tag = "red",
                        Direction = PlayerMoves.Up
                    },
                    new Command()
                    {
                        Tag = "blue",
                        Direction = PlayerMoves.Right
                    },
                    new Command()
                    {
                        Tag = "blue",
                        Direction = PlayerMoves.Right
                    },
                    new Command()
                    {
                        Tag = "blue",
                        Direction = PlayerMoves.Down
                    },
                    new Command()
                    {
                        Tag = "blue",
                        Direction = PlayerMoves.Down
                    }
                };

                if (_commands.Count != correctCommands.Count)
                    _parseFailed = true;

                else
                    CheckIfCommandsAreParsedCorrectly(correctCommands);

            }
            catch (Exception)
            {
                _parseFailed = true;
            }

            Assert.IsFalse(_parseFailed);
        }

        private void CheckIfCommandsAreParsedCorrectly(List<Command> correctCommands)
        {
            for (var i = 0; i < correctCommands.Count; i++)
            {
                var correctCommand = correctCommands[i];
                var command = _commands[i];

                if (command.Tag == correctCommand.Tag && command.Direction.ToString() == correctCommand.Direction.ToString())
                    continue;

                _parseFailed = true;
                break;
            }
        }

        [Then(@"the result should be an unexpected token error with the line and row")]
        public void ThenTheResultShouldBeAnUnexpectedTokenErrorWithTheLineAndRow()
        {
            try
            {
                _parser.Parse(_brokenPath);
            }
            catch (UnexpectedTokenException)
            {
                _threwUnexpectedToken = true;
            }

            Assert.IsTrue(_threwUnexpectedToken);
        }
        
        [Then(@"the result should be an invalid player move error with the line")]
        public void ThenTheResultShouldBeAnInvalidPlayerMoveErrorWithTheLine()
        {
            try
            {
                _parser.Parse(_invalidMovePath);
            }
            catch (InvalidMoveException)
            {
                _threwInvalidMove = true;
            }

            Assert.IsTrue(_threwInvalidMove);
        }
    }
}
