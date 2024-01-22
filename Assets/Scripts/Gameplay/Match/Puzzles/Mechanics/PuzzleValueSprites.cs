using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The puzzle value sprites, which are used to change the number shown on an image.
    public class PuzzleValueSprites : MonoBehaviour
    {
        // The default puzzle value.
        public Sprite spriteDefault;

        // Numbers sprites.
        public Sprite sprite0;
        public Sprite sprite1;
        public Sprite sprite2;
        public Sprite sprite3;
        public Sprite sprite4;
        public Sprite sprite5;
        public Sprite sprite6;
        public Sprite sprite7;
        public Sprite sprite8;
        public Sprite sprite9;

        // Math operations sprites.
        public Sprite spritePlus;
        public Sprite spriteMinus;
        public Sprite spriteMultiply;
        public Sprite spriteDivide;

        // Gets the sprite by value.
        public Sprite GetSpriteByValue(char value)
        {
            // The sprite to be returned.
            Sprite sprite;

            // Checks the sprite.
            switch (value)
            {
                case '0':
                    sprite = sprite0;
                    break;

                case '1':
                    sprite = sprite1;
                    break;

                case '2':
                    sprite = sprite2;
                    break;

                case '3':
                    sprite = sprite3;
                    break;

                case '4':
                    sprite = sprite4;
                    break;

                case '5':
                    sprite = sprite5;
                    break;

                case '6':
                    sprite = sprite6;
                    break;

                case '7':
                    sprite = sprite7;
                    break;

                case '8':
                    sprite = sprite8;
                    break;

                case '9':
                    sprite = sprite9;
                    break;

                case '+':
                    sprite = spritePlus;
                    break;

                case '-':
                    sprite = spriteMinus;
                    break;

                case '*':
                    sprite = spriteMultiply;
                    break;

                case '/':
                    sprite = spriteDivide;
                    break;

                default:
                    sprite = spriteDefault;
                    break;
            }

            // Returns the value prefab.
            return sprite;
        }


    }
}