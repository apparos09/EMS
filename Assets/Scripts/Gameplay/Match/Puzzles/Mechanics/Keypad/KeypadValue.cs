using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The keypad value.
    public class KeypadValue : PuzzleValue
    {
        [Header("KeypadValue")]

        // The animator.
        public Animator animator;

        // The string for empty animation.
        public string emptyAnim = "Keypad Button - Empty Animation";

        // The string for the click animation.
        public string clickAnim = "Keypad Button - Click Animation";

        // If 'true', animations are used.
        private bool useAnimations = true;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // If the animator is not set, set it.
            if(animator == null)
                animator = GetComponent<Animator>();
        }

        // OnHit
        public override void OnHit(bool rightAnswer)
        {
            // Checks if animations are being used.
            if(useAnimations)
            {
                PlayKeypadClickAnimation();
            }
            else
            {
                PlayKeypadClickSfx();
            }
        }

        // Animation
        // Plays the empty animation (no animation).
        public void PlayKeypadEmptyAnimation()
        {
            animator.Play(emptyAnim);
        }

        // Plays the keypad click animation.
        public void PlayKeypadClickAnimation()
        {
            animator.Play(clickAnim);
        }

        // Start of the keypad click animation.
        public void OnKeypadClickAnimationStart()
        {
            // ...
        }

        // End of the keypad click animation.
        public void OnKeypadClickAnimationEnd()
        {
            PlayKeypadEmptyAnimation();
        }

        // Audio
        // Plays the keyboard click sound effect.
        public void PlayKeypadClickSfx()
        {
            if (manager.matchAudio != null)
                manager.matchAudio.PlayKeypadClickSfx();
        }
    }
}