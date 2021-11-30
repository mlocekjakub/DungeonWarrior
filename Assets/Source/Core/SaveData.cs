using DungeonCrawl.Actors.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawl.Core
{
    [System.Serializable]
    public class SaveData
    {
        public int health;
        public int xPosition;
        public int yPosition;
        public string weapon;
        public string armor;
        public string[] items = new string[10];
        public List<int> actorsIndexesToDestroy = new List<int>();
        public List<(int, int, string)> enemyPositionsToSpawn = new List<(int, int, string)>();


        public SaveData(Player player)
        {
            health = player.Health;
            xPosition = player.Position.x;
            yPosition = player.Position.y;
            weapon = player.GetWeapon().DefaultName;
            armor = player.GetArmor().Name;
            actorsIndexesToDestroy = ActorManager.Singleton.GetActorsIndexesToDestroy();
            enemyPositionsToSpawn = ActorManager.Singleton.GetEnemyPositionsToSpawn();



            int inx = 0;
            foreach (var item in player.GetInventory()._inventory)
            {
                if (item._item != null)
                {
                    if (item._item.name == "Armor")
                    {
                        var armor = (Armor)item._item;
                        items[inx] = armor.Name;
                    }
                    else
                    {
                        items[inx] = item._item.name;
                    }
                    inx++;
                }
            }
        }
    }
}
