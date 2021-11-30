using DungeonCrawl.Core;
using UnityEngine;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Actors
{
    public abstract class Actor : MonoBehaviour
    {
        // TODO: IsDestroyable instead of destroyable, destroyable brzmi jak nazwa klasy/interfejsu
		public bool Destroyable = true;
        public int index { get; set; }

        public (int x, int y) Position
        {
            get => _position;
            set
            {
                _position = value;
                transform.position = new Vector3(value.x, value.y, Z);
            }
        }

        private (int x, int y) _position;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            SetSprite(DefaultSpriteId);
        }
        
        private void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        public void SetSprite(int id, bool flipedX = false, bool flipedY = false, int degrees = 0)
        {
            if (_spriteRenderer is null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            _spriteRenderer.flipX = flipedX;
            _spriteRenderer.flipY = flipedY;
            _spriteRenderer.transform.rotation = Quaternion.Euler(Vector3.forward * degrees);
            _spriteRenderer.sprite = ActorManager.Singleton.GetSprite(id);
        }
        
        public void SetSpriteColor(int red, int green, int blue, int transparency = 1)
        {
            if (_spriteRenderer is null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            _spriteRenderer.color = new Color(red, green, blue, transparency);
        }

        public void SetInvisibleSprite()
        {
            if (_spriteRenderer is null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }

            if (_spriteRenderer != null)
            {
                _spriteRenderer.enabled = false;
            }
        }

        public void SetVisibleSprite(int order = 0)
        {
            if (_spriteRenderer is null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
			if(_spriteRenderer != null){
            	_spriteRenderer.enabled = true;
            	_spriteRenderer.sortingOrder = order;
			}
        }

        public void TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

            if (actorAtTargetPosition == null)
            {
                // No obstacle found, just move
                Position = targetPosition;
            }
            else
            {
                if (actorAtTargetPosition.OnCollision(this))
                {
                    // Allowed to move
                    Position = targetPosition;
                }
            }
        }

        /// <summary>
        ///     Invoked whenever another actor attempts to walk on the same position
        ///     this actor is placed.
        /// </summary>
        /// <param name="anotherActor"></param>
        /// <returns>true if actor can walk on this position, false if not</returns>
        public virtual bool OnCollision(Actor anotherActor)
        {
            // All actors are passable by default
            return true;
        }

        /// <summary>
        ///     Invoked every animation frame, can be used for movement, character logic, etc
        /// </summary>
        /// <param name="deltaTime">Time (in seconds) since the last animation frame</param>
        protected virtual void OnUpdate(float deltaTime)
        {
        }

        /// <summary>
        ///     Can this actor be detected with ActorManager.GetActorAt()? Should be false for purely cosmetic actors
        /// </summary>
        public virtual bool Detectable { get; set; } = true;

        /// <summary>
        ///     Z position of this Actor (0 by default)
        /// </summary>
        public virtual int Z => 0;

        /// <summary>
        ///     Id of the default sprite of this actor type
        /// </summary>
        public abstract int DefaultSpriteId { get; }

        /// <summary>
        ///     Default name assigned to this actor type
        /// </summary>
        public abstract string DefaultName { get; }
    }
}