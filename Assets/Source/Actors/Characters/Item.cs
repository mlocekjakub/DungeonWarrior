using DungeonCrawl.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public abstract class Item : Actor
    {
        private bool _pickable = true;
        private bool isInInventory = false;

        public void SetPickable(bool pickable)
        {
            _pickable = pickable;
        }

        public bool GetIfIsPickable()
        {
            return _pickable;
        }
        
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player && this._pickable)
            {
                ((Player)anotherActor).GetInventory().AddItemToInventory(this);
                ActorManager.Singleton.GetActorsIndexesToDestroy().Add(this.index);
                isInInventory = true;
            }
            return true;
        }

        public bool GetIfIsInInventory()
        {
            return isInInventory;
        }
    }
}