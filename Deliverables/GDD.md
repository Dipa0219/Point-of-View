# Introduction

This game is part of [Politecnico di Milano](https://www.polimi.it) Videogame Design & Programming course.

This is the Game Design Document of the project and will contain an abstract of every choice made along the way.
Note that this document was updated along the development of the game, for previous version please check the [github repository](https://github.com/Dipa0219/Point-of-View).

# Description

“Point of View” is a level-based puzzle game in which two or more small robots must find a way to exit the room by activating all the buttons in the room simultaneously. Simple, right? Sure, if it weren’t for a malfunction preventing the two robots from changing their view, making everything outside their field of vision unknown. Therefore, the only way for them to escape is to rely on each other, and cooperation will be the key to their escape. The player will control these robots, being able to manage only one at a time, alternating between them. From the first-person perspective of each robot, the player must guide them to solve the puzzle in order to complete the level. The game’s main challenge lies in the fact that to figure out how to move one robot, it will be necessary to observe its posi=on from the other robot's perspective. However, to actually move, the player will need to switch back to controlling the first robot.

# Design & Development Decisions

### Game characteristics
- Second person
- 3D

### Level Design
It consists of several levels, divided into multiple worlds. Each world will contain a similar number of levels, united by thematic colors and related environments. The difficulty will progressively increase with the introduction of new mechanics, different obstacles, and more complicated puzzles that will increasingly influence the player's choices and actions. The core of the level design will be to structure the levels so that each robot can only see the other robot and the goal it needs to reach, but not itself. The goal is to force the player to observe both perspectives and frequently switch between the controlled robots to complete the level.

# Timeline

o-o-o-o-o