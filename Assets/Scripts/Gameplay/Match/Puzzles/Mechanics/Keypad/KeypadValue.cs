using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The keypad value.
    public class KeypadValue : PuzzleValue
    {
        // OnHit
        public override void OnHit(bool rightAnswer)
        {
            // Plays a SFX
            if (manager.matchAudio != null)
                manager.matchAudio.PlayKeyboardClickSfx();
        }
    }
}