using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The bubble piece.
    public class BubbleValue : PuzzleValue
    {
        // The bubble mechanic.
        public BubbleMechanic mechanic;

        // The bubble value's rigidbody.
        public new Rigidbody2D rigidbody;


        [Header("Life")]
        // How long the bubble is alive for.
        public float lifeTimer = 10.0F;

        // The life timer max.
        public float lifeTimerMax = 10.0F;

        [Header("Movement")]

        // The force applied to the bubble when it's not moving.
        public float moveForce = 1;

        // The force direction of the bubble.
        // TODO: maybe change this if the bubble hits something?
        public Vector2 forceDirec = Vector2.right;

        // Scales the force if set to true. Bigger bubbles move slower, small bubbles move faster.
        [Tooltip("Scales the movement force based on the bubble's scale. The smaller the scale, the stronger the force.")]
        public bool scaleForce = false;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Grabs the rigidbody.
            if(rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody2D>();
            }
        }

        // LIFE TIMER
        // Sets the life timer to the max.
        public void SetLifeTimerToMax()
        {
            lifeTimer = lifeTimerMax;
        }

        // FORCE
        // Adds force to the bubble.
        public void AddForceToBubble()
        {
            // Calculates the force.
            Vector2 force = forceDirec * moveForce;

            // If the force should be scaled.
            if (scaleForce)
            {
                // Averages out the scale.
                float avgScale = (transform.localScale.x + transform.localScale.y) / 2.0F;
                
                // Apply the scale.
                force *= Mathf.Pow(avgScale, -1);
            }
                
            // Add to the rigidbody.
            rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        // Kills the bubble.
        public void Kill()
        {
            // Seet timer to 0.
            lifeTimer = 0;

            // Return bubble.
            mechanic.ReturnBubble(this);
        }

        // OnHit Function
        public override void OnHit(bool rightAnswer)
        {
            // Kill the bubble.
            Kill();

            // Plays a SFX
            if (manager.matchAudio != null)
                manager.matchAudio.PlayPuzzleValueSelectSfx();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // If the match isn't paused.
            if(!mechanic.manager.MatchPaused)
            {
                // MOVEMENT
                // Moves the bubble if its stationary.
                if (rigidbody.velocity == Vector2.zero)
                    AddForceToBubble();


                // TIMER
                // Reduce timer.
                lifeTimer -= Time.deltaTime;

                // Set life timer to 0 if negative.
                if (lifeTimer < 0)
                    lifeTimer = 0;
            
                // If the life timer is set to 0.
                if(lifeTimer == 0)
                {
                    // Kills the bubble.
                    Kill();
                }
            }
        }
    }
}