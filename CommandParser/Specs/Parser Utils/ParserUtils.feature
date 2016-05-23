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
	When I call the function
	Then it should return the correct extension