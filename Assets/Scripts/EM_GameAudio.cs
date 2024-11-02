using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace RM_EM
{
    // Audio for the game.
    public class EM_GameAudio : MonoBehaviour
    {
        // Gets set to 'true' when post start is called.
        private bool calledPostStart = false;

        // The SFX source for UI elements.
        public AudioSource sfxSourceUI;

        // Automatically sets the UI audio to use the provided audio source.
        public bool autoSetUIAudioSources = true;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // The SFX audio source exists.
            if(sfxSourceUI != null && autoSetUIAudioSources)
            {
                // Finds all the objects.
                util.UIElementAudio[] uIElementAudios = FindObjectsOfType<util.UIElementAudio>(true);

                // Goes through all elements.
                foreach(util.UIElementAudio uIElementAudio in uIElementAudios)
                {
                    // Sets the SFX source UI if there's no audio source saved.
                    if(uIElementAudio.audioSource == null)
                        uIElementAudio.audioSource = sfxSourceUI;
                }
            }
        }

        // Called on the first frame update.
        protected virtual void PostStart()
        {
            calledPostStart = true;

            GameSettings.Instance.AdjustAllAudioLevels();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            // Calls post start.
            if (!calledPostStart)
                PostStart();
        }
    }
}