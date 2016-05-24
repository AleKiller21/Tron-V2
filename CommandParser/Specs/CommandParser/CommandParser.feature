Feature: CommandParser
	In order to execute the match
	the parser must return the commands parsed

Scenario: Parse a file that doesn't exist
	Given I have entered "supermatch.tb", a path of a non-existent file
	Then the result be a file not found error

Scenario: Parse a file with a wrong extension
	Given I have entered "match.png", a path with the wrong extension
	Then the result be an invalid file extension error

Scenario: Parse a valid match file
	Given I have entered "valid-match.tb", the path of a valid and existent file
	Then the result be the correct list of commands

Scenario: Parse a match file with unexpected token
	Given I have entered "broken-match.tb", the valid path with an unexpected token
	Then the result should be an unexpected token error with the line and row

Scenario: Parse a match file with invalid player move
	Given I have entered "invalid-move-match.tb", the valid path with an invalid player move
	Then the result should be an invalid player move error with the line