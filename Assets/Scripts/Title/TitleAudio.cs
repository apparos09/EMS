using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The audio for the title screen.
    public class TitleAudio : EM_GameAudio
    {
        [Header("TitleAudio")]

        // Manager
        public TitleManager manager;

        // The BGM audio source.
        public AudioSource bgmSource;

        // The sfx audio source.
        public AudioSource sfxSource;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            if (manager == null)
                manager = TitleManager.Instance;
        }

    }
}