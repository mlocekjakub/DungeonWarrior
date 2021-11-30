using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Food : Item
    {
        private int _numberOfHealthToRestore = 10;

        public int GetNumberOfHealthToRestore()
        {
            return _numberOfHealthToRestore;
        }
        public void SetRandomSprite()
        {
            int[] arrayOfSprites = new[] {848,896,897,944,945};
            int randomNumber = UnityEngine.Random.Range(0, arrayOfSprites.Length);
            this.SetSprite(arrayOfSprites[randomNumber]);
        }
        
        public override int DefaultSpriteId => 560;
        public override string DefaultName => "Food";
    }
}