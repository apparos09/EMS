using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RM_EM
{
    // The match end.
    public class MatchEnd : MonoBehaviour
    {
        // The match manager.
        public MatchManager matchManager;

        // The match UI.
        public MatchUI matchUI;

        // The match over text.
        public TMP_Text matchOverText;

        // The match over text.
        public TMP_Text playerWinText;

        [Header("Buttons")]
        // The world button.
        public Button worldButton;

        // The replay button.
        public Button rematchButton;

        // The quit button.
        public Button quitButton;

        // Start is called before the first frame update
        void Start()
        {
            SetManagerAndUI();
        }

        // Sets the manager and the UI.
        private void SetManagerAndUI()
        {
            // Set Manager
            if (matchManager == null)
                matchManager = MatchManager.Instance;

            // Set UI
            if (matchUI == null)
                matchUI = matchManager.matchUI;
        }

        // Sets the player win text.
        public void SetPlayerWinText()
        {
            SetManagerAndUI();

            // The language key.
            string langKey = "";

            // Checks who has won.
            if (matchManager.HasPlayer1Won()) // P1
            {
                langKey = "mth_userWins";
                playerWinText.text = "You Won!";
            }
            else if (matchManager.HasPlayer2Won()) // P2
            {
                langKey = "mth_oppWins";
                playerWinText.text = "The Opponent Won!";
            }
            else // None
            {
                langKey = "mth_matchOver";
                playerWinText.text = "-";
            }


            // If text-to-speech is active and available.
            if(GameSettings.Instance.UseTextToSpeech && SystemManager.IsLOLSDKInitialized())
            {
                // Speak the text.
                // If the tutorial text box is open, this doesn't happen since it overrides the text box TTS.
                if(!matchManager.IsTutorialTextBoxOpen())
                    SystemManager.Instance.SpeakText(langKey);
            }
        }

        // Return to the game world.
        public void ToWorldScene()
        {
            SetManagerAndUI();

            matchUI.ToWorldScene();
        }

        // Replays the match.
        public void ReplayMatch()
        {
            SetManagerAndUI();

            matchUI.ReplayMatch();
        }

        // Quits the game.
        public void QuitGame()
        {
            SetManagerAndUI();

            matchUI.QuitGame();
        }

    }
}