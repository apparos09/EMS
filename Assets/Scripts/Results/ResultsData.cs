using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The results data.
    public class ResultsData : MonoBehaviour
    {
        // The game time.
        public float gameTime = 0;

        // The game score.
        public int gameScore = 0;

        // The number of wrong answers.
        public int wrongAnswers = 0;

        // The number of losses the player had.
        public int losses = 0;
    }
}