CS426 Assignment 6

Group 19 

Have You Tried Turning It Off and On Again?

Matthew Mohaupt, Scott Hong, MaryJo Santos


New shaders:
	Implemented 4 different shader objects
	- Made the trash cans use toon shader
	- Made the shelves use toon shaders
	- Made tables use toon shaders
	- Made the cable spools use toon shaders

	The idea being is that these objects are all objects that used for the maze like level design and are used to have a different shader than than the perimeter objects as those are meant make the perimeter of the play area. These toon shader objects make up the obstacles in an office setting that can block off the player so are needed to be navigated around so a highlight on them is used to emphasize the level design.


Alpha release fixes:

	MaryJo -> implemented a sound effect that when you fix a computer so you know if you did it correctly

	Scott -> Made a title, how to play, credits gui screens so the player has an easier time understanding the game. Made the toon shader code.

	Matthew -> Fixed a bug that caused npc's to not despawn their pc after leaving when not solved in time. This fix also solves another bug where the player could fix multiple pc's at the same time if they all used the same object as now there is only one pc at play at a time. Made the level design and made the obstacles in the area so forced to move around and not a big open map. Implemented the toon shader in the objects