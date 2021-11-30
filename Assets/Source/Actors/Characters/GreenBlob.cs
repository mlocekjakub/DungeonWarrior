using System;
using UnityEngine;
using DungeonCrawl.Core;
using Assets.Source.Actors.Characters;
using Assets.Source.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class GreenBlob : Character
    {
        private bool canMove = false;
        private int currentSprite;
        private float countdown = 0.5f;

        public int Damage { get; private set; } = 500;

        private void Start()
        {
            SetHealth(90);
            currentSprite = 401;
        }

        protected override void OnUpdate(float deltaTime)
        {
            countdown -= deltaTime;
            if (countdown < 0.0f)
            {
                if (currentSprite != 405)
                {
                    currentSprite++;
                }
                else
                {
                    currentSprite = 401;
                }
                SetSprite(currentSprite);
                countdown = 0.5f;

                if (currentSprite == 404)
                {
                    canMove = true;
                }
            }
            if (canMove)
            {
                int randomMove = UnityEngine.Random.Range(0, 2);
                if (randomMove == 0)
                {
                    var arrow = ActorManager.Singleton.Spawn<Bullet>(Position, "BlobProjectile");
                    arrow.SetDefaultSprite("BlobProjectile");
                    arrow.SetDamage(30);
                    arrow.SetDirection(Direction.Right);
                    var arrow2 = ActorManager.Singleton.Spawn<Bullet>(Position, "BlobProjectile");
                    arrow2.SetDefaultSprite("BlobProjectile");
                    arrow2.SetDamage(30);
                    arrow2.SetDirection(Direction.Up);
                    var arrow3 = ActorManager.Singleton.Spawn<Bullet>(Position, "BlobProjectile");
                    arrow3.SetDefaultSprite("BlobProjectile");
                    arrow3.SetDamage(30);
                    arrow3.SetDirection(Direction.Down);
                    var arrow4 = ActorManager.Singleton.Spawn<Bullet>(Position, "BlobProjectile");
                    arrow4.SetDefaultSprite("BlobProjectile");
                    arrow4.SetDamage(30);
                    arrow4.SetDirection(Direction.Left);
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
                
                canMove = false;
            }

            this.Burn(deltaTime);
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Bullet)
            {
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
            Debug.Log("Well, I was already galaretka anyway...");
            ActorManager.Singleton.Spawn<GreenBlobCorpse>(Position);
        }

        public override int DefaultSpriteId => 401;
        public override string DefaultName => "Green blob";
    }
}
