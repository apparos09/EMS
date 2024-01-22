using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The script for controlled players in matches.
    public class PlayerMatch : MonoBehaviour
    {
        // The match manager.
        public MatchManager manager;

        // If 'true', the player uses mouse touch.
        public bool useMouseTouch = true;

        // The reticle that's used to select the next value.
        public PlayerReticle reticle;

        [Header("Match")]

        // The puzzle the player is answering.
        public Puzzle puzzle;

        // Uses the wrong answer delay.
        public bool useWrongAnswerDelay = true;

        // The delay timer for the user making a wrong answer (delays the ability to select a new value).
        public float wrongAnswerDelayTimer = 0.0F;

        // The max for the wrong answer delay timer.
        public const float WRONG_ANSWER_DELAY_MAX = 0.90F;


        [Header("Match/Points")]
        // The amount of match points the player has.
        public float points = 0;

        // A multiplier for the points the player gets.
        public float pointsMultiplier = 1.0F;

        // Power
        [Header("Match/Power")]
        // The power the player has.
        public Power power = null;

        // Skip
        [Header("Match/Skip")]

        // If 'true', the skip feature is limited.
        public bool limitSkipping = true;

        // The skip points.
        public float skipPoints = 100;

        // The maximum amount of skip points.
        public float skipPointsMax = 100;

        // The amount of points that a skip costs.
        public float skipPointCost = 25;

        // The restoration speed for skipping.
        public float skipRestoreMult = 5.0F;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // Grab the instance.
            if (manager == null)
                manager = MatchManager.Instance;
        }

        // Called when the reticle trigger enters a collision with something.
        public virtual void OnReticleTriggerEnter(Collider other)
        {
            // ...
        }

        // Called when the reticle trigger enters a collision with something.
        public virtual void OnReticleTriggerEnter2D(Collider2D collision)
        {
            // ...
        }

        // Called when the reticle trigger enters a collision with something.
        public virtual void OnReticleTriggerStay(Collider other)
        {
            // ...
        }

        // Called when the reticle trigger enters a collision with something.
        public virtual void OnReticleTriggerStay2D(Collider2D collision)
        {
            // ...
        }

        // EQUATION //

        // WRONG ANSWER
        // Checks if the delay is acive.
        public bool IsWrongAnswerDelayActive()
        {
            // Checks if the delay should be used.
            if(useWrongAnswerDelay)
            {
                // Returns 'true' if the delay timer is above 0, false if it's 0 or below.
                return wrongAnswerDelayTimer > 0;
            }
            else
            {
                return false;
            }
        }

        // Sets the wrong answer delay timer.
        public void SetWrongAnswerDelayTimer(float maxTime)
        {
            // If the wrong answer delay should be used.
            if (useWrongAnswerDelay)
            {
                wrongAnswerDelayTimer = maxTime;

                // Plays the wrong answer animation.
                puzzle.PlayWrongAnswerAnimation();
            }
            else
            {
                wrongAnswerDelayTimer = 0;
            }
        }

        // Sets the wrong answer delay timer.
        public void SetWrongAnswerDelayTimer()
        {
            SetWrongAnswerDelayTimer(WRONG_ANSWER_DELAY_MAX);
        }

        // Updates the wrong answer delay timer.
        public void UpdateWrongAnswerDelayTimer()
        {
            // Timer is above 0.
            if(wrongAnswerDelayTimer > 0)
            {
                wrongAnswerDelayTimer -= Time.deltaTime;

                // Set to 0.
                if (wrongAnswerDelayTimer <= 0)
                    wrongAnswerDelayTimer = 0.0F;
            }
        }

        // POWER //
        // If the player has a power, return true.
        public bool HasPower()
        {
            // Checks if a power is set.
            bool result = power != null;

            return result;
        }

        // Sets the power to the provided power.
        // setPlayerAsParent - sets the player as the power's parent if it's not attached to the player directly.
        public void SetPower(Power newPower, bool setPlayerAsParent = true)
        {
            // Removes the current power.
            RemovePower();

            // Sets the power.
            power = newPower;

            // The power has been set.
            if(power != null)
            {
                // Sets the player.
                power.playerMatch = this;

                // Sets the transform parent if the component isn't attached to the player.
                if (setPlayerAsParent && power.gameObject != gameObject)
                {
                    power.transform.parent = transform;
                }
            }
            
        }

        // Sets the power using the provided type.
        // setPlayerAsParent - sets the player as the power's parent if it's not attached to the player directly.
        public void SetPower(Power.powerType type, bool setPlayerAsParent = true)
        {
            // The power prefabs.
            PowerInfo powerPrefabs = PowerInfo.Instance;

            // User and Opponent
            // The user.
            PlayerMatch user = this;

            // The opponent.
            if (manager == null) // Sets manager if it's not set already.
                manager = MatchManager.Instance;

            // Sets opponent.
            PlayerMatch opp = (this == manager.p1) ? manager.p2 : manager.p1;

            // POWERS
            // The new power for the player.
            Power newPower = null;

            // Derived powers (for player settings).
            PowerPoints powerPoints = null;
            PowerEquationChange powerEquation = null;
            PowerPointsTransfer powerTransfer = null;
            PowerPointsBlock powerBlock = null;
            PowerTwist powerTwist = null;

            // Checks the power type.
            switch (type)
            {
                // Generates the power for the player.
                default:
                case Power.powerType.none:
                    // Uses null instead of the nothing power.
                    // newPower = Instantiate(powerPrefabs.nothing);
                    newPower = null;
                    break;

                case Power.powerType.pointsPlus: // Points Plus
                    newPower = Instantiate(powerPrefabs.pointsPlus);

                    // Settings
                    powerPoints = (PowerPoints)newPower;
                    powerPoints.target = user;
                    powerPoints.targetIsUser = true;

                    break;

                case Power.powerType.pointsMinus: // Points Minus
                    newPower = Instantiate(powerPrefabs.pointsMinus);

                    // Settings
                    powerPoints = (PowerPoints)newPower;
                    powerPoints.target = opp;
                    powerPoints.targetIsUser = false;

                    break;

                case Power.powerType.equationShorten: // Equation Shorten
                    newPower = Instantiate(powerPrefabs.equationShorten);

                    // Settings
                    powerEquation = (PowerEquationChange)newPower;
                    powerEquation.target = user;
                    powerEquation.targetIsUser = true;

                    break;

                case Power.powerType.equationLengthen: // Equation Lengthen
                    newPower = Instantiate(powerPrefabs.equationLengthen);

                    // Settings
                    powerEquation = (PowerEquationChange)newPower;
                    powerEquation.target = opp;
                    powerEquation.targetIsUser = false;

                    break;

                case Power.powerType.pointsTransfer: // Points Transfer
                    newPower = Instantiate(powerPrefabs.pointsTransfer);

                    // Settings
                    powerTransfer = (PowerPointsTransfer)newPower;
                    powerTransfer.taker = user;
                    powerTransfer.giver = opp;

                    break;

                case Power.powerType.pointsBlock: // Points Block
                    newPower = Instantiate(powerPrefabs.pointsBlock);

                    // Settings
                    powerBlock = (PowerPointsBlock)newPower;
                    powerBlock.target = opp;
                    break;

                case Power.powerType.twist: // Render Twist
                    newPower = Instantiate(powerPrefabs.twist);

                    // Settings
                    powerTwist = (PowerTwist)newPower;
                    powerTwist.target = opp;

                    break;
            }

            // // Sets the player match (is handled by the other function).
            // if(newPower != null)
            //     newPower.playerMatch = this;

            // Sets the power.
            SetPower(newPower, setPlayerAsParent);
        }

        // Returns the power type of the player's power.
        public Power.powerType GetPowerType()
        {
            // Checks if the player has a power.
            if(HasPower()) // Has power.
            {
                // Returns the type.
                return power.power;
            }
            else // Has no power.
            {                
                // Returns none type.
                return Power.powerType.none;
            }
        }

        // Removes the power. If 'destroyPower' is true, the power component is destroyed.
        public void RemovePower(bool destroyPower = true)
        {
            // Power is set.
            if (power != null)
            {
                // If attached to the player, only destroy the component.
                if(power.gameObject == gameObject)
                {
                    Destroy(power);
                }
                // If not attached to the player, destroy the whole object.
                else if(power.gameObject != gameObject)
                {
                    Destroy(power.gameObject);
                }
            }
        }

        // Checks if the power is available.
        public bool IsPowerAvailable()
        {
            // Checks if the player has a power at all.
            if(HasPower())
            {
                bool result = power.IsPowerUsable();
                return result;
            }
            else
            {
                return false;
            }
        }

        // Call to use the power.
        public void UsePower()
        {
            // Checks if the player has a power.
            if(HasPower())
            {
                // Checks if the power is active.
                if (!IsPowerActive()) // Not active.
                {
                    // If the power is usable, use it.
                    if(power.IsPowerUsable())
                    {
                        power.UsePower();
                    }

                }

            }

        }

        // Checks if the player's power is active.
        public bool IsPowerActive()
        {
            // Checks if a power is equipped.
            if(power != null)
            {
                return power.powerActive;
            }
            else // Not equipped, so always false.
            {
                return false;
            }
        }

        // TODO: should this be moved?
        // SKIP

        // Returns 'true' if the player can skip the equation.
        public bool CanSkipEquation()
        {
            // If the skip option is available.
            if(!limitSkipping || (skipPoints - skipPointCost > 0))
            {
                return true;
            }
            else // Skip option is not available.
            {
                return false;
            }
        }

        // Skips the equation.
        public void SkipEquation(bool useLimit)
        {
            // Checks if the limit should be used.
            if(useLimit && limitSkipping)
            {
                // Skip valid.
                if(skipPoints - skipPointCost >= 0)
                {
                    // Reduce skip points.
                    skipPoints -= skipPointCost;
                }

                // Skip with no point changes.
                puzzle.SkipEquation();
            }
            else
            {
                // Skip with no point changes.
                puzzle.SkipEquation();
            }
        }


        // EQUATION //
        // Called when an equation is generated.
        public virtual void OnEquationGenerated()
        {
            // ...
        }

        // Called when the equation has been completed.
        public virtual void OnEquationComplete()
        {
            // ...
        }

        // Resets the player.
        public virtual void ResetPlayer()
        {
            // Points
            points = 0;
            
            // Power
            if(power != null)
            {
                // Set energy to 0.
                power.energy = 0;

                // Call power finished to disable the power.
                if(power.powerActive)
                    power.OnPowerFinished();

                // Power is not active.
                power.powerActive = false;
            }
            
            // Skip
            skipPoints = skipPointsMax;

            // If the player has a reticle, reset the position.
            if (reticle != null)
                reticle.ResetPosition();
        }


        // Update is called once per frame
        protected virtual void Update()
        {
            // Updates the wrong answer delay timer.
            UpdateWrongAnswerDelayTimer();

            // If the mouse touch should be used, and the mouse has been clicked.
            // Maybe split this conditional into 2? 
            if(useMouseTouch)
            {
                // This checks if the wrong answer delay is being used, and if the timer is active.
                if(!IsWrongAnswerDelayActive())
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        // TODO: maybe use the callbacks?

                        // OLD
                        // This gives a delayed click.
                        // // Grabs the mouse button.
                        // util.MouseButton mb = manager.mouseTouch.MouseButton0;

                        // NEW
                        // Generates a mouse button, and sets both held and last clicked as the hovered object.
                        // This is to make sure the hit is as updated (there was a delay using last clickedb efore).

                        // The hit info for the mouse button isn't referenced.
                        util.MouseButton mb = new util.MouseButton();

                        // Held
                        mb.held = manager.mouseTouch.mouseHoveredObject;
                        mb.heldHit = manager.mouseTouch.mouseHoveredHit;
                        mb.heldHit2D = manager.mouseTouch.mouseHoveredHit2D;

                        // Last Clicked
                        mb.lastClicked = manager.mouseTouch.mouseHoveredObject;
                        mb.lastClickedHit = manager.mouseTouch.mouseHoveredHit;
                        mb.lastClickedHit2D = manager.mouseTouch.mouseHoveredHit2D;


                        // Checks last clicked object if it's from the user's puzzle render.
                        if (mb.held != null && mb.held == puzzle.puzzleRender.gameObject)
                        {
                            // Grabs the object.
                            GameObject hit = puzzle.puzzleRender.TryHit(mb);

                            // Something was hit.
                            if (hit != null)
                            {
                                // Select the element.
                                puzzle.SelectElement(this, hit);
                            }
                        }
                    }

                
                }
            }

            // If skipping should be limited.
            if(limitSkipping)
            {
                // Skip points not at max.
                if(skipPoints < skipPointsMax)
                {
                    // Restores skip points.
                    skipPoints += Time.deltaTime * skipRestoreMult;

                    // Max reached.
                    if (skipPoints >= skipPointsMax)
                    {
                        // Set to max.
                        skipPoints = skipPointsMax;
                    }
                }
            }
            
        }
    }
}