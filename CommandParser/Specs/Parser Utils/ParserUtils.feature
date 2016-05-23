Feature: ParserUtils
	It performs basic, reusable tasks for the command parser

Scenario: Get a path extension
	Given I have entered the following paths
	|           Path              |
	| Desktop/match.tb            |
	| match.tb                    |
	| match.png                   |
	| math.py                     |
	| hey                         |
	| Documents/My Folder/file.tb |
	| Desktop/match               |
	|                             |
	When I submit a match file
	Then it should return the correct extension

Scenario: An incorrect path extension has been entered
	Given I have entered the following invalid paths
	|           Path              |
	| match.png                   |
	| math.py                     |
	| hey                         |
	| Desktop/match               |
	|                             |
	When I submit a match file
	Then it should display an error message

Scenario: A correct path extension has been entered
	Given I have entered the following valid paths
	|           Path              |
	| match.tb                    |
	| Desktop/math.tb             |
	| hey.tb                      |
	| Desktop/Docs/hm.tb          |
	| a.tb                        |
	When I submit a match file
	Then it should accept parse the file