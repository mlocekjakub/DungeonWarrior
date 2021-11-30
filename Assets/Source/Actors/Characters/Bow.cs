using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Bow : Weapon
    {
        public override void Attack((int x, int y) position, Direction direction)
        {
            var arrow = ActorManager.Singleton.Spawn<Bullet>(position, "Arrow");
            arrow.SetDefaultSprite("Arrow");
            arrow.SetDamage(20);
            arrow.SetDirection(direction);
            ActorManager.Singleton.PlayArrowWhooshSound();
        }
        public override int DefaultSpriteId => 327;
        public override string DefaultName => "Bow";

    }
}