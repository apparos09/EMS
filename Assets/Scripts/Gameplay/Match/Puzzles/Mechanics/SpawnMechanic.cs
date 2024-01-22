using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The spawn mechanic.
    public abstract class SpawnMechanic : PuzzleMechanic
    {
        [Header("Spawner")]
        // The maximum number of puzzle values.
        public int activeValueCountMax = 10;

        // The timer for spawning a piece.
        public float spawnTimer = 0.0F;

        // The max time it takes to spawn another piece.
        public float spawnTimeMax = 2.0F;

        // Returns the number of active values.
        public virtual int GetActivePuzzleValues()
        {
            return puzzleValues.Count;
        }

        // Checks if the puzzle values count reached the max.
        public virtual bool HasPuzzleValuesCountReachedMax()
        {
            return GetActivePuzzleValues() >= activeValueCountMax;
        }

        // Resets the spawn timer to the max.
        public void ResetSpawnTimerToMax()
        {
            spawnTimer = spawnTimeMax;
        }

        // Checks if the spawn timer is 0 or less.
        public bool IsSpawnTimerFinished()
        {
            return spawnTimer <= 0.0F;
        }

        // Count down the spawn timer.
        // If the spawn timer is 0, it returns true. If it's not 0, it returns false.
        public bool UpdateSpawnTimer()
        {
            // Checks if the timer is over.
            bool timerOver = false;

            // Checks if the timer is going.
            if (spawnTimer > 0)
            {
                // Reduce timer.
                spawnTimer -= Time.deltaTime;

                // Set to zero if negative.
                if (spawnTimer < 0)
                    spawnTimer = 0.0F;

                // CHecks if the time is over.
                timerOver = spawnTimer == 0;
            }
            else
            {
                timerOver = true;
            }

            return timerOver;
        }

        // Resets the mechanic.
        public override void ResetMechanic()
        {

        }

    }
}