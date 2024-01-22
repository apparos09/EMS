using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using util;

namespace RM_EM
{
    // TODO: properly implement this for the match UI.
    // The player UI.
    public class PlayerMatchUI : MonoBehaviour
    {
        // The manager for the match.
        public MatchManager manager;

        // The UI for the match.
        public MatchUI matchUI;

        // The player this UI is for.
        public PlayerMatch playerMatch;

        [Header("UI")]

        // The header text for the view.
        public TMP_Text headerText;

        // The player equation.
        public TMP_Text equationText;

        // The player match points bar.
        public ProgressBar pointsBar;

        // The player power bar.
        public ProgressBar powerBar;

        // The fill image for the player power bar.
        public Image powerBarFill;

        [Header("UI/Buttons")]

        // The power button for the player.
        public Button powerButton;

        // The power button image.
        public Image powerButtonImage;

        // The skip button for the player.
        public Button skipButton;

        [Header("Other")]
        // The power bar color when the player has a power.
        public Color hasPowerColor = Color.red;

        // The power bar color when the player has no power.
        public Color noPowerBarColor = Color.grey;

        // Start is called before the first frame update
        void Start()
        {
            // The manager.
            if (manager == null)
                manager = MatchManager.Instance;

            // Autoset match UI.
            if (matchUI == null)
                matchUI = manager.matchUI;


            // Setting the power button icon.
            // NOTE: this doesn't work because the player's power hasn't been saved yet.
            UpdatePlayerPowerButton();


            // Checks what the player is.
            if (manager.p1 == playerMatch)
            {
                // Translates the text to show the player header.
                if (LOLManager.IsLOLSDKInitialized())
                {
                    headerText.text = "-" + LOLManager.Instance.GetLanguageText("kwd_player") + "-";
                }
            }    
            // If player 2 is set.
            else if (manager.p2 == playerMatch)
            {
                // Translates the text to show the player header.
                if (LOLManager.IsLOLSDKInitialized())
                {
                    headerText.text = "-" + LOLManager.Instance.GetLanguageText("kwd_com") + "-";
                }

                // If player 2 is a computer player.
                if (manager.p2 is ComputerMatch)
                {
                    powerButton.interactable = false;
                    powerButton.gameObject.SetActive(false);

                    skipButton.interactable = false;
                    skipButton.gameObject.SetActive(false);
                }
            }

            
        }

        // Equation Display
        public void UpdatePlayerEquationDisplay()
        {
            // Gets the equation question formatted.
            if(manager.p1 == playerMatch)
            {
                equationText.text = manager.p1Puzzle.GetEquationQuestionFormatted();
            }
            else if(manager.p2 == playerMatch)
            {
                equationText.text = manager.p2Puzzle.GetEquationQuestionFormatted();
            }
        }


        // PROGRESS BARS //
        // POINTS BARS
        // Gets the percentage of the points bar filled.
        private float GetPointsGoalPercentage(float points)
        {
            // The percent to be returned.
            float percent = 0.0F;

            // Checks if the point goal is set, and if it should even be used at all.
            if (manager.usePointGoal)
            {
                if (manager.pointGoal <= 0)
                {
                    percent = 1.0F; // Always set at 100.
                }
                else
                {
                    // Calculates the percentage and clamps it.
                    percent = (points) / manager.pointGoal;
                    percent = Mathf.Clamp01(percent);
                }
            }
            else
            {
                // Set to max.
                percent = 1.0F;
            }


            return percent;
        }

        // Updates the player points bar.
        private void UpdatePlayerPointsBar(PlayerMatch player, ProgressBar bar)
        {
            // Percent
            float percent = GetPointsGoalPercentage(player.points);

            // Sets the points bar percentage.
            bar.SetValueAsPercentage(percent);
        }

        // Updates player's points bar.
        public void UpdatePlayerPointsBar()
        {
            // Updates player points bar using this function.
            if(manager.p1 == playerMatch)
            {
                UpdatePlayerPointsBar(manager.p1, pointsBar);
            }
            else if(manager.p2 == playerMatch)
            {
                UpdatePlayerPointsBar(manager.p2, pointsBar);
            }      
        }

        // POWERS //

        // Updates the player's power bar.
        public void UpdatePlayerPowerBarFill(PlayerMatch player, ProgressBar bar)
        {
            // Checks if a player has a power.
            bool hasPower;

            // Percent
            float percent;

            // Sees if the player has a power.
            hasPower = player.HasPower();

            // Does the player have a power?
            if (hasPower) // Get percentage.
            {
                percent = player.power.GetPowerFillPercentage();
            }
            else // Set to full (bar should be greyed out).
            {
                percent = 1.0F;
            }

            // Sets the points bar percentage.
            bar.SetValueAsPercentage(percent);
        }

        // Updates the power bar fill of the provided player.
        public void UpdatePlayerPowerBarFill()
        {
            // Checks match manager to see which player it is.
            if (manager.p1 == playerMatch)
            {
                UpdatePlayerPowerBarFill(manager.p1, powerBar);
            }
            else if (manager.p2 == playerMatch)
            {
                UpdatePlayerPowerBarFill(manager.p2, powerBar);
            }
        }

        // Updates the player power bar color.
        private void UpdatePlayerPowerBarColor(PlayerMatch player, ProgressBar bar, Image fillImage)
        {
            // Checks if the player has a power.
            bool hasPower = player.HasPower();

            // Fills the image.
            fillImage.color = (hasPower) ? hasPowerColor : noPowerBarColor;

            // If the player has no power, set the bar to be full. 
            if (!hasPower)
                bar.SetValueAsPercentage(1.0F, false);
        }

        // Updates player's power bar color.
        public void UpdatePlayerPowerBarColor()
        {
            // Checks match manager to see which player it is.
            if (manager.p1 == playerMatch)
            {
                UpdatePlayerPowerBarColor(manager.p1, powerBar, powerBarFill);
            }
            else if (manager.p2 == playerMatch)
            {
                UpdatePlayerPowerBarColor(manager.p2, powerBar, powerBarFill);
            }
        }


        // Updates the player power symbol.
        public void UpdatePlayerPowerButton()
        {
            // If the image is set, update the image.
            if (powerButtonImage != null)
                powerButtonImage.sprite = PowerInfo.Instance.GetPowerSymbol(playerMatch.GetPowerType());

            // Not needed. 
            powerButton.interactable = playerMatch.IsPowerAvailable();
        }


        // OPERATIONS //
        // Use Player's Power
        public void UsePlayerPower()
        {
            playerMatch.UsePower();
        }

        // Skips
        // Player Equation Skip
        public void UsePlayerEquationSkip()
        {
            // Uses limit setting.
            playerMatch.SkipEquation(playerMatch.limitSkipping);
        }

        // Update is called once per frame
        private void Update()
        {
            // If skipping is limited, check if the player can skip.
            // If skipping isn't limited, then keep it on.

            // If the match isn't paused.
            if(!manager.MatchPaused && !manager.IsTutorialTextBoxOpen())
            {
                skipButton.interactable = (playerMatch.limitSkipping) ? playerMatch.CanSkipEquation() : true;
            }

            // NOTE: if the skip button is disabled as part of some operation (window open, tutorial, etc.)...
            // This constant update will make sure the skip button is always given its right value.
        }

    }
}