using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DungeonCrawl.Core
{
    using System.Net.NetworkInformation;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///     MapLoader is used for constructing maps from txt files
    /// </summary>
    public static class MapLoader
    {
        private static Player player = null;
        /// <summary>
        ///     Constructs map from txt file and spawns actors at appropriate positions
        /// </summary>
        /// <param name="id"></param>
        public static void LoadMap(int id)
        {
            var lines = Regex.Split(Resources.Load<TextAsset>($"map_{id}").text, "\r\n|\r|\n");

            // Read map size from the first line
            var split = lines[0].Split(' ');
            var width = int.Parse(split[0]);
            var height = int.Parse(split[1]);

            // Create actors
            for (var y = 0; y < height; y++)
            {
                var line = lines[y + 1];
                for (var x = 0; x < width; x++)
                {
                    var character = line[x];

                    SpawnActor(character, (x, -y), id);
                }
            }

            switch (id)
            {
                case 1:
                    CameraController.Singleton.Size = 5;
                    CameraController.Singleton.Position = (6, -11);
                    break;
                case 2:
                    CameraController.Singleton.Size = 5;
                    CameraController.Singleton.Position = (7, -30);
                    break;
            }
            // Set default camera size and position
            
        }

        private static void SpawnActor(char c, (int x, int y) position, int mapId)
        {
            switch (c)
            {
                case '#':
                    SpawnDefaultWall(mapId, position);
                    break;
                case '.':
                    SpawnDefaultFloor(mapId, position);
                    break;
                case 'p':
                    SpawnDefaultFloor(mapId, position);
                    if (player is null)
                    {
                        player = ActorManager.Singleton.Spawn<Player>(position);
                        player.Destroyable = false;
                    }
                    else
                    {
                        player.Position = position;
                    }
                    break;
                case 's':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Skeleton>(position);
                    break;
                case '@':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<GreenBlob>(position);
                    break;
                case ' ':
                    break;
                case 'b':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Bow>(position);
                    break;
                case 'P':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Armor>(position, "Plate")
                        .SetDefaultSprite("Plate")
                        .SetDefence(100);
                    break;
                case 'L':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Armor>(position, "Leather")
                        .SetDefaultSprite("Leather")
                        .SetDefence(100);
                    break;
                case 'w':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Wand>(position);
                    break;
                case 'q':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Sword>(position);
                    break;
                case ',':
                    SpawnDefaultFloor(mapId, position);
                    break;
                case 't':
                    SpawnDefaultWall(mapId, position);
                    break;
                case '~':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("WaterWall");
                    break;
                case '`':
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("WaterBound")
                        .SetSprite(248, true, false, 90);
                    break;
                case '(':
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("WaterCorner")
                        .SetSprite(249, true, false, 90);
                    break;
                case ')':
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("WaterCorner")
                        .SetSprite(249, true, true, 90);
                    break;
                case '>':
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("WaterCorner")
                        .SetSprite(249, false, true, 90);
                    break;
                case '<':
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("WaterCorner")
                        .SetSprite(249, false, false, 90);
                    break;
                case '!':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("WaterBound");;
                    break;
                case '|':
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("WaterBound")
                        .SetSprite(248, true);
                    break;
                case '_':
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("WaterBound")
                        .SetSprite(248, false, false, 90);
                    break;
                case 'C':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("Castle");
                    break;
                case '$':
                    var door = ActorManager.Singleton.Spawn<CastleDoor>(position);
                    if (mapId == 1)
                    {
                        door.SetMapId(2);
                    }
                    if(mapId == 2)
                    {
                        door.SetMapId(3);
                    }
                    
                    break;
                case 'B':
                    ActorManager.Singleton.Spawn<Floor>(position).SetDefaultSprite("Bridge");
                    break;
                case 'F':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("Fence");
                    break;
                case 'H':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("WallieWall");
                    break;
                case '7':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("OldmanWall");
                    break;
                case '%':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("Cow");
                    break;
                case 'K':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("Hen");
                    break;
                case 'D':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("WaterWall");
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("Duck");
                    break;
                case 'i':
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("WaterBound")
                        .SetSprite(248, false, false, 90);
                    ActorManager.Singleton.Spawn<Floor>(position)
                        .SetDefaultSprite("Bridge")
                        .SetSprite(255, false, false, 90);
                    break;
                case 'Y':
                   	var farmerCottage = ActorManager.Singleton.Spawn<Door>(position);
					farmerCottage.SetSprite(965);
					farmerCottage.SetMapId(0);
					farmerCottage.SetOpenable();
                    break;
                case 'O':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("UrbanHouse1");
                    break;
                case '0':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("UrbanHouse2");
                    break;
                case 'o':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("UrbanHouse3");
                    break;
                case '?':
                    ActorManager.Singleton.Spawn<Floor>(position).SetDefaultSprite("StoneCastleFloor");
                    break;
                case 'n':
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("WaterWall");
                    ActorManager.Singleton.Spawn<Floor>(position).SetDefaultSprite("Bridge");
                    break;
                case '*':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("Dungeon")
                        .SetSprite(113, false, true);
                    break;
                case '^':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("Dungeon")
                        .SetSprite(113, true, true);
                    break;
                case '-':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("Dungeon")
                        .SetSprite(113, true);
                    break;
                case '+':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("Dungeon");
                    break;
                case '/':
                    SpawnDefaultFloor(mapId, position);
                    var dungeonWallDown = ActorManager.Singleton.Spawn<Wall>(position);
                    dungeonWallDown.SetDefaultSprite("SecondDungeon");
                    dungeonWallDown.SetSprite(114, false, true);
                    break;
                case ';':
                    SpawnDefaultFloor(mapId, position); 
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("SecondDungeon")
                        .SetSprite(114, false, false, 90);
                    break;
                case ':':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("SecondDungeon")
                        .SetSprite(114, false, true, 90);
                    break;
                case '=':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("SecondDungeon");
                    break;
                case 'X':
                    ActorManager.Singleton.Spawn<Floor>(position).SetDefaultSprite("HouseFloor");
                    ActorManager.Singleton.Spawn<Key>(position).SetDoorId(2);
                    break;
                case 'Z':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Key>(position).SetDoorId(3);
                    break;
				case 'y':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Key>(position).SetDoorId(1);
                    break;
				case '1':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Door>(position).SetMapId(1).SetOpen(false);
                    break;
                case '2':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Door>(position).SetMapId(2).SetOpen(false);
                    break;
                case '3':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Door>(position).SetMapId(3).SetOpen(false);
                    break;
				case 'V':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Food>(position).SetRandomSprite();
                    break;
                case 'v':
                    ActorManager.Singleton.Spawn<Floor>(position).SetDefaultSprite("HouseFloor");
                    ActorManager.Singleton.Spawn<Food>(position).SetRandomSprite();
                    break;
				case 'f':
                    ActorManager.Singleton.Spawn<Floor>(position).SetDefaultSprite("HouseFloor");
                    break;
				case '&':
					ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("Bookshelf");
                    break;
				case '€':
					var houseDoor = ActorManager.Singleton.Spawn<Door>(position);
					houseDoor.SetMapId(0);
					houseDoor.SetOpenable();
					houseDoor.SetSprite(293,false,false,180);
                    break;
				case 'ƒ':
					ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("HouseWall");
                    break;
				case '‰':
					ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("HouseWall")
                        .SetSprite(726,false,false,-90);
                    break;
				case '£':
					ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("HouseWall")
                        .SetSprite(726,false,false,90);
					break;
				case 'Œ':
					ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("HouseWall")
                        .SetSprite(726,false,false,180);
					break;
				case '¢':
					ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("CornerHouseWall");
					break;
				case '¥':
					ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("CornerHouseWall")
                        .SetSprite(725,false,true);
					break;
				case '§':
					ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("CornerHouseWall")
                        .SetSprite(725,true,false);
					break;
				case '©':
					ActorManager.Singleton.Spawn<Wall>(position)
                        .SetDefaultSprite("CornerHouseWall")
                        .SetSprite(725,true,true);
					break;
                case 'd':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Dragon>(position);
                    break;
                case '8':
                    SpawnDefaultFloor(mapId, position);
                    ActorManager.Singleton.Spawn<Floor>(position)
                        .SetDefaultSprite("ArrowUp");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void SpawnDefaultWall(int mapId, (int x, int y) position)
        {
            switch (mapId)
            {
                case 1:
                    ActorManager.Singleton.Spawn<Wall>(position).SetDefaultSprite("Forest");
                    break;
                default:
                    ActorManager.Singleton.Spawn<Wall>(position);
                    break;
            }
        }

        private static void SpawnDefaultFloor(int mapId, (int x, int y) position)
        {
            switch (mapId)
            {
                case 1:
                    ActorManager.Singleton.Spawn<Floor>(position).SetDefaultSprite("Grass");
                    break;
                default:
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
            }
        }
    }
}
