using DungeonCrawl.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Bullet : Actor
    {
        private Dictionary<string, int> BulletSpriteBank = new Dictionary<string, int>()
        {
            {"Fire", 494}, {"Arrow", 279}, {"BlobProjectile", 801}, {"Bone", 607}, {"DragonFire", 494}
        };
        
        public Direction direction;
        private float countdown = 0.1f;
        private int damage;
        private string secondName;

		public string GetSecondName(){
			return secondName;
		}

        public Bullet SetDirection(Direction direction)
        {
            if (direction == Direction.Down || direction == Direction.Up || direction == Direction.Left 
                || direction == Direction.Right || direction == Direction.UpRight || direction == Direction.UpLeft 
                || direction == Direction.DownRight || direction == Direction.DownLeft)
            {
                this.direction = direction;
            }
            if (direction == Direction.Up)
            {
                SetSprite(BulletSpriteBank[secondName],false, false, 45);
            }
            
            if (direction == Direction.Down)
            {
                SetSprite(BulletSpriteBank[secondName], false, true, -45);
            }
            
            if (direction == Direction.Left)
            {
                SetSprite(BulletSpriteBank[secondName],true, false, 45);
            }
            
            if (direction == Direction.Right)
            {
                SetSprite(BulletSpriteBank[secondName],false, false, -45);
            }

            return this;
        }

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
        protected override void OnUpdate(float deltaTime)
        {
            countdown -= deltaTime;
            if (countdown < 0.0f)
            {
                TryMove(direction);
                countdown = 0.1f;
            }
        }
        public override int DefaultSpriteId { get; } = 279;
        public override string DefaultName { get; } = "Bullet";

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Bullet && ((Bullet)anotherActor).GetSecondName() != "DragonFire")
            {
                ActorManager.Singleton.DestroyActor(anotherActor);
                ActorManager.Singleton.DestroyActor(this);
            }
            return true;
        }
        
        public Bullet SetDefaultSprite(string name)
        {
            this.SetSprite(BulletSpriteBank[name]);
            this.secondName = name;
            return this;
        }
    }
}
