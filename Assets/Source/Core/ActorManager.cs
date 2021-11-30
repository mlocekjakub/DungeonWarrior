using System.Collections.Generic;
using System.Linq;
using DungeonCrawl.Actors;
using UnityEngine;
using UnityEngine.U2D;
using DungeonCrawl.Actors.Characters;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     Main class for Actor management - spawning, destroying, detecting at positions, etc
    /// </summary>
    public class ActorManager : MonoBehaviour
    {
        private static ActorManager _instance;
        private SpriteAtlas _spriteAtlas;
        private HashSet<Actor> _allActors;
        private int _actorIndex = 0;
        private List<int> actorsIndexesToDestroy = new List<int>();
        private List<(int, int, string)> enemyPositionsToSpawn = new List<(int, int, string)>();
		
		public List<int> GetActorsIndexesToDestroy(){
			return actorsIndexesToDestroy;
		}

		public List<(int, int, string)> GetEnemyPositionsToSpawn(){
			return enemyPositionsToSpawn;
		}
        /// <summary>
        ///     ActorManager singleton
        /// </summary>
        public static ActorManager Singleton {
            get
            {
                if (_instance is null)
                {
                    GameObject gameObject = new GameObject();
                    _instance = gameObject.AddComponent<ActorManager>();
                    _instance.OnStart();
                }

                return _instance;
            } 
            private set {} }

        #region Save system
        //private readonly string path = Application.persistentDataPath + "/quickSave.dupa";

        public void QuickSave(Player player)
        {
            string path = Application.persistentDataPath + "/quickSave.dupa";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            SaveData data = new SaveData(player);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public SaveData QuickLoad()
        {
            string path = Application.persistentDataPath + "/quickSave.dupa";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                SaveData data = (SaveData)formatter.Deserialize(stream);
                stream.Close();
                return data;
            }
            else
            {
                Debug.Log("nie ma sejwa ziomeczku");
                return null;
            }
        }
        #endregion

        #region Audio functions
        public AudioSource source;
        public AudioClip bruh;
        public AudioClip goatScream;
        public AudioClip manScream;
        public AudioClip tomScream;
        public AudioClip darkSoulDeath;
        public AudioClip step1;
        public AudioClip step2;
        public AudioClip step3;
        public AudioClip ouch;
        public AudioClip fbiOpenUp;
        public AudioClip openDoors;
        public AudioClip arrowWhoosh;
        public AudioClip fireball1;
        public AudioClip fireball2;
        public AudioClip swing1;
        public AudioClip swing2;
        public AudioClip swing3;
        public AudioClip dragonEntry;
        public AudioClip winSound;
        
        public AudioClip Wallie;
        public AudioClip Oldman;

        private void OnStart()
        {
            _allActors = new HashSet<Actor>();
            _spriteAtlas = Resources.Load<SpriteAtlas>("Spritesheet");
        }
        
        public bool IsPlayingAudio()
        {
            return source.isPlaying;
        }

        public void PlayRandomDeathSound()
        {
            int sample = Random.Range(0, 4);
            switch (sample)
            {
                case 0:
                    source.PlayOneShot(bruh);
                    break;
                case 1:
                    source.PlayOneShot(goatScream);
                    break;
                case 2:
                    source.PlayOneShot(manScream);
                    break;
                case 3:
                    source.PlayOneShot(tomScream);
                    break;
                default:
                    break;
            }
        }

        public void PlayDarkSoulsDeath()
        {
            source.PlayOneShot(darkSoulDeath);
        }

        public void PlayWinSound()
        {
            source.PlayOneShot(winSound);
        }
        
        public void PlayWallySound()
        {
            source.PlayOneShot(Wallie);
        }
        
        public void PlayOldmanSound()
        {
            source.PlayOneShot(Oldman);
        }

        public void PlayRandomStep()
        {
            int sample = Random.Range(0, 3);
            switch (sample)
            {
                case 0:
                    source.PlayOneShot(step1);
                    break;
                case 1:
                    source.PlayOneShot(step2);
                    break;
                case 2:
                    source.PlayOneShot(step3);
                    break;
                default:
                    break;
            }
        }

        public void PlayRandomFireballSound()
        {
            int sample = Random.Range(0, 2);
            switch (sample)
            {
                case 0:
                    source.PlayOneShot(fireball1);
                    break;
                case 1:
                    source.PlayOneShot(fireball2);
                    break;
                default:
                    break;
            }
        }

        public void PlayRandomSwingSound()
        {
            int sample = Random.Range(0, 3);
            switch (sample)
            {
                case 0:
                    source.PlayOneShot(swing1);
                    break;
                case 1:
                    source.PlayOneShot(swing2);
                    break;
                case 2:
                    source.PlayOneShot(swing3);
                    break;
                default:
                    break;
            }
        }

        public void PlayArrowWhooshSound()
        {
            source.PlayOneShot(arrowWhoosh);
        }

        public void PlayOuch()
        {
            source.PlayOneShot(ouch);
        }

        public void PlayFbiOpenUp()
        {
            source.PlayOneShot(fbiOpenUp);
        }

        public void PlayOpenDoors()
        {
            source.PlayOneShot(openDoors);
        }

		public void PlayDragonEntry()
        {
            source.PlayOneShot(dragonEntry);
        }
        #endregion

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
            _instance.OnStart();
        }

        /// <summary>
        ///     Returns actor present at given position (returns null if no actor is present)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Actor GetActorAt((int x, int y) position)
        {
            return _allActors.FirstOrDefault(actor => actor.Detectable && actor.Position == position);
        }

        /// <summary>
        ///     Returns actor of specific subclass present at given position (returns null if no actor is present)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="position"></param>
        /// <returns></returns>
        public T GetActorAt<T>((int x, int y) position) where T : Actor
        {
            return _allActors.FirstOrDefault(actor => actor.Detectable && actor is T && actor.Position == position) as T;
        }

        /// <summary>
        ///     Returns actor present at given position (returns null if no actor is present)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public HashSet<Actor> GetAllActors()
        {
            return _allActors;
        }


        /// <summary>
        ///     Unregisters given actor (use when killing/destroying)
        /// </summary>
        /// <param name="actor"></param>
        public void DestroyActor(Actor actor)
        {
            _allActors.Remove(actor);
            Destroy(actor.gameObject);
        }

        /// <summary>
        ///     Used for cleaning up the scene before loading a new map
        /// </summary>
        public void DestroyAllActors()
        {
            var actors = _allActors.ToArray();

            foreach (var actor in actors)
			{
				if(actor.Destroyable){
                	DestroyActor(actor);
				} 

			}
        }

        /// <summary>
        ///     Returns sprite with given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sprite GetSprite(int id)
        {
            return _spriteAtlas.GetSprite($"kenney_transparent_{id}");
        }

        /// <summary>
        ///     Spawns given Actor type at given position
        /// </summary>
        /// <typeparam name="T">Actor type</typeparam>
        /// <param name="position">Position</param>
        /// <param name="actorName">Actor's name (optional)</param>
        /// <returns></returns>
        public T Spawn<T>((int x, int y) position, string actorName = null) where T : Actor
        {
            return Spawn<T>(position.x, position.y, actorName);
        }

        /// <summary>
        ///     Spawns given Actor type at given position
        /// </summary>
        /// <typeparam name="T">Actor type</typeparam>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="actorName">Actor's name (optional)</param>
        /// <returns></returns>
        public T Spawn<T>(int x, int y, string actorName = null) where T : Actor
        {
            var go = new GameObject();
            go.AddComponent<SpriteRenderer>();

            var component = go.AddComponent<T>();

            go.name = actorName ?? component.DefaultName;
            component.Position = (x, y);
            component.index = _actorIndex;
            _actorIndex++;

            _allActors.Add(component);

            return component;
        }
    }
}
