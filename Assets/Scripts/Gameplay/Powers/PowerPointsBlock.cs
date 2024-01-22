using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // A power that temporarily blocks the user from gaining more points.
    public class PowerPointsBlock : Power
    {
        [Header("Points Block")]

        // The target of the points block power.
        public PlayerMatch target;

        // The percentage of the points bar that gets blocked when the power is activated.
        public float blockPercent = 0.25F;

        // The original points the target has.
        public float origPoints = -1.0F;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update's the target's points bar.
        private void UpdateTargetPointsBar()
        {
            // Checks which points bar to update.
            if (target == manager.p1)
            {
                manager.matchUI.UpdatePlayer1PointsBar();
            }
            else if (target == manager.p2)
            {
                manager.matchUI.UpdatePlayer2PointsBar();
            }
        }

        // Called when the power has been started.
        public override void OnPowerStarted()
        {
            base.OnPowerStarted();

            // Saves the target's current points.
            origPoints = target.points;

            // Calculates the points reduction based on the points goal.
            float pointsReduct = Mathf.Ceil(manager.pointGoal * blockPercent);

            // Remove the number of points.
            target.points -= pointsReduct;

            // Updates the points bar.
            UpdateTargetPointsBar();
            

            // TODO: add some kind of animation or indicator for the block.
        }

        // Called when the power is finished.
        public override void OnPowerFinished()
        {
            base.OnPowerFinished();

            // If the target's points is less than their original point count...
            // It means they did not break the blockade. If so, restore their original point count.
            // If the target has surpassed their original point count, then the block was broken, so do nothing.
            if(target.points < origPoints)
            {
                // Set poitns back to normal.
                target.points = origPoints;

                // Updates the points bar.
                UpdateTargetPointsBar();
            }

            // Reset.
            origPoints = -1.0F;

            // TODO: add some kind of animation or indicator for the block.
        }

        // Updates the power.
        public override void UpdatePower()
        {
            // Nothing
        }
    }
}