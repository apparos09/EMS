using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // A reticle that's used to select values in th puzzle space.
    public class PlayerReticle : MonoBehaviour
    {
        // The sprite renderer.
        public SpriteRenderer spriteRenderer;

        // The player the reticle belongs to.
        public PlayerMatch playerMatch;

        // The reset position of the reticle.
        public Vector3 resetPos = Vector3.zero;

        // Autosets the reset pos.
        public bool autoSetResetPos = true;

        // Start is called before the first frame update
        void Start()
        {
            // Checks if the reset pos should be automatically set.
            if(autoSetResetPos)
                resetPos = transform.position;
        }

        // TriggerEnter
        private void OnTriggerEnter(Collider other)
        {
            playerMatch.OnReticleTriggerEnter(other);
        }

        // TriggerEnter2D
        private void OnTriggerEnter2D(Collider2D collision)
        {
            playerMatch.OnReticleTriggerEnter2D(collision);
        }

        // OnTriggerStay
        private void OnTriggerStay(Collider other)
        {
            playerMatch.OnReticleTriggerStay(other);
        }

        // OnTriggerStay2D
        private void OnTriggerStay2D(Collider2D collision)
        {
            playerMatch.OnReticleTriggerStay2D(collision);
        }

        // Resets the reticle's position.
        public void ResetPosition()
        {
            transform.position = resetPos;
        }

    }
}