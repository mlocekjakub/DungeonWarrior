namespace DungeonCrawl.Actors.Static
{
    using System.Collections.Generic;

    public class Floor : Actor
    {
        private Dictionary<string, int> FloorSpriteBank = new Dictionary<string, int>() {{"Bridge", 255}, {"Grass", 4}, 
            {"HellFloor", 1}, {"StoneCastleFloor", 6}, {"StoneFloor", 66}, {"HouseFloor", 334},{"ArrowUp", 1035}
            
        };

        public override int DefaultSpriteId => 1;
        public override string DefaultName => "Floor";
        public override bool Detectable => false;
        
        public Floor SetDefaultSprite(string name)
        {
            this.SetSprite(FloorSpriteBank[name]);
            return this;
        }
    }
}
