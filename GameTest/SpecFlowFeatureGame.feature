Feature: SpecFlowFeatureGame
	In order to allow others to play with the Tron game
	As a programmer
	I want to build all the logic components of the game

@mytag
Scenario: Set rows and columns for matrix
	When I set the matrix with 8 rows and 8 columns
	Then the Matrix should be 8 rows and 8 columns

@mytag
Scenario: Add new player
	When I add a new player to the game
	Then the player should appear in row 0 and column 0