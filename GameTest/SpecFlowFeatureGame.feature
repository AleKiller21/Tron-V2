﻿Feature: SpecFlowFeatureGame
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

@mytag
Scenario: Collision with border
	Given I have created a new player 'rojo'
	When I crash player 'rojo' with a border
	Then player 'rojo' should die
	
@mytag
Scenario: Collision with one's trail
	Given I have created a player 'rojo'
	When I crash player 'rojo' with his own trail
	Then player 'rojo' should be dead

@mytag
Scenario: Collision with other player's trail
	Given I have created player 'rojo'
	And I have also created player 'azul'
	When I crash player 'azul' with player 'rojo' trail
	Then player 'azul' must die

@mytag
Scenario: Collision with other player
	Given I have created player 'rojo'
	And I have also created player 'azul'
	When I crash player 'azul' with player 'rojo'
	Then both player 'azul' and player 'rojo' must die