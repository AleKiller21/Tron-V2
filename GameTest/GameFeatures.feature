Feature: GameFeatures
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
	Then the player should appear in a random position like row 0 and column 0

@mytag
Scenario: Look for player that doesn't exist
	When I search the tag of a non-existent player
	Then I should get a null value

@mytag
Scenario: Move player
	When I move a new player right
	Then the current player should update its position to row 0 column 1

@mytag
Scenario: Update the matrix
	Given I have created a new player
	When I move the new player right 3
	Then the matrix should update itself

@mytag
Scenario: Disable old player cell
	When I move a player down
	Then the player's old cell should get disable

@mytag
Scenario: Collision with border
	Given I have created a new player 'red'
	When I crash player 'red' with a border
	Then player 'red' should die

@mytag
Scenario: Collision with one's trail
	Given I have created a player 'red'
	When I crash player 'red' with his own trail
	Then player 'red' should be dead

@mytag
Scenario: Collision with other player's trail
	Given I have created player 'red'
	And I have also created player 'blue'
	When I crash player 'blue' with player 'red' trail
	Then player 'blue' must die

@mytag
Scenario: Collision with other player
	Given I have created player 'red'
	And I have also created player 'blue'
	When I crash player 'blue' with player 'red'
	Then both player 'blue' and player 'red' must die