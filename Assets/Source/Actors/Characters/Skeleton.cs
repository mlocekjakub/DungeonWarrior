using System;
using UnityEngine;
using DungeonCrawl.Core;
using Assets.Source.Actors.Characters;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        private float countdown = 2.0f;
        
        private void Start()
        {
            SetHealth(90);
        }

        private int timeOfLastUpdate;

        protected override void OnUpdate(float deltaTime)
        {
            countdown -= deltaTime;
            if (countdown < 0.0f)
            {
                countdown = 2.0f;
                int randomMove = UnityEngine.Random.Range(0, 2);
                if (randomMove == 0)
                {
                    ActorManager.Singleton.Spawn<Bullet>(Position, "Bone")
                        .SetDefaultSprite("Bone")
                        .SetDirection(Direction.Right)
                        .SetDamage(20);
                    ActorManager.Singleton.Spawn<Bullet>(Position, "Bone")
                        .SetDefaultSprite("Bone")
                        .SetDirection(Direction.Up)
                        .SetDamage(20);
                    ActorManager.Singleton.Spawn<Bullet>(Position, "Bone")
                        .SetDefaultSprite("Bone")
                        .SetDirection(Direction.Down)
                        .SetDamage(20);
                    ActorManager.Singleton.Spawn<Bullet>(Position, "Bone")
                        .SetDefaultSprite("Bone")
                        .SetDirection(Direction.Left)
                        .SetDamage(20);
                }
                else
                {
                    randomMove = UnityEngine.Random.Range(0, 4);
                    switch (randomMove)
                    {
                        case 0:
                            TryMove(Direction.Up);
                            break;
                        case 1:
                            TryMove(Direction.Down);
                            break;
                        case 2:
                            TryMove(Direction.Left);
                            break;
                        case 3:
                            TryMove(Direction.Right);
                            break;
                        default:
                            break;
                    }
                }
            }

            this.Burn(deltaTime);
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if(anotherActor.DefaultName == "Bullet")
            {
                if (((Bullet)anotherActor).GetSecondName() == "Bone")
                {
                    ActorManager.Singleton.DestroyActor(anotherActor);
                    return true;
                }
            
                if (((Bullet)anotherActor).GetSecondName() == "Fire")
                {
                    this.SetIsBurn(true);
                }
                
                this.ApplyDamage(((Bullet)anotherActor).GetDamage());
                ActorManager.Singleton.DestroyActor(anotherActor);
            }

            if (anotherActor is Weapon)
            {
                this.ApplyDamage(((Weapon)anotherActor).GetDamage());
            }
         
            
            
            return false;
        }

        protected override void OnDeath()
        {
            Debug.Log("Well, I was already dead anyway...");
            ActorManager.Singleton.Spawn<SkeletonCorpse>(Position);
            ActorManager.Singleton.PlayRandomDeathSound();
            ActorManager.Singleton.GetEnemyPositionsToSpawn().Add((Position.x, Position.y, name));
        }

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}
