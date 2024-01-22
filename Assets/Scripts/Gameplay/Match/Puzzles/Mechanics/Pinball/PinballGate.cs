using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The gate for the pinball game.
    public class PinballGate : MonoBehaviour
    {
        // The match manager.
        public MatchManager matchManager;

        // The pinball mechanic.
        public PinballMechanic mechanic;

        // The collider of the gate.
        public new BoxCollider2D collider;

        // The sprite renderer.
        public SpriteRenderer spriteRenderer;

        [Header("Gate")]

        // Gets set to 'true' if the gate is open.
        public bool openedGate = false;

        // The max weight that can be handled before the gate opens.
        public float maxWeight = 5.0F;

        // The colour for the gate when the weight is at its minimum (0).
        [Tooltip("The gate colour when the gate is at its minimum weight.")]
        public Color weightMinClr = Color.green;

        // The colour for the gate when the weight is at its maximum.
        [Tooltip("The gate colour when the gate is at its maximum weight.")]
        public Color weightMaxClr = Color.red;

        // The timer for how long the gate stays open for.
        public float openTimer = 0.0F;

        // The maximum time for the gate to be open.
        public float openTimerMax = 10.0F;

        // The balls touching the gate.
        public List<BallValue> touchingBalls = new List<BallValue>();

        // Start is called before the first frame update
        void Start()
        {
            // Sets the manager instance.
            if (matchManager == null)
                matchManager = MatchManager.Instance;
        }
        
        // OnCollisionEnter2D
        private void OnCollisionEnter2D(Collision2D collision)
        {
            BallValue ball;

            // Tries to get the ball component.
            if(collision.gameObject.TryGetComponent<BallValue>(out ball))
            {
                // Add to the list.
                if(!touchingBalls.Contains(ball))
                    touchingBalls.Add(ball);
            }
        }

        // OnCollisionExit2D
        private void OnCollisionExit2D(Collision2D collision)
        {
            BallValue ball;

            // Tries to get the ball component.
            if (collision.gameObject.TryGetComponent<BallValue>(out ball))
            {
                // Remove from the list.
                if (touchingBalls.Contains(ball))
                    touchingBalls.Remove(ball);
            }
        }

        // I don't think this gets used...
        // OnMouseDown
        private void OnMouseDown()
        {
            // If the pinball gate has been clicked on, change the gate.
            if(openedGate)
            {
                CloseGate();
            }
            else
            {
                OpenGate();
            }
        }

        // Checks if the pinball gate is open.
        public bool IsOpen()
        {
            return openedGate;
        }

        // TODO: change how the door opens and closes, and allow the player to manually open it.
        // Opens the gate.
        public void OpenGate()
        {
            // Open the gate.
            openedGate = true;

            // Disables door and hides the sprite.
            collider.enabled = false;
            spriteRenderer.enabled = false;

            // Set the timer to max.
            ResetOpenTimerToMax();

            // Resets the colour.
            spriteRenderer.color = weightMinClr;
        }

        // Closes the gate.
        public void CloseGate()
        {
            // Close the gate.
            openedGate = false;

            // Enables door and shows the sprite.
            collider.enabled = true;
            spriteRenderer.enabled = true;

            // Set timer to 0.
            openTimer = 0.0F;

            // Resets the colour.
            spriteRenderer.color = weightMinClr;
        }

        // Set the timer to max.
        public void ResetOpenTimerToMax()
        {
            openTimer = openTimerMax;
        }

        // Calculates the weight being applied on the gate.
        public float CalculateAppliedWeight()
        {
            // The balls in contact with the gate (direct and in-direct).
            List<BallValue> contactBalls = new List<BallValue>();

            // The sum of the weights.
            float weightSum = 0.0F;

            // Goes through the balls touching the platform directly.
            foreach(BallValue ball in touchingBalls)
            {
                // Add to the contact balls list in a recursive loop.
                ball.AddTouchingBalls(ref contactBalls);
            }

            // OLD
            // // Goes through all touching balls.
            // for (int i = 0; i < touchingBalls.Count; i++)
            // {
            //     // The current ball.
            //     BallValue currentBall = touchingBalls[i];
            // 
            //     // If the ball isn't in the list, add it.
            //     if (!contactBalls.Contains(currentBall))
            //         contactBalls.Add(currentBall);
            // 
            //     // TODO: account for the balls touching other balls, but not the gate itself.
            //     // TODO: there needs to be a better way to do this. Maybe do something with the physics engine?
            // 
            //     
            // }
           
            // Sum up the weights of the balls.
            foreach(BallValue ball in contactBalls)
            {
                // Add to the current ball's weight.
                weightSum += ball.GetWeight();
            }


            // GATE COLOUR
            // Sets the gate colour
            // The colour's T value.
            float colorT = Mathf.Clamp01(weightSum / maxWeight);

            // Gets the new color.
            Color newColor = new Color(
                Mathf.Lerp(weightMinClr.r, weightMaxClr.r, colorT),
                Mathf.Lerp(weightMinClr.g, weightMaxClr.g, colorT),
                Mathf.Lerp(weightMinClr.b, weightMaxClr.b, colorT)
                );

            // Set the new color.
            spriteRenderer.color = newColor;


            // Return the weight sum.
            return weightSum;
        }

        // Called when the user has interacted with the pinball gate.
        public void OnInteract()
        {
            // TODO: if the gate is open, don't allow the player to interact with it.
            // Checks if the gate is open.
            if(IsOpen())
            {
                CloseGate();
            }
            else
            {
                OpenGate();   
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Checks if the manager has been paused.
            if(!matchManager.MatchPaused)
            {
                // The gate is open or closed.
                if(openedGate)
                {
                    // The open timer is going.
                    if (openTimer > 0.0F)
                    {
                        // Reduce timer.
                        openTimer -= Time.deltaTime;

                        // Bounds check for timer.
                        if (openTimer < 0.0F)
                            openTimer = 0.0F;
                    }

                    // Close the gate if the open timer is zero.
                    if (openTimer == 0.0F)
                        CloseGate();
                }
                else
                {
                    // If balls are touching the gate.
                    if(touchingBalls.Count > 0)
                    {
                        // Checks if the weight is too much, causing the gate to open.
                        if (CalculateAppliedWeight() > maxWeight)
                        {
                            OpenGate();
                        }
                    }
                    
                }

            }
            
        }
    }
}