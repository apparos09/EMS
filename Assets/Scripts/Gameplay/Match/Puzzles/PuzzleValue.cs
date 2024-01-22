using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The puzzle value script.
    public abstract class PuzzleValue : MonoBehaviour
    {
        // The match manager.
        public MatchManager manager;

        // The value saved to this puzzle entity.
        // 0-9, +, -, *, /
        public char value = '\0';

        // The sprite renderer.
        public SpriteRenderer valueRenderer;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // Sets the manager.
            if (manager == null)
                manager = MatchManager.Instance;
        }

        // Sets the sprite using the set value.
        public void SetSpriteByValue(PuzzleValueSprites sprites)
        {
            // Grabs the value from the provided script.
            valueRenderer.sprite = sprites.GetSpriteByValue(value);
        }

        // Sets the new value, and sprite using said value.
        public void SetValueAndSprite(char newValue, PuzzleValueSprites sprites)
        {
            value = newValue;
            SetSpriteByValue(sprites);
        }

        // Called when the puzzle value is hit.
        // The 'rightAnswer' argument shows if it was the right value or not.
        public abstract void OnHit(bool rightAnswer);

        // Update is called once per frame
        protected virtual void Update()
        {
            // ...
        }
    }
}