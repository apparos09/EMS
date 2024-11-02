using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The results audio.
    public class ResultsAudio : EM_GameAudio
    {
        [Header("ResultsAudio")]

        // Manager
        public ResultsManager manager;

        // BGM
        public AudioSource bgmSource;

        // SFX
        public AudioSource sfxSource;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            if (manager == null)
                manager = ResultsManager.Instance;
        }

    }
}