using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Inventory
    {
        private static int _NumberSlotsInColumn = 5;
        private static int _NumberSlotsInRow = 2;
        private bool _isShowedInventory = true;
        public InventorySlot[,] _inventory;

        public InventoryPointer _pointer;
       
        public Inventory()
        {
            _inventory = new InventorySlot[_NumberSlotsInColumn,_NumberSlotsInRow];
           
            int index = 0;
            for (int x = 0; x <_NumberSlotsInColumn ; x++)
            {
                for (int y = 0; y < _NumberSlotsInRow; y++)
                {
                    _inventory[x,y] = ActorManager.Singleton.Spawn<InventorySlot>((x,y));
                    SetApperanceOfActor( _inventory[x,y],255,0,255);
                    index++;
                }
            }
            
            _pointer = ActorManager.Singleton.Spawn<InventoryPointer>((0, 0));
			SetApperanceOfActor(_pointer,0,0,0);
        }

		private void SetApperanceOfActor(Actor actor,int r, int g, int b){
			actor.Destroyable = false;
			actor.SetSpriteColor(r,g,b);
            actor.SetInvisibleSprite();
		}

        public bool GetIsShowedInventory()
        {
            return _isShowedInventory;
        }

        public void AddItemToInventory(Item item)
        {
			InventorySlot emptySlot =  FindEmptySlot();

			if(!(emptySlot is null))
			{
				emptySlot.SetItem(item);
				item.Detectable = false;
				item.Destroyable = false;
            	item.SetInvisibleSprite();
			}
        }

		private InventorySlot FindEmptySlot(){
			InventorySlot emptySlot = null;
			foreach (InventorySlot slot in _inventory)
            {
                if (slot.GetItem() is null)
                {
                    emptySlot = slot;
					break;
                }
            }

			return emptySlot;
		}
        
        public void ShowInventory((int x, int y) position)
        {
			
            InventorySlot firstSlot = _inventory[0,0];
            if (_isShowedInventory)
            {
                OpenInventory(position);
            }
            else
            {
				CloseInventory(position);
            }

            _pointer.Position = (firstSlot.Position.x, firstSlot.Position.y);
        }

		private void OpenInventory((int x, int y) position){
				int order = 1;
				int orderItem = 2;
				_isShowedInventory = false;
				foreach (InventorySlot slot in _inventory)
				{
                    slot.SetVisibleSprite();
                    slot.Position = (slot.Position.x + 2 + position.x, slot.Position.y + position.y);
                    Item item = slot.GetItem();
                    if(!(item is null))
                    {
                        item.SetVisibleSprite(orderItem);
                        item.Position = slot.Position;
                    }
                }
                _pointer.SetVisibleSprite(order);
                
		}

		private void CloseInventory((int x, int y) position){
			_isShowedInventory = true;
            _pointer.SetInvisibleSprite();
            foreach (InventorySlot slot in _inventory)
            {
                slot.SetInvisibleSprite();
                slot.Position = (slot.Position.x - 2 - position.x, slot.Position.y - position.y);
                Item item = slot.GetItem();
                if(!(item is null))
                {
                    item.SetInvisibleSprite();
                }
            }   
		}
		
		public InventorySlot CheckKeyToOpenDoor(int doorId){
			foreach (InventorySlot slot in _inventory)
			{
				Item item = slot.GetItem();
				if(!(item is null) && item is Key)
				{
					if (doorId == ((Key) item).GetDoorId())
					{
						return slot;
					} 
				}
			}

			return null;
		}

		public InventorySlot[,] GetInventorySlots()
		{
			return _inventory;
		}
    }
}
