Mini-Project

Project Name: Portal Without the portals
Name: Mark Louis Albrecht Gerstrup
Link to Project: https://github.com/ContaDoge/3D1.git 

Overview of the Game:
The idea of the project is a physics-based puzzle game similar to portal. The player solves puzzles or challenges by utilizing the movement system and the interact ability. By interacting with an object by pressing e, you can either pickup and drop the object, or throw it. The aim of the game is to progress through levels using the interact mechanic to solve puzzles and challenges. The game gets progressively harder with level 1 being an extremely simple puzzle and level 2 being more of a challenge.
The main parts of the game are:
•	Player – moved with the keyboard WASD or arrow keys. Crouching with left control and sprinting with left shift.
•	Camera – The camera is a first-person view
•	Pickup able objects – Thees objects are the only ones that can be interacted with.
•	Buttons - These blue buttons allow the player to progress through the levels. If a pickup able object is placed on the blue button a obstacle disappears.
Game features:
•	Movement abilities such as sprinting and crouching
•	The interact button (e)
•	Death zone – By entering this area the player respawns
How were the Different Parts of the Course Utilized:
The project implements an interactive camera system that allows users to control the view using inputs from the keyboard and mouse. A player movement system enables direct interaction with the character or objects, allowing for movement such as jumping, crouching and sprinting. It also allows actions such as grabbing, throwing, or pushing objects. The environment includes interactive objects that respond indirectly or directly to user actions, such as being picked up or manipulated through collisions or raycasting. Scripted behavior informs the movement of objects such as platforms. Lighting effects such as materials that emit real time light and general directional light. Physics interactions are implemented through rigidbodies, forces, and raycasting to simulate object movement and collisions. Levels are created using ProBuilder to give more options while level designing. The only GUI element is a progress bar that show how charged up the throw force of the object your currently holding is. Lastly the Unity particle system is used for smoke effects.
Project Parts:
•	Scripts:
o	ButtonTrigger: Detects when specific objects or the player enter a trigger zone and performs actions like deactivating objects or triggering events.
o	MovingPlatform: Moves platforms along predefined routes or waypoints and allows players to move with them when standing on the platform.
o	MoveCamera: Controls the movement of the camera
o	ObjectPickup: Enables the player to pick up, hold, and interact with objects, including throwing or dropping them.
o	PlayerCam: Handles camera rotation based on player input to look around.
o	PlayerMovement: Manages the player's movement, jumping, sprinting, and crouching.
o	RespawnManager: Resets the position of the player and specific objects to predefined respawn points when they enter a death zone.
o	RouteManager: Defines shared routes or waypoints for objects like platforms to follow.
•	Levels
o	Tutorial level: introduces the pickup mechanic
o	Level 2: introduces the throw mechanic
•	Materials:
o	Basic Unity materials to represent different functions by different colors
•	Scenes:
o	The game consists of two scenes for different levels
•	Testing:
o	Game was tested on Windows
•	Particles:
o	Smoke particles are used in combination with the color red to show danger.









Time Management
Task	Time it Took (in hours)
Setting up Unity, making a project in GitHub	0.5
Researching player movement options and testing them	1
Making camera movements and testing with player movement	0.5
Player movement	1
Player movement bugfixing, mostly concerned with jumping	1
Adding Crouching and getting that to work with jumping (bugfixing)	0.5
Adding Slope capability	0.5
Pick up and throw capabilities	1
Throwing UI	0.5
Button interaction	0.5
Tutorial Level created through ProBuilder	2
Tutorial Level Lighting (attempt at using baked light (was not a good choice))	2
Moving Platform Script, implementation and bug fixing	1.5
Level 1 created through ProBuilder	1.5
Respawn Script	1
Code documentation	0.5
Using Unity particle system to create smoke to the death zone (the area that respawns you)	0.5
Making readme	0.5
All	16

Used Resources
•	Movement tutorial: https://www.youtube.com/watch?v=f473C43s8nE 
•	Slope & Crouching: https://www.youtube.com/watch?v=xCxSjgYTw9c 
