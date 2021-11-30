# Dungeon Crawl (sprint 1)

## Story

[Roguelikes](https://en.wikipedia.org/wiki/Roguelike) are one of the oldest
types of video games. The earliest ones were made in the 70s, and they were inspired
a lot by tabletop RPGs. Roguelikes usually have the following features in common.

- They are tile-based.
- The game is divided into turns, that is, you take one action, then the other
  entities (monsters, allies, and so on, controlled by the CPU) take one.
- The task is usually to explore a labyrinth and retrieve some treasure from its
  bottom.
- They feature permadeath: if you die, it's game over, you need to start from the
  beginning again.
- They rely heavily on procedural generation: Levels, monster placement, items, and so on
  are randomized, so the game does not get boring.

Your task is to create a roguelike. You can deviate from the rules above,
the important bit is that it should be fun.

## What are you going to learn?

- Get more practice in OOP.
- Understand design patterns: layer separation. (All of the game logic, such as player
  movement, game rules, and so on), is in the `logic` package, completely
  independent of user interface code. In principle, you could implement a
  completely different interface, such as terminal, web, Virtual Reality, and so on, for
  the same logic code.)

## Tasks

1. Understand the existing code, classes, and tests so you can make changes. Do this before planning anyything else. It helps you understand what is going on.
    - A class diagram is created in a digialized format which contains the following.
- enums, classes, interfaces with all fields, methods
- connections between classes: inheritance, aggregation, composition
- multiplicity of connections (1..1, 1..*, *..*)

2. Create a game logic which restricts the movement of the player so they cannot run into walls and monsters.
    - The hero is not able to move into walls.
    - The hero is not able to move into monsters.

3. There are items lying around the dungeon. They are visible in the GUI.
    - There are at least two item types, such as a key and a sword.
    - There can be one item in a map square.
    - A player can stand on the same square as an item.
    - The item must be displayed on screen (unless somebody stands on the same square).

4. Create a feature that allows the hero to pick up an item.
    - In the bottom right corner, "Press E to pick up" text appears when the hero can pick up an item.
    - After the player presses E, the item the hero is standing on is gone from map.

5. Show picked up items in the inventory list.
    - There is an `Inventory` list on the screen.
    - After the hero picks up an item, it appears in the inventory.

6. Make the hero able to attack monsters by moving into them.
    - Attacking a monster removes 5 health points. If the health of a monster goes below 0, it dies and disappears.
    - If the hero attacks a monster and it does not die, it also attacks the hero at the same time (it only removes 2 health points).
    - Having a weapon increases attack strength.
    - Different monsters have different health and attack strengths.

7. Create doors in the dungeon that are opened using keys.
    - There are two new square types, closed door and open door.
    - The hero cannot pass through a closed door unless there is a key in the inventory. After moving through, the closed door becomes an open door.

8. Create three different monster types with different behaviors.
    - There are at least three different monster types with different behaviors.
    - One type of monster does not move at all.
    - One type of monster moves randomly. It cannot go trough walls or open doors.

9. [OPTIONAL] Create a more sophisticated movement AI.
    - One type of monster moves around randomly and teleports to a random free square every few turns.
    - A custom movement logic is implemented (such as Ghosts that can move trough walls, or a monster that chases the player).

10. Create maps that have more varied scenery. Take a look at the tile sheet (tiles.png). Get inspired!
    - At least three more tiles are used. These can be more monsters, items, or background. At least one of them must be not purely decorative, but have some effect on gameplay.

11. [OPTIONAL] Allow the player to set a name for the character. This name can also function as a cheat code.
    - There is a `Name` label and text field on the screen.
    - If the name given is one of the game developers' name, the player can walk through walls.

12. [OPTIONAL] Make the game sound fun by implementing audio effects, played when the player or enemies do stuff.
    - There is a footstep sound that plays whenever the player takes a step.
    - There is an attack sound whenever the player or an enemy attacks someone This sound might vary depending on the weapon (sword, axe, arrow).
    - Enemies, such as skeletons or ghosts, play characteristic sounds randomly every few seconds.
    - Some background music is added to the game.

13. Add the possibility to add more levels.
    - There are at least two levels.
    - There is a square type "stairs down". Entering this square moves the player to a different map.

14. Implement bigger levels than the game window.
    - Levels are larger than the game window (for example three screens wide and three screens tall).
    - When the player moves, the player character stays in the center of the view.

## General requirements

None

## Hints

- Start with the smaller tasks, and then move into the more difficult ones.
- Make sure you understand the whole starting code before making any changes.
- This project uses the Unity engine to run the game. It is an advanced game development tool
with multiple features, but don't worry, you don't have to learn it (unless you want to).
All essential features, such as moving the characters or spawning new actors are simplified
with some custom framework code. For instructions on setting up Unity, see the Background section.
- After installing Unity, open the Unity Hub, click the "Add" button and navigate to the project folder.
- [Link Unity with your C# IDE of choice](https://docs.unity3d.com/Manual/Preferences.html#External-Tools) (Visual Studio or Rider) before starting work on the project.
- Open the Game scene in the Scenes folder in the Project section after opening the project for the first time.
- There are only two things you have to do in Unity editor: start the game by pressing the PLAY button and start the C# solution.
- Save all files (CTRL+Shift+S might be helpful) before testing the game.
- After switching to Unity editor window, before starting the game, you have to wait until Unity recompiles the code (it should take 10-60 seconds).
- Do not modify the provided methods, because this might break or alter game behavior. The start code provides simple tools to interact with Unity.
- Several classes in this project utilize _Singleton pattern_. For more information, see the Background section.


## Background materials

- <i class="far fa-book-open"></i> [RogueBasin, a wiki with lots of resources on Roguelike creation](http://roguebasin.com/index.php?title=Articles)
- <i class="far fa-exclamation"></i> [Basics of OOP](project/curriculum/materials/pages/oop/basics-of-object-oriented-programming.md)
- <i class="far fa-exclamation"></i> [UML diagrams](project/curriculum/materials/pages/general/uml-unified-modeling-language.md)
- <i class="far fa-exclamation"></i> [How to design classes](project/curriculum/materials/pages/csharp/how-to-design-classes.md)
- <i class="far fa-exclamation"></i> [Unity setup](https://docs.unity3d.com/Manual/GettingStartedInstallingHub.html)
- <i class="far fa-book-open"></i> [Unity Documentaton](https://docs.unity3d.com/Manual/index.html)
- <i class="far fa-book-open"></i> [Singleton Pattern](https://www.dofactory.com/net/singleton-design-pattern)
- <i class="far fa-book-open"></i> [How to add sounds in Unity](https://support.unity.com/hc/en-us/articles/206116056-How-do-I-use-an-Audio-Source-in-a-script-)
- [1-Bit Pack by Kenney](https://kenney.nl/assets/bit-pack)
