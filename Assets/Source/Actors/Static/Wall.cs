using Assets.Source.Actors.Characters;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Static
{
    using System.Collections.Generic;

    public class Wall : Actor
    {
        private Dictionary<string, int> WallSpriteBank = new Dictionary<string, int>() {{"WaterBound", 248}, {"WaterCorner", 249}, 
            {"WaterWall", 247}, {"UrbanHouse1", 963}, {"UrbanHouse2", 1007}, {"UrbanHouse3", 1009}, {"Hen", 361},
            {"HellWall", 825}, {"Forest", 48}, {"Fence", 148}, {"Dungeon", 113}, {"Duck", 360}, {"Cow", 362},
            {"Castle", 917}, {"WallieWall", 126}, {"OldmanWall", 23}, {"SecondDungeon", 114}, {"Bookshelf", 292}, {"HouseWall", 726}, {"CornerHouseWall", 725}
        };

        public override int DefaultSpriteId => 825;
        public override string DefaultName => "Wall";

        public string secondName;
        private int _blobSpawnChance = 3;

        public override bool OnCollision(Actor anotherActor)
        {
            if(anotherActor is Bullet)
            {
                if (anotherActor.DefaultName == "BlobProjectile" && UnityEngine.Random.Range(0 ,101) < _blobSpawnChance)
                {
                    ActorManager.Singleton.Spawn<GreenBlob>(anotherActor.Position);
                    _blobSpawnChance = _blobSpawnChance - 1;
                }
                ActorManager.Singleton.DestroyActor(anotherActor);
            }
            
            if (anotherActor is Player && this.secondName == "WallieWall")
            {
                ActorManager.Singleton.PlayWallySound();
            }
            
            if (anotherActor is Player && this.secondName == "OldmanWall")
            {
                ActorManager.Singleton.PlayOldmanSound();
            }
            return false;
        }

        public Wall SetDefaultSprite(string name)
        {
            secondName = name;
            this.SetSprite(WallSpriteBank[name]);
            return this;
        }
    }
}
