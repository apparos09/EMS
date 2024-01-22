using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // Power that changes the equations the target gets.
    public class PowerEquationChange : Power
    {
        [Header("Equation")]
        // The target of the power.
        public PlayerMatch target;

        // If set to 'true', the target is auto set.
        [Tooltip("Autosets the target if it's equal to null.")]
        public bool autoSetTarget = true;

        // Checks if the target is the 'user', or if it's the enemy.
        public bool targetIsUser = true;

        [Header("Equation/Terms")]
        // Modifiers the term count by 'change' by the following amount.
        // Set to negative if you want it to reduce the number of terms.
        [Tooltip("The change to the term count. Positive to go up, negative to go down.")]
        public int termCountChange = 2;

        // The original term counts (min, max) for the target when the power is active.
        public int origTermMax = 0;
        public int origTermMin = 0;

        [Header("Equation/Points")]

        // The points multiplier for the adjusted equation.
        public float pointsMultiplier = 1.0F;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // If the target should be auto-set, and the target is null.
            if (autoSetTarget && target == null)
            {
                // Checks target.
                if (targetIsUser) // Target is user.
                {
                    target = (manager.p1 == playerMatch) ? manager.p1 : manager.p2;
                }
                else // Target is enemy.
                {
                    target = (manager.p2 == playerMatch) ? manager.p2 : manager.p1;
                }
            }
        }

        // Called when the power has been started.
        public override void OnPowerStarted()
        {
            base.OnPowerStarted();

            // Save originals.
            origTermMax = target.puzzle.equationTermsMax;
            origTermMin = target.puzzle.equationTermsMin;

            // Adjust the terms count max and min.
            target.puzzle.equationTermsMax += termCountChange;
            target.puzzle.equationTermsMin += termCountChange;


            // Bounds checking to make sure these values are above 0.
            if (target.puzzle.equationTermsMax <= 0) // Max
                target.puzzle.equationTermsMax = 1;

            if (target.puzzle.equationTermsMin <= 0) // Min
                target.puzzle.equationTermsMin = 1;

            // Applies the point multipler.
            if(pointsMultiplier > 0)
            {
                target.pointsMultiplier *= pointsMultiplier;
            }

        }

        // Called when the power is finished.
        public override void OnPowerFinished()
        {
            base.OnPowerFinished();

            // Reset to originals.
            target.puzzle.equationTermsMax = origTermMax;
            target.puzzle.equationTermsMin = origTermMin;

            // Removes the point multipler.
            if(pointsMultiplier > 0)
            {
                target.pointsMultiplier /= pointsMultiplier;
            }
            
        }

        // Updates the power.
        public override void UpdatePower()
        {
            // ...
        }

    }
}