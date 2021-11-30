using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Fist : Weapon
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
            var fist = ActorManager.Singleton.Spawn<Fist>((position.x, position.y));
            fist.SetDirection(direction);

            if (direction == Direction.Down)
            {
                fist.SetSprite(DefaultSpriteId,false, false, 180);
            }
            
            if (direction == Direction.Right)
            {
                fist.SetSprite(DefaultSpriteId,false, false, 270);
            }
            
            if (direction == Direction.Left)
            {
                fist.SetSprite(DefaultSpriteId,false, false, 90);
            }
            fist.SetPickable(false);
			fist.attacking = true;
            ActorManager.Singleton.PlayRandomSwingSound();
        }

        void Start()
        {
            SetDamage(5);
        }
        void Update()
        {
            if (time == 1 && attacking)
            {
                TryMove(this.direction);
            }
            if (time == speed && attacking)
            {
                ActorManager.Singleton.DestroyActor(this);
            }
            time++;

        }

        public override int DefaultSpriteId => 88;
        public override string DefaultName => "Fist";
    }
}