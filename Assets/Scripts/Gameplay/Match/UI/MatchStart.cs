using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RM_EM
{
    // The match start script.
    // Set the animator to use unscaled time since it pauses and unpauses the match.
    public class MatchStart : MonoBehaviour
    {
        // The manager.
        public MatchManager manager;

        // The match UI.
        public MatchUI matchUI;

        // The animator.
        public Animator animator;

        // The display text.
        public TMP_Text displayText;

        // Start is called before the first frame update
        void Start()
        {
            // Manager
            if (manager == null)
                manager = MatchManager.Instance;

            // Match UI
            if(matchUI == null)
                matchUI = manager.matchUI;
        }

        // Plays the start animation.
        public void PlayStartAnimation()
        {
            animator.Play("Match Start Animation");
        }

        // Called when the start animation is started.
        public void OnStartAnimationStart()
        {
            // Pause the match.
            manager.PauseMatch();

            // Pause the background music.
            if (manager.matchAudio != null)
                manager.matchAudio.bgmSource.Pause();
        }

        // TEXT //
        // It should be noted that text takes a while to update.

        // Resets the text.
        public void ResetText()
        {
            displayText.text = "...";
        }

        // Sets the text to "Ready".
        public void SetTextToReady()
        {
            // Text object.
            string text = "";

            // Setting the text.
            if(false)
            {
                // The language key.
                string key = "mth_ready";

                // Sets the text.
                text = LOLManager.Instance.GetLanguageText(key);

                // If TTS should be used.
                if (GameSettings.Instance.UseTextToSpeech)
                {
                    // Reads the key.
                    LOLManager.Instance.SpeakText(key);
                }
            }
            else
            {
                text = "Ready...";
            }

            // Set object.
            displayText.text = text;
        }

        // Sets the text to "Start"
        public void SetTextToStart()
        {
            // Text object.
            string text = "";

            // Setting the text.
            if (false)
            {
                // The language key.
                string key = "mth_start";

                // Sets the text.
                text = LOLManager.Instance.GetLanguageText(key);

                // If TTS should be used.
                if (GameSettings.Instance.UseTextToSpeech)
                {
                    // Reads the key.
                    LOLManager.Instance.SpeakText(key);
                }
            }
            else
            {
                text = "Start!";
            }

            // Set object.
            displayText.text = text;
        }

        // AUDIO //
        // Plays the match start sound effect.
        public void PlayMatchStartSfx()
        {
            // Plays the sound effect.
            if (manager.matchAudio != null)
                manager.matchAudio.PlayMatchStartSfx();
        }


        // END //
        // Called when the start animation is ended.
        public void OnStartAnimationEnd()
        {
            // Unpauses the match.
            manager.UnpauseMatch();

            // Unpause the background music.
            if(manager.matchAudio != null)
                manager.matchAudio.bgmSource.UnPause();

            // Plays the empty animation.
            PlayEmptyAnimation();
        }

        // Plays the empty animation.
        public void PlayEmptyAnimation()
        {
            // Plays the empty animation to reset the animation trigger.
            if (animator != null)
                animator.Play("Empty");
        }

    }
}