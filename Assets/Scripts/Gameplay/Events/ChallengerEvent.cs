using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // An event to unlock a challenger.
    public class ChallengerEvent : GameEvent
    {
        [Header("Challenger")]

        // The challenger to be unlocked.
        [Tooltip("The challenger this event is attached to.")]
        public ChallengerWorld challenger;

        // The challengers that must be beaten for this challenger to be available.
        [Tooltip("The challengers that must be beaten for this challenger to be made active.")]
        public List<ChallengerWorld> reqChallengers;


        // Start is called before the first frame update
        protected override void Start()
        {
            if(eventTag == "")
                eventTag = "Challenger";

            // Tries to get the challenger.
            if (challenger == null)
                challenger = GetComponent<ChallengerWorld>();

            // Calls base.Start() at the end to make sure everything's set.
            base.Start();
        }

        // Initializes the event.
        public override void InitalizeEvent()
        {
            // Make challenger unavailable.
            challenger.SetChallengerAvailable(false);

            // Disable the challenger - why do I do this?
            // Let's try not doing this.
            // challenger.enabled = false;
        }

        // Updates the event.
        public override void UpdateEvent()
        {
            // Checks if all challengers have been beaten.
            bool allBeaten = true;

            // Checks if there are prior challengers that need to be beatren.
            if (reqChallengers.Count > 0)
            {
                // Goes through the prior challengers.
                for (int i = 0; i < reqChallengers.Count; i++)
                {
                    // Not all challengers have been beaten.
                    if (!reqChallengers[i].IsChallengerDefeated())
                    {
                        allBeaten = false;
                        break;
                    }
                }
            }

            // Sets the cleared parameter.
            cleared = allBeaten;
        }

        // Completes the event.
        public override void OnEventComplete()
        {
            // Turn on the component again. Do I have to do this?
            // Let's try not doing this.
            // challenger.enabled = true;


            // Make challenger available.
            challenger.SetChallengerAvailable(true);

            // Calls the event complete base function.
            base.OnEventComplete();
        }

    }
}