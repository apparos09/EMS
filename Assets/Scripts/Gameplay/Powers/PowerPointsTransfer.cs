using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // Transfers points from one user to another.
    public class PowerPointsTransfer : Power
    {
        [Header("Points Transfer")]

        // Targets
        // Target taker (user)
        [Tooltip("The target taker of the power. This target takes points from the other target.")]
        public PlayerMatch taker;

        // Target giver (opponent)
        [Tooltip("The target giver of the power. This target gives points to the other target.")]
        public PlayerMatch giver;

        // The points the taker has, which is used to mark how much should be taken away from the giver.
        [Tooltip("The number of points the taker has.")]
        public float takerPoints = 0;

        // The percentage of points taken from the giver, based on how many points the taker just earned.
        [Tooltip("The percentage of points to be taken from the giver. The percentage is based on how many points the taker just earned.")]
        public float takePercent = 0.5F;


        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

        }

        // Called when the power is started.
        public override void OnPowerStarted()
        {
            base.OnPowerStarted();

            // Set points.
            takerPoints = taker.points;

            // // Increases points multiplier by the take percent.
            // taker.pointsMultiplier *= (1.0F + takePercent);
        }

        // Called when the power is over.
        public override void OnPowerFinished()
        {
            base.OnPowerFinished();

            // Clear points.
            takerPoints = 0;

            // // Undoes the points multiplier by the take percent.
            // taker.pointsMultiplier /= (1.0F + takePercent);
        }

        // Updates the power.
        public override void UpdatePower()
        {
            // The taker has gained points, so remove points from the giver.
            if(taker.points > takerPoints)
            {
                // The point increase.
                float pointsInc = taker.points - takerPoints;

                // Reduces the giver's points.
                float pointsReduct = pointsInc * takePercent;

                // Bounds checking.
                if (pointsReduct <= 0) // Less than 0, so set it to 1.
                {
                    pointsReduct = 1;
                }
                else // Round up to whole number.
                {
                    pointsReduct = Mathf.Ceil(pointsReduct);
                }

                // Reduce points.
                giver.points -= pointsReduct;

                // Bounds check for giver's points.
                if (giver.points < 0)
                    giver.points = 0;


                // Updates the giver's points bar.
                if(giver == manager.p1) // P1
                {
                    manager.matchUI.UpdatePlayer1PointsBar();
                }
                else if(giver == manager.p2) // P2
                {
                    manager.matchUI.UpdatePlayer2PointsBar();
                }

                // Save the new point total.
                takerPoints = taker.points;
            }
        }
    }
}