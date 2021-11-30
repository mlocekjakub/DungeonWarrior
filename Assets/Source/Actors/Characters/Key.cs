using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Key : Item
    {
        private int _doorId;
        
        public void SetDoorId(int doorId)
        {
            if (doorId >= 0)
            {
                _doorId = doorId;
            }
        }

        public int GetDoorId()
        {
            return _doorId;
        }
        public override int DefaultSpriteId => 560;
        public override string DefaultName => "Key";
    }
}