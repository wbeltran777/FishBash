﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FishBash
{

    public interface IFish
    {
        float Speed { get; set; }
        bool HasBeenHit { get; set; }

        bool HasLeaped { get; }

        /// <summary>
        /// Checks if the fish is within a set radius of the fish's goal
        /// </summary>
        /// <returns>True if it is close</returns>
        bool CheckRadius(float radius);

        int FishId { get; }

        int ScoreVal { get; }

        void Reclaim();

        void HitFish();

        void MarkDestroyed();

        void HomingHit(Vector3 initialPos, Vector3 finalPos, Vector3 dirOfImpact, Vector3 velocity);

        FishPool Pool{ get; set;}

        GameObject GameObject { get; }

        float Distance { get; }

    }
}
