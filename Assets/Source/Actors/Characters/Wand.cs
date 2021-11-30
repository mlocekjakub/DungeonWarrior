using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Wand : Weapon
    {
        public override void Attack((int x, int y) position, Direction direction)
        {
            ActorManager.Singleton.Spawn<Bullet>(position, "Fire")
                .SetDefaultSprite("Fire")
                .SetDirection(direction)
                .SetDamage(10);
            ActorManager.Singleton.PlayRandomFireballSound();
        }
        public override int DefaultSpriteId => 272;
        public override string DefaultName => "Wand";

    }
}