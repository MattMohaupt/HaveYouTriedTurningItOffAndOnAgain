CS426 Assignment 7

Group 19 

Have You Tried Turning It Off and On Again?

Matthew Mohaupt, Scott Hong, MaryJo Santos

How to play:

	Controls:
		- W key to move forward
		- S key to move backwards
		- A key to strafe left
		- D key to strafe right
		- Mouse Movement for camera controls
		- E key to pick up a carriable object
		- F key to interact with interactable object

	Objective:
		To make as much money as possible before the minute 30 timer is up. You need to fix the pcs and that will give you $100 per pc fixed. Red pcs have hardware related issues so they need to be interacted with while holding the wrench&screw, the blue pcs have software related issues so they need to be interacted with while holding the battery.


UI Design:

	Problems:
		-Unclear navigation
			The basic structure of the game is identical to the networking demo we experienced in class. We used a networking manager so that the player can host the networking session and clients can freely join that session by typing join code to the text field. However, since our initial version of the game did not have any instruction or explanation for such a process, this is not going to be clear for those who are playing this game for the first time. 
		-Weak color contrast
			All of the user interface components in this version of the game have white color, which is considerably similar to the brightness and colorfulness of the floor and wall. It is very likely that the player will have a hard time recognizing the user interface. 
		-Aesthetically poor design
			The UI looks too basic and simple. Well-designed colors, shapes, and fonts have power to clearly represent the overall concept of the game. Even though sometimes a very simple design UI fits with the atmosphere of the products, our initial UI does not really seem to harmonize with a concept of our game. 

	Changes:
		-In order to navigate the player what to do when they first open the game, we added a title screen with informative buttons such as Play, How to Play, and Quit.
		-If you press the play button, then it will lead you to the Host and Client button, clearly delivering how to start the game. In-game user interface also contains significant information that the player has to know while playing the game.
		-Color contrast between background and texts are all clearly distinct for both title screen and in-game screen. Title screen has dim color on the background image and lighter color on the texts
		-In game is exactly vice-versa.
		-We used simple and intuitive color schemes with light skin color and navy color to ensure the atmosphere of the game consisted throughout the game experience.


Sound Design:
	
	Sounds in Overcooked (observed):
		-Background music + ambient sounds that match the current setting
			Plays during the level, cuts out immediately once the timer runs out
		-Picking up and dropping food 
			Happens every time a player picks up/drops any food: ingredients and dishes
		-Stations for prepping food (i.e. chopping stations and ovens)
			Happens when interacting with the station
			continuous / loops until food has been fully prepared at that station
		-Successful / unsuccessful meal delivery
			Different sounds for delivering correct meals and incorrect meals
		-timers running out
			Happens either when food is about to burn or when the level is starting to finish
		-End of level bell
			All sounds cut out as this plays
		-Score display screen
			Happens after the current level 

Sounds to put into our game:
	-Background music (approved)
	-Walking (rejected)
	-Doorbell (rejected)
	-Fixing computer (approved)
	-Successful Computer Fix (approved)
	-Unsuccessful computer fix (approved)
	-Wrong tool usage (approved)
	-Score Screen (approved)

	We decided to reject the walking and doorbell sounds because they became too repetitive as the level went on and didn’t really add much to the game. For the sounds we approved, many of them were approved because they let the player know whether or not they are playing the game correctly


Gameplay:
	The game is only a minute 30 so the game is played at a high stress level needing to use quick snapshot decisions in order to maximize profit. The winding area map forces users to move around the maze like structure. The fact that NPC's will remove their pc's with not completed within 30 seconds further adds to the stress of the game as each pc is needed to be fixed quickly and efficiently.




