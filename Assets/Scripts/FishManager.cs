﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace FishBash
{
    public class FishManager : MonoBehaviour
    {
        public static FishManager instance = null;

        /// <summary>
        /// Returns fish remaining in current wave
        /// </summary>
        public int FishRemaining
        {
            get
            {
                return fishList.Count;
            }
        }

        [SerializeField] AudioClip[] fishSounds;

        [Range(1,10)]
        public float innerRadius;

        public GameObject platform;

        private IList<IFish> fishList;

        #region UNITY_METHODS
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        #endregion //UNITY_METHODS

        #region PUBLIC_METHODS
        /// <summary>
        /// Destroys the given fish after a set time
        /// </summary>
        /// <param name="toDestroy">Fish object to destroy</param>
        /// <param name="t">Time in seconds to wait before destroying</param>
        public void DestroyFish(IFish toDestroy, float t)
        {
            fishList.Remove(toDestroy);
            toDestroy.Destroy(t);
        }

        /// <summary>
        /// Destroys all active fish
        /// </summary>
        public void DestroyAllFish()
        {
            while (fishList.Count > 0)
            {
                DestroyFish(fishList[0], 0);
            }
                
        }

        /// <summary>
        /// Initializes fish list for new game
        /// </summary>
        public void InitializeFishList()
        {
            if (fishList != null)
            {
                fishList.Clear();
            }
            else
            {
                fishList = new List<IFish>();
            }
        }

        /// <summary>
        /// Spawns a fish in a fixed position
        /// </summary>
        /// <param name="f">Fish object</param>
        /// <param name="speedMultiplier">Speed multiplier for this wave</param>
        public GameObject SpawnFish(FishContainer f, float speedMultipler)
        {
            IFish fish;
            GameObject obj;
            (fish, obj) = SpawnFish(f.fishPrefab, f.spawnPositionOverride.Value, speedMultipler);
            fishList.Add(fish);
            return obj;
        }

        /// <summary>
        /// Spawns a fish in a random position
        /// </summary>
        /// <param name="f">Fish object</param>
        /// <param name="speedMultiplier">Speed multiplier for this wave</param>
        /// <param name="distance">Range of distance for this wave</param>
        /// <param name="distance">Range of angles (in radians) for this wave</param>
        public GameObject RandomSpawnFish(FishContainer f, float speedMultiplier, Vector2 distance, Vector2 angle)
        {
            Vector2 position = Utility.RandomPointOnUnitCircle(distance, angle);
            IFish fish;
            GameObject obj;
            (fish, obj) = SpawnFish(f.fishPrefab, f.spawnPositionOverride.GetValueOrDefault(position), speedMultiplier);
            fishList.Add(fish);
            return obj;
        }

        /// <summary>
        /// Creates a fish of the specified object at the given position with the given speed
        /// </summary>
        /// <param name="fishToSpawn">Fish Object to use</param>
        /// <param name="position">Vector2 specifying x and z position of the fish</param>
        /// <param name="speedMultiplier">Optional multiplier for the fish speed</param>
        /// <returns>IFish component created</returns>
        private (IFish,GameObject) SpawnFish(GameObject fishToSpawn, Vector2 position, float speedMultiplier = 1)
        {
            
            GameObject fish = Instantiate(fishToSpawn, new Vector3(position.x, 0, position.y), new Quaternion(), transform);
            SoundManager.instance.RandomizeSfxOnObject(fish, out _, fishSounds);
            fish.layer = 10;
            IFish toReturn = fish.GetComponent<IFish>();
            toReturn.Speed *= speedMultiplier;
            return (toReturn,fish);
        }

        #endregion //PUBLIC_METHODS


    }
}
