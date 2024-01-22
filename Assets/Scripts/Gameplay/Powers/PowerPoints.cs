using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // Adjust the amount of points the target gets momentarily.
    public class PowerPoints : Power
    {
        [Header("Points")]

        // The target of the points power.
        public PlayerMatch target;

        // If set to 'true', the target is auto set.
        [Tooltip("Autosets the target if it's equal to null.")]
        public bool autoSetTarget = true;

        // Checks if the target is the 'user', or if it's the enemy.
        public bool targetIsUser = true;

        // The multiplier.
        public float multiplier = 1.0F;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Autoset information.
            if (multiplier > 1.0F && targetIsUser) // Points Plus
            {
                // Autoset Type
                if (power == powerType.none)
                    power = powerType.pointsPlus;

                // Autoset Name
                if (powerName == string.Empty)
                    powerName = "Points Up";

                // Autoset description.
                if (powerDesc == string.Empty)
                    powerDesc = "Increases the number of points the user gets for a limited time.";
            }
            else if(multiplier < 1.0F && !targetIsUser) // Points Minus
            {
                // Autoset Type
                if (power == powerType.none)
                    power = powerType.pointsMinus;

                // Autoset Name
                if (powerName == string.Empty)
                    powerName = "Points Down";

                // Autoset description.
                if (powerDesc == string.Empty)
                    powerDesc = "Decreases the number of points the opponent gets for a limited time.";
            }



            // If the target should be auto-set, and the target is null.
            if (autoSetTarget && target == null)
            {
                // Checks target.
                if(targetIsUser) // Target is user.
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

            // Applies the point multipler.
            if (multiplier > 0)
            {
                target.pointsMultiplier *= multiplier;
            }
            
        }

        // Called when the power is finished.
        public override void OnPowerFinished()
        {
            base.OnPowerFinished();

            // Removes the point multipler.
            if(multiplier > 0)
            {
                target.pointsMultiplier /= multiplier;
            }
        }

        // Updates the power.
        public override void UpdatePower()
        {
            // Nothing
        }
    }
}