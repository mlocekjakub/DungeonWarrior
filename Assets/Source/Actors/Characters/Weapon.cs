using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public abstract class Weapon : Item
    {
        private int damage;

        public void SetDamage(int damage)
        {
            if (damage > 0)
            {
                this.damage = damage;
            }
        }
        public int GetDamage()
        {
            return damage;
        }
        public abstract void Attack((int x, int y) position,Direction direction);
    }

}
