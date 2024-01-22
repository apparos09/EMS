using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The audio for the title screen.
    public class TitleAudio : MonoBehaviour
    {
        // Manager
        public TitleManager manager;

        // The BGM audio source.
        public AudioSource bgmSource;

        // The sfx audio source.
        public AudioSource sfxSource;

        // Start is called before the first frame update
        void Start()
        {
            if (manager == null)
                manager = TitleManager.Instance;
        }

    }
}