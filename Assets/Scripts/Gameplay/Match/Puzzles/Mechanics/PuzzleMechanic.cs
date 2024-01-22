using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // A script for a puzzle mechanic/set of mechanics.
    public abstract class PuzzleMechanic : MonoBehaviour
    {
        // The match manager.
        public MatchManager manager;

        // The puzzle this mechanic belongs too.
        public Puzzle puzzle;

        // The list of puzzle values in the mechanic.
        public List<PuzzleValue> puzzleValues = new List<PuzzleValue>();

        // If set to 'true', the puzzle values are auto-filled.
        [Tooltip("Autofills the puzzleValues list if it's empty if this is set to 'true'.")]
        public bool autoFillPuzzleValues = true;


        // Start is called before the first frame update
        protected virtual void Start()
        {
            // If the manager isn't set, grab the instance.
            if (manager == null)
                manager = MatchManager.Instance;

            // If the puzzle values list is empty, try searching for values.
            if (autoFillPuzzleValues && puzzleValues.Count == 0)
            {
                // Puts the values in the list.
                GetComponentsInChildren<PuzzleValue>(true, puzzleValues);

                // NOTE: this should not be needed.
                //// Removes null values.
                //for(int i = puzzleValues.Count - 1; i >= 0; i--)
                //{
                //    // Remove at the set index.
                //    if (puzzleValues[i] == null)
                //        puzzleValues.RemoveAt(i);
                //}
            }
        }

        // Called to update the mechanic.
        public abstract void UpdateMechanic();

        // Resets the mechanic.
        public abstract void ResetMechanic();

        // Update is called once per frame
        protected virtual void Update()
        {
            // Updates the mechanic if the game isn't paused.
            if(!manager.MatchPaused)
            {
                UpdateMechanic();
            }
        }
    }
}