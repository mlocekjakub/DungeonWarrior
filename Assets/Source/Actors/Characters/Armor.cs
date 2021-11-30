using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Armor : Item
    {
        private string secondName;
        private Dictionary<string, int> ArmorSpriteBank = new Dictionary<string, int>()
        {
            {"NoArmor", 1}, {"Leather", 85}, {"Plate", 80}
        };

        int defence;

        public void SetDefence(int defence)
        {
            if (defence > 0)
            {
                this.defence = defence;
            }
        }
        public string GetSecondName()
        {
            return secondName;
        }
        
        public Armor SetDefaultSprite(string name)
        {
            this.SetSprite(ArmorSpriteBank[name]);
            secondName = name;
            return this;
        }

        public int GetValueFromArmorSpriteBank(string name)
        {
            return ArmorSpriteBank[name];
        }

        public int GetDefence()
        {
            return defence;
        }

        public override int DefaultSpriteId { get; } = 24;
        public override string DefaultName { get; } = "Armor";

        public string Name { get; set; }
    }
}
