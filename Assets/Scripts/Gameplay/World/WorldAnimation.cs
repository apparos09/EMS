using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The animation for the world scene.
    public class WorldAnimation : MonoBehaviour
    {
        // Manager
        public WorldManager manager;

        // The notification animation.
        public Animator notifyAnimator;

        // Start is called before the first frame update
        void Start()
        {
            if (manager == null)
                manager = WorldManager.Instance;
        }

        // Plays the power unlock animation.
        public void PlayPowerUnlockAnimation()
        {
            notifyAnimator.Play("Power Unlock Animation");
        }

    }
}