Feature: GameLogic
	In order to allow others to play with the Tron game
	As a programmer
	I want to build all the logic components of the game

@mytag
Scenario: Add two numbers
	Given I have entered 50 into the calculator
	And I have also entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen