using UnityEngine;
using DungeonCrawl.Core;
using Assets.Source.Core;
using UnityEngine.SceneManagement;
using Assets.Source.Actors.Characters;
using DungeonCrawl.Actors.Static;
using Assets;
using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        private Weapon _weapon; 
        private Armor _armor; 
        private Inventory _inventory;
        public Direction direction;
        public HealthBar healthBar = HealthBar.Singleton;

        private bool _alive = true;
        private float _deathTime;
        private const float _showMenuCooldown = 7.0f;

        override public void ApplyDamage(int damage)
        {
            Health -= damage;
            ActorManager.Singleton.PlayOuch();

            if (Health <= 0 && _alive)
            {
                OnDeath();
                SetSprite(359);
            }
        }

        public void Start()
        {
            (int, int) startPosition = (0, 0);
            _weapon = ActorManager.Singleton.Spawn<Fist>(startPosition);
            _armor = ActorManager.Singleton.Spawn<Armor>(0, 0, "NoArmor");
            _armor.SetDefaultSprite("NoArmor");
			_weapon.SetInvisibleSprite();
			_armor.SetInvisibleSprite();
            SetInventory();
            SetHealth(100);
            healthBar.SetMaxHealth(Health);
            healthBar.SetHealth(Health);
            CameraController.Singleton.Position = Position;
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void SetArmor(Armor armor)
        {
            _armor = armor;
        }

        public Inventory GetInventory()
        {
            return _inventory;
        }

        public Armor GetArmor()
        {
            return _armor;
        }
        
        public Weapon GetWeapon()
        {
            return _weapon;
        }

        public void SetInventory()
        {
            if (_inventory == null)
            {
                _inventory = new Inventory();
            }
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (_inventory.GetIsShowedInventory())
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    // Move up
                    TryMove(Direction.Up);
                    direction = Direction.Up;
                    CameraController.Singleton.Position = Position;
                    ActorManager.Singleton.PlayRandomStep();
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    // Move down
                    TryMove(Direction.Down);
                    direction = Direction.Down;
                    CameraController.Singleton.Position = Position;
                    ActorManager.Singleton.PlayRandomStep();
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    // Move left
                    TryMove(Direction.Left);
                    direction = Direction.Left;
                    CameraController.Singleton.Position = Position;
                    ActorManager.Singleton.PlayRandomStep();
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    // Move right
                    TryMove(Direction.Right);
                    direction = Direction.Right;
                    CameraController.Singleton.Position = Position;
                    ActorManager.Singleton.PlayRandomStep();
                }

				if (Input.GetKeyDown(KeyCode.F10)){
					ActorManager.Singleton.DestroyAllActors();
					MapLoader.LoadMap(99);
					CameraController.Singleton.Position = Position;
				}

                if (Input.GetKeyDown(KeyCode.Equals))
                {
                    if (CameraController.Singleton.Size > 2)
                    {
                        CameraController.Singleton.Size = CameraController.Singleton.Size - 1;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Minus))
                {
                    if (CameraController.Singleton.Size < 10)
                    {
                        CameraController.Singleton.Size = CameraController.Singleton.Size + 1;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _weapon.Attack(this.Position, direction);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    InventorySlot slot = ActorManager.Singleton.GetActorAt<InventorySlot>(_inventory._pointer.Position);
                    Item item = slot.GetItem();

                    if (!(item is null))
                    {
                        UseItem(item, (InventorySlot) slot);
                    }

                    SetSkin();
                }

                if (Input.GetKeyDown(KeyCode.K))
                {
                    InventorySlot slot = ActorManager.Singleton.GetActorAt<InventorySlot>(_inventory._pointer.Position);
                    Item item = slot.GetItem();

                    if (!(item is null))
                    {
                        ((InventorySlot) slot).DeleteItem();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                _inventory.ShowInventory(this.Position);
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                ActorManager.Singleton.QuickSave(this);
                ActorManager.Singleton.GetEnemyPositionsToSpawn().Clear();
                Debug.Log("save");
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                QuickLoad();
            }

            if (!_alive)
            {
                _deathTime += deltaTime;
            }

			this.Burn(deltaTime);			

            if (_deathTime > _showMenuCooldown)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        private void QuickLoad()
        {
            SaveData data = ActorManager.Singleton.QuickLoad();
            Health = data.health;
            Position = (data.xPosition, data.yPosition);
            CameraController.Singleton.Position = Position;
            switch (data.weapon)
            {
                case "Sword":
                    var sword = ActorManager.Singleton.Spawn<Sword>((0, 0));
                    sword.SetInvisibleSprite();
                    sword.Destroyable = false;
                    sword.SetPickable(false);
                    SetWeapon(sword);
                    break;
                case "Bow":
                    var bow = ActorManager.Singleton.Spawn<Bow>((0, 0));
                    bow.SetInvisibleSprite();
                    bow.Destroyable = false;
                    bow.SetPickable(false);
                    SetWeapon(bow);
                    break;
                case "Wand":
                    var wand = ActorManager.Singleton.Spawn<Wand>((0, 0));
                    wand.SetInvisibleSprite();
                    wand.Destroyable = false;
                    wand.SetPickable(false);
                    SetWeapon(wand);
                    break;
                default:
                    var fist = ActorManager.Singleton.Spawn<Fist>((0, 0));
                    fist.SetInvisibleSprite();
                    fist.Destroyable = false;
                    fist.SetPickable(false);
                    SetWeapon(fist);
                    break;
            }
            switch (data.armor)
            {
                case "Leather":
                    var leather = ActorManager.Singleton.Spawn<Armor>((0, 0));
                    leather.Name = "Leather";
                    leather.SetInvisibleSprite();
                    leather.Destroyable = false;
                    leather.SetPickable(false);
                    leather.SetDefaultSprite("Leather");
                    leather.SetDefence(100);
                    SetArmor(leather);
                    break;
                case "Plate":
                    var plate = ActorManager.Singleton.Spawn<Armor>((0, 0));
                    plate.Name = "Plate";
                    plate.SetInvisibleSprite();
                    plate.Destroyable = false;
                    plate.SetPickable(false);
                    SetArmor(plate);
                    plate.SetDefaultSprite("Plate");
                    plate.SetDefence(100);
                    break;
                default:
                    var noArmor = ActorManager.Singleton.Spawn<Armor>((0, 0));
                    noArmor.Name = "NoArmor";
                    noArmor.SetInvisibleSprite();
                    noArmor.Destroyable = false;
                    noArmor.SetPickable(false);
                    SetArmor(noArmor);
                    noArmor.SetDefaultSprite("NoArmor");
                    noArmor.SetDefence(100);
                    break;
            }
            _inventory = new Inventory();
            foreach (string item in data.items)
            {
                if (item != null)
                {
                    switch (item)
                    {
                        case "Sword":
                            var sword = ActorManager.Singleton.Spawn<Sword>((0, 0));
                            _inventory.AddItemToInventory(sword);
                            break;
                        case "Bow":
                            var bow = ActorManager.Singleton.Spawn<Bow>((0, 0));
                            _inventory.AddItemToInventory(bow);
                            break;
                        case "Wand":
                            var wand = ActorManager.Singleton.Spawn<Wand>((0, 0));
                            _inventory.AddItemToInventory(wand);
                            break;
                        case "Leather":
                            var leather = ActorManager.Singleton.Spawn<Armor>((0, 0));
                            leather.Name = "Leather";
                            leather.SetDefaultSprite("Leather");
                            leather.SetDefence(100);
                            _inventory.AddItemToInventory(leather);
                            break;
                        case "Plate":
                            var plate = ActorManager.Singleton.Spawn<Armor>((0, 0));
                            plate.Name = "Plate";
                            plate.SetDefaultSprite("Plate");
                            plate.SetDefence(100);
                            _inventory.AddItemToInventory(plate);
                            break;
                        default:
                            break;
                    }
                }
            }
            _inventory.ShowInventory((0, 0));
            _inventory.ShowInventory((0, 0));



            List<Actor> actorsToDestroy = new List<Actor>();
            foreach (var item in ActorManager.Singleton.GetAllActors())
            {
                if (data.actorsIndexesToDestroy.Contains(item.index))
                {
                    actorsToDestroy.Add(item);
                }
            }
            foreach (var item in actorsToDestroy)
            {
                ActorManager.Singleton.DestroyActor(item);
            }
            foreach (var item in ActorManager.Singleton.GetEnemyPositionsToSpawn())
            {
                if (item.Item3 == "Skeleton")
                {
                    var corpse = ActorManager.Singleton.GetActorAt<SkeletonCorpse>((item.Item1, item.Item2));
                    if (corpse != null)
                    {
                        ActorManager.Singleton.DestroyActor(corpse);
                    }
                    ActorManager.Singleton.Spawn<Skeleton>((item.Item1, item.Item2));
                }
            }
            _alive = true;
            _deathTime = 0.0f;
            ActorManager.Singleton.GetEnemyPositionsToSpawn().Clear();
            SetSkin();
        }

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Bullet)
            {
				if(((Bullet) anotherActor).GetSecondName() == "Fire"){
					this.SetIsBurn(true);
				}
                ApplyDamage(((Bullet)anotherActor).GetDamage());
                ActorManager.Singleton.DestroyActor(anotherActor);
                healthBar.SetHealth(Health);
            }
            
            
            return false;
        }

        private void UseItem(Item item, InventorySlot slot)
        {
            if (item is Weapon)
            {
                UseWeapon((Weapon) item, slot);
            }

            if (item is Armor)
            {
                UseArmor((Armor) item, slot);
            }

			if (item is Food)
            {
                 UseFood((Food) item, slot);   
            }
		} 


        private void UseWeapon(Weapon weapon, InventorySlot slot)
        {
            if (!(_weapon is Fist))
            {
                slot.SwapItem(weapon, _weapon);
            }
            else
            {
                slot.DeleteItem();
            }

            SetWeapon(weapon);
        }

		private void UseArmor(Armor armor, InventorySlot slot)
        {
            if (!(_armor.GetValueFromArmorSpriteBank(_armor.GetSecondName()) == 1))
            {
                slot.SwapItem(armor, _armor);
            }
            else
            {
                slot.DeleteItem();
            }

            SetArmor(armor);
        }

        private void UseFood(Food food, InventorySlot slot)
        {
            if (Health < MaxHealth)
            {
                Health += food.GetNumberOfHealthToRestore();
                slot.DeleteItem();
                healthBar.SetHealth(Health);
            }

            if (Health >= MaxHealth)
            {
                Health = MaxHealth;
            }

            
        }

        private void SetSkin()
        {
            if(_weapon is Wand && _armor.GetSecondName() == "Leather")
            {
                this.SetSprite(78);
            }
            if ((_weapon is Bow || _weapon is Fist) && _armor.GetSecondName() == "Leather")
            {
                this.SetSprite(77);
            }

            if (_weapon is Sword && _armor.GetSecondName() == "Leather")
            {
                this.SetSprite(71);
            }

            if (_weapon is Wand && _armor.GetSecondName() == "Plate")
            {
                this.SetSprite(28);
            }

            if ((_weapon is Bow || _weapon is Fist) && _armor.GetSecondName() == "Plate")
            {
                this.SetSprite(29);
            }

            if (_weapon is Sword && _armor.GetSecondName() == "Plate")
            {
                this.SetSprite(27);
            }

            if (_weapon is Wand && _armor.GetSecondName() == "NoArmor")
            {
                this.SetSprite(25);
            }

            if ((_weapon is Bow || _weapon is Fist) && _armor.GetSecondName() == "NoArmor")
            {
                this.SetSprite(24);
            }

            if (_weapon is Sword && _armor.GetSecondName() == "NoArmor")
            {
                this.SetSprite(26);
            }
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
            ActorManager.Singleton.Spawn<SkeletonCorpse>(Position);
            ActorManager.Singleton.PlayDarkSoulsDeath();
            _alive = false;
        }

       
        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}