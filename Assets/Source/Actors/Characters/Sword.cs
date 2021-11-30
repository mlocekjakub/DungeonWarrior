using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Sword : Weapon
    {
        public int speed = 20;
        public int slices = 1;
        private int time = 0;
        public Direction direction;
        private bool attacking = false;
        
        public void SetDirection(Direction direction)
        {
            this.direction = direction;
        }
        // Start is called before the first frame update
        public override void Attack((int x, int y) position, Direction direction)
        {
            var sword = ActorManager.Singleton.Spawn<Sword>((position.x, position.y));
            sword.SetDirection(direction);

            if (direction == Direction.Up)
            {
                sword.SetSprite(DefaultSpriteId,false, false, 45);
            }
            
            if (direction == Direction.Down)
            {
                sword.SetSprite(DefaultSpriteId, false, true, -45);
            }
            
            if (direction == Direction.Left)
            {
                sword.SetSprite(DefaultSpriteId,true, false, 45);
            }
            
            if (direction == Direction.Right)
            {
                sword.SetSprite(DefaultSpriteId,false, false, -45);
            }

            sword.attacking = true;
            sword.SetPickable(false);
            ActorManager.Singleton.PlayRandomSwingSound();
        }

        void Start()
        {
            SetDamage(40);
        }

        void Update()
        {
            if (time == 0 && attacking)
            {
                TryMove(this.direction);
            }
            if (time == speed && attacking)
            {
                ActorManager.Singleton.DestroyActor(this);
            }
            time++;

        }

        public override int DefaultSpriteId => 417;
        public override string DefaultName => "Sword";
    }
}