using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The world audio.
    public class WorldAudio : EM_GameAudio
    {
        [Header("WorldAudio")]

        // Manager
        public WorldManager manager;

        // The BGM audio source.
        public AudioSource bgmSource;

        // The SFX audio source.
        public AudioSource sfxSource;

        [Header("WorldAudio/Audio Clips/SFXs")]

        // The audio clip for buttons.
        public AudioClip buttonSfx;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            if (manager == null)
                manager = WorldManager.Instance;
        }

        // Plays the button sound effect.
        public void PlayButtonSfx()
        {
            sfxSource.PlayOneShot(buttonSfx);

        }
    }
}