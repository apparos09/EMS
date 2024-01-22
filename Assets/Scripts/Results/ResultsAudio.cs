using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The results audio.
    public class ResultsAudio : MonoBehaviour
    {
        // Manager
        public ResultsManager manager;

        // BGM
        public AudioSource bgmSource;

        // SFX
        public AudioSource sfxSource;

        // Start is called before the first frame update
        void Start()
        {
            if (manager == null)
                manager = ResultsManager.Instance;
        }

    }
}