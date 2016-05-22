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

@mytag
Scenario: Look for player that doesn't exist
	When I search the tag of a non-existent player
	Then I should get a 'null' value

@mytag
Scenario: Move player
	When I move a new player 'derecha'
	Then the current player should update its position to row 0 column 1

@mytag
Scenario: Update the matrix
	Given I have created a new player
	When I move the new player 'derecha'
	Then the matrix should update itself

@mytag
Scenario: Disable old player cell
	When I move a player 'abajo'
	Then the player's old cell should get disable