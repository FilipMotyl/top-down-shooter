# Top-down-shooter
Top-down shooter is a basic game created in Unity as an exercise in using animation system and implementing functionality of this genre of video games.

# Design
1. The idea is that the player goal is to kill as many enemies as possible.
2. Enemies are moving towards the player, dealing damage and killing themselves on contact.
3. The game ends when player loses all hitpoints.
4. There are two types of enemies: 
   1. Fast enemy, easy to kill awarding small number of points
   2. Slow enemy, hard to kill awarding large number of points
5. The game starts with 4 enemies on the map.
   1. Enemy is spawned every 5 seconds.
   2. There can be no more than 12 enemies at the time on the map. 

# Implementation
## objects

### Bullet
This is a generic bullet monobehaviour representing each projectile fired by the player. It has it's sprite, a box colldier and a rigidbody2d. It is destroyed after 5 seconds from instantiating, when hitting enemy or when flying out of the map.
#### Important methods
1. OnCollisionEnter2d() method which happens when the game detects collision with another object. If it is an enemy, it deals damage to it and destroys the bullet, if it is a map wall it just destroys the bullet.

### Enemy
This is monobehaviour representing an enemy on the map. It holds it's stats like hitpoints, damage, points awarde and necessary variables like it's box collider or renderer. It holds transform object of a player and uses it to track players position
#### Important methods
1. Death() method, in which it destroys itself and if killed by the player awards points.
2. FollowPlayer() and RotateEnemy() methods, which ensure that the enemies are constantly following the player and have slight rotation time offset.
3. Explosion() method, which is called when enemy reaches player. It deals damage to the player and does not award points.

### Explosion
This is very simple class ensuring that there is nothing left on the screen after Explosion animation.

### Game_Manager
This is main script of the game. It holds player, game over state, final score and map variables. It also uses CinemaVirtualCamera variables which are used in the project to ensure smooth interactive camera that is following the player and does not leave the map bounds.

#### Important methods
1. GameOver() method is enabling game over screen and destroys all the prefabs, the enemy spawners and the map.
2. StartGame() method is initializing the map, 4 enemy spawners and the player character which move along the corners of the map (which is not optimal way of implementing enemy spawning in this type of game, but I was just expermenting with moving spawners).

### MovingSpawner
This monobehaviour is conncted to each spawner object and serves in moving them along the corners of the map. It holds variables to customize direction and speed of it's pendulum-like movement.

### PlayAgain
This monobehaviour is representing game over screen. It disables itself and prompts Game_Manager to start new game.

### Player
This is a player script it represents player and has methods for player movement and rotation.

#### Important methods
1. Update() method read keyboard and mouse input and calls other methods. It also changes animation states from idle to moving depending on the player's actions.
2. PlayerMove() method moves player using MovePosition method of the RigidBody2d component.
3. PlayerRotate() method slowly rotates player in the direction of the cursor.

### Shooting
This is a component that allow the player to shoot. It spawns bullets on mouse clicks with the position of the player position and rotation depending on the player's current rotation.

#### Important methods
1. Shoot() method instantiates the bullet and adds force to its rigidbody using ImpulseMode2d mode.

### Spawner
Is a general class responsible for spawning enemies and keeps the list of living enemies on the map.

#### Important methods
1. Update() method - spawns enemy every few seconds.
2. SpawnEnemy() method randomly chooses enemy type and creates it on randomly chosen MovingSpawner.
3. RandomizeEnemy() method randolmy chooses enemy type.
