using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class InventorySlot : Actor
    {
        public Item _item = null;

        public Item GetItem()
        {
            return _item;
        }
        
        public void SetItem(Item item)
        { 
            _item = item;
        }
        public void DeleteItem()
        {
            _item.SetInvisibleSprite();
            _item = null;
        }
        public void SwapItem(Item item, Item newItem)
        {
            _item.SetInvisibleSprite();
            newItem.Position = _item.Position;
            newItem.SetVisibleSprite();
            (_item, newItem) = (newItem, _item);
        }
        
        public override int DefaultSpriteId => 758 ;
        public override string DefaultName => "Slot";
    }
}