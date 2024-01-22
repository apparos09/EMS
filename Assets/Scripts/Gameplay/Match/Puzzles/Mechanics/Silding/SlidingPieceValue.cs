using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RM_EM
{
    // A sliding piece.
    public class SlidingPieceValue : PuzzleValue
    {
        // The mechanic this sliding piece belongs to.
        public SlidingMechanic mechanic;

        // The segment the sliding piece is in (starts at 0).
        [Tooltip("The segment the piece is part of. It starts at (0) so that it also counts as the segment index.")]
        public int segment = -1;

        // The movement direction.
        public Vector3 moveDirec = new Vector3(0, -1, 0);

        // The move speed.
        public float moveSpeed = 1.0F;

        // Kills the piece.
        public void Kill()
        {
            // Returns the piece.
            mechanic.ReturnPiece(this);
        }

        // OnHit Function
        public override void OnHit(bool rightAnswer)
        {
            mechanic.ReturnPiece(this);

            // Plays a SFX
            if (manager.matchAudio != null)
                manager.matchAudio.PlayPuzzleValueSelectSfx();
        }



        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            // Moves the piece.
            transform.Translate(moveDirec * moveSpeed * Time.deltaTime);
            
            // Checks to see if the position is in bounds.
            if(!mechanic.PositionXYInBounds(transform.position))
            {
                // Not in bounds, so return the piece.
                Kill();
            }
        }
    }
}