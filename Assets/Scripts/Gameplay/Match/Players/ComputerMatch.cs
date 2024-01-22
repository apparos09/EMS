using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RM_EM
{
    // The script for computer players in matches.
    public class ComputerMatch : PlayerMatch
    {
        [Header("Computer")]

        // The target value to be selected.
        public PuzzleValue targetValue;

        // The difficulty of the computer.
        private int difficulty = 0;

        [Header("AI")]

        // The wait timer between movements.
        public float waitTimer = 0.0F;

        // The max wait time.
        public float waitTimeMax = 2.5F;

        // The move speed.
        public float moveSpeed = 5.0F;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // The computer does not use moue touch, so don't check for that.
            useMouseTouch = false;
        }

        // Gets the difficulty.
        public int GetDifficulty()
        {
            return difficulty;
        }

        // Sets the difficulty of the computer.
        public void SetDifficulty(int newDiff)
        {
            // Sets the difficulty.
            difficulty = newDiff;

            // Changes the difficulty settings.
            switch(difficulty)
            {
                default: // 0
                    waitTimeMax = 4.00F;
                    moveSpeed = 4.0F;
                    break;

                case 1: // Level 1
                    waitTimeMax = 4.80F;
                    moveSpeed = 2.00F;
                    break;

                case 2: // Level 2
                    waitTimeMax = 4.70F;
                    moveSpeed = 2.20F;
                    break;

                case 3: // Level 3 (Normal)
                    waitTimeMax = 4.60F;
                    moveSpeed = 2.40F;
                    break;

                case 4: // Level 4
                    waitTimeMax = 4.50F;
                    moveSpeed = 2.60F;
                    break;

                case 5: // Level 5
                    waitTimeMax = 4.40F;
                    moveSpeed = 2.80F;
                    break;

                case 6: // Level 6
                    waitTimeMax = 4.30F;
                    moveSpeed = 3.00F;
                    break;

                case 7: // Level 7
                    waitTimeMax = 4.20F;
                    moveSpeed = 3.20F;
                    break;

                case 8: // Level 8
                    waitTimeMax = 4.10F;
                    moveSpeed = 3.40F;
                    break;

                case 9: // Level 9
                    waitTimeMax = 4.00F;
                    moveSpeed = 3.60F;
                    break;

            }
        }


        // RETICLE COLLISION CALLS

        // Called when the reticle trigger continually collides with something.
        public override void OnReticleTriggerStay(Collider other)
        {
            TryGetPuzzleElement(other.gameObject);
        }

        // Called when the reticle trigger continually collides with something.
        public override void OnReticleTriggerStay2D(Collider2D collision)
        {
            TryGetPuzzleElement(collision.gameObject);
        }


        // Tries to get the correct puzzle element from the provided object.
        public bool TryGetPuzzleElement(GameObject hitObject)
        {
            PuzzleValue pv;

            // Attempts to select the value from the hit object and see if it's the correct one.
            // Tries to get the component.
            if(hitObject.TryGetComponent<PuzzleValue>(out pv))
            {
                // If the puzzle hasn no missing values, clear the target and return false.
                if (puzzle.missingValues.Count == 0)
                {
                    targetValue = null;
                    return false;
                }
                    
                // Puzzle has missing values.
                // If the element's value is the same as the next missing value.
                if(pv.value == puzzle.missingValues.Peek().value)
                {
                    // Selects the value since it's the correct one, and returns true.
                    puzzle.SelectValue(this, pv);

                    targetValue = null; // No target value set now.
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Called when an equation is generated.
        public override void OnEquationGenerated()
        {
            base.OnEquationGenerated();
            waitTimer = waitTimeMax;
        }

        // Called when the equation has been completed.
        public override void OnEquationComplete()
        {
            base.OnEquationComplete();
            waitTimer = waitTimeMax;
        }


        // Runs the computer AI.
        public void UpdateAI()
        {
            // Checks if the wait timer is going.
            if(waitTimer > 0.0F)
            {
                // Reduce timer.
                waitTimer -= Time.deltaTime;

                // The wait timer is negative, so set it to 0.
                if (waitTimer < 0.0F)
                {
                    waitTimer = 0.0F;
                }

                // Still waiting, so return.
                return;
                    
            }

            // Tries to find the value in the puzzle space so that the reticle can move towards it.

            // If there are no missing values, do nothing?
            // TODO: this was made to address a quick error. Maybe there's a better way to handle it.
            if (puzzle.missingValues.Count == 0)
            {
                // Skips the equation since there are no missing values.
                // Always ignore the limit.
                SkipEquation(false);

                // Return.
                return;
            }
                

            // Gets the value needed.
            char value = puzzle.missingValues.Peek().value;

            // Gets set to 'true' if the value should be searched for.
            bool findValue = false;

            // If the target value isn't set, always search for it.
            if(targetValue == null)
            {
                findValue = true;
            }
            else
            {
                // Checks if the target value matches the desired value. If it doesn't, search for it.
                if (targetValue.value != value)
                    findValue = true;
            }

            // If the value should be searched for.
            if(findValue)
            {
                // Set to null to start off.
                targetValue = null;

                // TODO: have the computer sometimes get questions wrong?

                // The value options.
                List<PuzzleValue> valueOptions = new List<PuzzleValue>();

                // Checks the value list.
                for (int i = 0; i < puzzle.puzzleMechanic.puzzleValues.Count; i++)
                {
                    // The value is set.
                    if (puzzle.puzzleMechanic.puzzleValues[i] != null)
                    {
                        // If this has the correct value.
                        if (puzzle.puzzleMechanic.puzzleValues[i].value == value)
                        {
                            // Checks if the loop should be broken early.
                            bool breakEarly = false;

                            // Adds to the list of valeu options.
                            valueOptions.Add(puzzle.puzzleMechanic.puzzleValues[i]);

                            // Checks the puzzle type.
                            switch (puzzle.puzzle)
                            {
                                // If it's a keypad, there's one of every value, so break early.
                                case Puzzle.puzzleType.keypad:
                                    breakEarly = true;
                                    break;

                                default:
                                    breakEarly = false;
                                    break;
                            }

                            // If the loop should be broken early.
                            if(breakEarly)
                                break;
                        }
                    }
                }


                // Original
                // targetValue = puzzle.puzzleMechanic.puzzleValues[i];

                // Checks the number of value options. If tehre's only one, select that value.
                if (valueOptions.Count == 1)
                {
                    // Sets the target value.
                    targetValue = valueOptions[0];
                }
                else if (valueOptions.Count > 1) // There's multiple values, so check the closet one.
                {
                    targetValue = valueOptions[0];
                    
                    // Goes through all values.
                    foreach(PuzzleValue pv in valueOptions)
                    {
                        // Gets the current distance and the comparable distance.
                        float currDist = Vector3.Distance(reticle.transform.position, targetValue.transform.position);
                        float compDist = Vector3.Distance(reticle.transform.position, pv.transform.position);

                        // If the compared distance is smaller than the current distance, use the compared distance.
                        if(compDist < currDist)
                        {
                            // Set to the target value.
                            targetValue = pv;
                        }
                    }
                }

                
                // If the power is available for the computer.
                if(IsPowerAvailable())
                {
                    // The opponent
                    PlayerMatch opp = (this == manager.p2) ? manager.p1 : manager.p2;

                    // The percent gap between the computer and the opponent (player).
                    // Calculates how far the computer is to the goal compared to the opponent (percentage-wise)
                    // If the percentage gap is above a certain amount, the opponent uses their power.
                    float percentThreshold = 0.0F;

                    // Uses a power based on how far ahead the opponent is compared to the computer.
                    // Adjusted the power thresholds since hte higher difficulties won't be used.
                    switch(difficulty)
                    {
                        default: // Use power instantly.
                        case 0:
                            percentThreshold = -1.0F;
                            break;

                        case 1: // L01
                        case 2: // L02
                        case 3: // L03
                            percentThreshold = 0.30F; // 0.25F
                            break;

                        case 4: // L04
                        case 5: // L05
                        case 6: // L06
                            percentThreshold = 0.25F; // 0.20F
                            break;

                        case 7: // L07
                        case 8: // L08
                        case 9: // L09
                            percentThreshold = 0.20F; // 0.05F
                            break;
                    }

                    // If the percent threshold has been passed, use the power.
                    if (opp.points / manager.pointGoal - points / manager.pointGoal >= percentThreshold)
                    {
                        // Use the power.
                        UsePower();

                        // Plays the power use SFX.
                        if (manager.matchAudio != null)
                            manager.matchAudio.PlayPowerUseSfx();
                    }
                }
                
            }


            // If the target value is still not set, do nothing.
            if (targetValue == null)
                return;

            // NOTE: this moves to where the value physically is, which is outside of the game view.
            // The value is projected through a quad, which means it visually won't look right.
            // I won't fix this unless I plan to actually show reticles.

            Vector3 newPos = Vector3.MoveTowards(reticle.transform.position, targetValue.transform.position, moveSpeed * Time.deltaTime);
            reticle.transform.position = newPos;

            // TODO: set something to use skips?
        }

        // Resets the player.
        public override void ResetPlayer()
        {
            base.ResetPlayer();

            // Sets the wait time to max.
            waitTimer = waitTimeMax;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // Only run this update if the wrong answer delay isn't active.
            // Note that the computer should never pick wrong answers anyway.
            if(!IsWrongAnswerDelayActive())
            {
                // Run the computer's AI.
                UpdateAI();
            }
            
        }
    }
}