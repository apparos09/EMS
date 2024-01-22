using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // Twists the opponent's render 360 degrees.
    public class PowerTwist : Power
    {
        [Header("Twist")]

        // The target of the twist power.
        public PlayerMatch target;

        // The target's render.
        public PuzzleRender targetRender;

        // The target's camera.
        public Camera targetCamera;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Autoset Type
            if (power == powerType.none)
                power = powerType.twist;

            // Autoset Name
            if (powerName == string.Empty)
                powerName = "Power Twist";

            // Autoset description.
            if (powerDesc == string.Empty)
                powerDesc = "This user turns the target's puzzle upside down.";



            // Target not set.
            if(target == null)
            {
                // Checks if the user of this power is player 1 or player 2.
                if (playerMatch == manager.p1) // Onwer is P1
                {
                    // P2
                    target = manager.p2;
                }
                else if (playerMatch == manager.p2) // Owner is P2
                {
                    // P1
                    target = manager.p1;
                }
            }

            // Checks if the opponent render is set.
            if(targetRender == null)
            {
                targetRender = target.puzzle.puzzleRender;
            }
            
            // Gets the render camera.
            if(targetCamera == null)
            {
                targetCamera = targetRender.renderCamera;
            }
           
        }

        // TODO: rotate in increments instead of all at once. Going to need to change some things here.

        // Called when the power is started.
        public override void OnPowerStarted()
        {
            base.OnPowerStarted();

            // Rotates the opponent's camera (formally render) by 180 degrees.
            
            // Render
            // targetRender.gameObject.transform.Rotate(Vector3.forward, 180.0F);
            
            // Camera
            targetCamera.gameObject.transform.Rotate(Vector3.forward, 180.0F);
        }

        // Called when the power is over.
        public override void OnPowerFinished()
        {
            base.OnPowerFinished();

            // Rotates the opponent's camera (formally render) by 180 degrees again.

            // Render
            // targetRender.gameObject.transform.Rotate(Vector3.forward, -180.0F);

            // Camera
            targetCamera.gameObject.transform.Rotate(Vector3.forward, -180.0F);
        }

        // Updates the power.
        public override void UpdatePower()
        {
            // Nothing
        }

        // Update is called once per frame
        protected override void Update()
        {
            // TODO: use this to make gradual rotation.
            base.Update();
        }
    }
}