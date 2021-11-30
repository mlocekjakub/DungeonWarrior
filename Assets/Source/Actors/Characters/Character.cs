using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public abstract class Character : Actor
    {
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }

        private bool _isBurned = false;

        private static float timeIsBurned = 1.0f;
        private float burnCountdown = timeIsBurned;
        private int times = 5;

        virtual public void ApplyDamage(int damage)
        {
            
            Health -= damage;
            if (Health <= 0)
            {
                // Die
                OnDeath();

                ActorManager.Singleton.DestroyActor(this);
            }
        }

        public void SetHealth(int health)
        {
            MaxHealth = health;
            Health = health;
        }

        public void Burn(float deltaTime)
        {
            if (_isBurned){
                burnCountdown-=deltaTime;
                if(burnCountdown<=0){
                    ApplyDamage(2);
                    times--;
                    burnCountdown = timeIsBurned;
                }
                if(times==0){
                    burnCountdown = timeIsBurned;
                    this.SetIsBurn(false);
                }
            }
        }

        public void SetIsBurn(bool isBurned)
        {
            _isBurned = isBurned;
        } 
        
        protected abstract void OnDeath();

        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
