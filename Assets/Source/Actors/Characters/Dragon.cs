using System;
using UnityEngine;
using DungeonCrawl.Core;
using Assets.Source.Actors.Characters;
using Assets.Source.Core;
using UnityEngine.SceneManagement;

namespace DungeonCrawl.Actors.Characters
{
    public class Dragon : Character
    {
        private int currentSprite;
        private static float speed = 0.7f;
        private float countdown = speed;

        private bool isMove = true;
        private bool _isAlive = true;
        private float _deathTime = 0.0f;
        private const float _showMenuCooldown = 7.0f;
        public int Damage { get; private set; } = 500;

        private void Start()
        {
            SetHealth(150);
        }
        
        override public void ApplyDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0 && _isAlive)
            {
                OnDeath();
                this.SetInvisibleSprite();
            }
        }
        
        protected override void OnUpdate(float deltaTime)
        {
            countdown -= deltaTime;
            if (countdown < 0.0f && _isAlive)
            {
                countdown = speed;
                if (isMove)
                {
                    
                    GenerateFireball(Direction.Right, Position.x + 2, Position.y, 1, -1);
                    GenerateFireball(Direction.Left, Position.x - 2, Position.y, -1, -1);
                    GenerateFireball(Direction.Up, Position.x, Position.y + 2, -1, 1);
                    GenerateFireball(Direction.Down, Position.x, Position.y - 2, -1, -1);
                    GenerateFireball(Direction.DownRight, Position.x + 2, Position.y - 2, -1, -1);
                    GenerateFireball(Direction.DownLeft, Position.x - 2, Position.y - 2, -1, -1);
                    GenerateFireball(Direction.UpRight, Position.x + 2, Position.y + 2, -1, 1);
                    GenerateFireball(Direction.UpLeft, Position.x - 2, Position.y + 2, -1, 1);

                    isMove = false;
                }
                else
                {
                    isMove = true;
                   var randomMove = UnityEngine.Random.Range(0, 4);
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
            
            if (!_isAlive)
            {
                _deathTime += deltaTime;
            }

            if (_deathTime > _showMenuCooldown)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        private void GenerateFireball(Direction direction, int posX, int posY, int paddingX, int paddingY)
        {
            ActorManager.Singleton.Spawn<Bullet>((posX,posY+paddingY))
                .SetDefaultSprite("DragonFire")
                .SetDirection(direction)
                .SetDamage(25);
            ActorManager.Singleton.Spawn<Bullet>((posX+paddingX,posY+paddingY))
                .SetDefaultSprite("DragonFire")
                .SetDirection(direction)
                .SetDamage(25);
            ActorManager.Singleton.Spawn<Bullet>((posX,posY))
                .SetDefaultSprite("DragonFire")
                .SetDirection(direction)
                .SetDamage(25);
            ActorManager.Singleton.Spawn<Bullet>((posX+paddingX,posY))
                .SetDefaultSprite("DragonFire")
                .SetDirection(direction)
                .SetDamage(25);
        }
        
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Bullet)
            {
                    this.ApplyDamage(((Bullet) anotherActor).GetDamage());
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
            Debug.Log("Oh no, I'm dead!");
            ActorManager.Singleton.Spawn<SkeletonCorpse>(Position);
            ActorManager.Singleton.PlayWinSound();
            _isAlive = false;
        }

        public override int DefaultSpriteId => 411;
        public override string DefaultName => "Dragon";
    }
}
