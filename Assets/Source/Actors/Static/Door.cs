using Assets.Source.Actors.Characters;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Static
{
    public class Door : Actor
    {
        private int _mapId;
		private bool _isOpenable = true;
        private bool _isOpen = false;
        private int _blobSpawnChance = 3;

        public Door SetMapId(int mapId)
        {
            _mapId = mapId;
            return this;
        }
        
		public void SetOpenable()
        {
            _isOpenable = false;
			_isOpen = true;
        }

        public void SetOpen(bool isOpen)
        {
            _isOpen = isOpen;
            _isOpen = isOpen;
        }

        public void Update()
        {
            if (_isOpen && _isOpenable)
            {
                this.SetSprite(627);
            }
            
			if(!_isOpen && _isOpenable)
            {
                this.SetSprite(625);
            }
        }

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player && _isOpen)
            {	
				if(_mapId != 0)
				{
                	ActorManager.Singleton.DestroyAllActors();
                	MapLoader.LoadMap(_mapId);
				}else{
					
					if(((Player)anotherActor).Position == (80,-25)){
						((Player)anotherActor).Position = (140,-13);
					}

					if(((Player)anotherActor).Position == (140,-14)){
						((Player)anotherActor).Position = (81,-25);
					}
				}

                if (_mapId == 3)
                {
                    ActorManager.Singleton.PlayDragonEntry();
                }
            }
            
            if (anotherActor is Player && !_isOpen)
            {
                InventorySlot slotWithKey = ((Player) anotherActor).GetInventory().CheckKeyToOpenDoor(this._mapId);
                if (slotWithKey != null)
                {
                    ActorManager.Singleton.PlayOpenDoors();
                    _isOpen = true;
                    slotWithKey.DeleteItem();
                }
                else
                {
                    ActorManager.Singleton.PlayFbiOpenUp();
                }
            }
            
            if(anotherActor is Bullet)
            {
                if (anotherActor.DefaultName == "BlobProjectile" && UnityEngine.Random.Range(0 ,101) < _blobSpawnChance)
                {
                    ActorManager.Singleton.Spawn<GreenBlob>(anotherActor.Position);
                    _blobSpawnChance = _blobSpawnChance - 1;
                }
                ActorManager.Singleton.DestroyActor(anotherActor);
            }
            
            return false;
        }
        
        public override int DefaultSpriteId => 1;
        public override string DefaultName => "Door";
    }
}