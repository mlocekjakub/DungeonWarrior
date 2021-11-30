using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class InventoryPointer : Actor
    {
        protected override void OnUpdate(float deltaTime)
        {
            
                if (Input.GetKeyDown(KeyCode.W))
                {
                    // Move up
                    TryMovePointer(Direction.Up);
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    // Move down
                    TryMovePointer(Direction.Down);
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    // Move left
                    TryMovePointer(Direction.Left);
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    // Move right
                    TryMovePointer(Direction.Right);
                }

        }
        
        public  void TryMovePointer(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt<InventorySlot>(targetPosition);
            if (actorAtTargetPosition != null)
            {
                Position = targetPosition;
            }
            
        }
        
        public override int DefaultSpriteId => 611 ;
        public override int Z => 100 ;
        public override string DefaultName => "Pointer";
        }
}