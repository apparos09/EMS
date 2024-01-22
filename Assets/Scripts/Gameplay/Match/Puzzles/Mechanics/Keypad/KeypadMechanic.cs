using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The keyboard mechanic.
    public class KeypadMechanic : PuzzleMechanic
    {
        [Header("Keyboard")]
        // Numbers.
        public KeypadValue key0;
        public KeypadValue key1;
        public KeypadValue key2;
        public KeypadValue key3;
        public KeypadValue key4;
        public KeypadValue key5;
        public KeypadValue key6;
        public KeypadValue key7;
        public KeypadValue key8;
        public KeypadValue key9;        

        // Math operations.
        public KeypadValue keyPlus;
        public KeypadValue keyMinus;
        public KeypadValue keyMultiply;
        public KeypadValue keyDivide;


        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Set the puzzle type.
            puzzle.puzzle = Puzzle.puzzleType.keypad;
        }

        // Updates the mechanic.
        public override void UpdateMechanic()
        {
            // Nothing
        }

        // Resets the mechanic.
        public override void ResetMechanic()
        {
            // Nothing
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}