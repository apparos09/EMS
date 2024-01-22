using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The puzzle value prefabs, which is used to generate puzzle value objects.
    public class PuzzleValuePrefabs : MonoBehaviour
    {
        // The default puzzle value.
        public PuzzleValue valueDefault;

        // Numbers puzzle values.
        public PuzzleValue value0;
        public PuzzleValue value1;
        public PuzzleValue value2;
        public PuzzleValue value3;
        public PuzzleValue value4;
        public PuzzleValue value5;
        public PuzzleValue value6;
        public PuzzleValue value7;
        public PuzzleValue value8;
        public PuzzleValue value9;

        // Math operations puzzle values.
        public PuzzleValue valuePlus;
        public PuzzleValue valueMinus;
        public PuzzleValue valueMultiply;
        public PuzzleValue valueDivide;

        // Gets the prefab by a value.
        public PuzzleValue GetPrefabByValue(char value)
        {
            // The prefab to be instantiated.
            PuzzleValue valuePrefab;

            // Checks the value.
            switch (value)
            {
                case '0':
                    valuePrefab = value0;
                    break;

                case '1':
                    valuePrefab = value1;
                    break;

                case '2':
                    valuePrefab = value2;
                    break;

                case '3':
                    valuePrefab = value3;
                    break;

                case '4':
                    valuePrefab = value4;
                    break;

                case '5':
                    valuePrefab = value5;
                    break;

                case '6':
                    valuePrefab = value6;
                    break;

                case '7':
                    valuePrefab = value7;
                    break;

                case '8':
                    valuePrefab = value8;
                    break;

                case '9':
                    valuePrefab = value9;
                    break;

                case '+':
                    valuePrefab = valuePlus;
                    break;

                case '-':
                    valuePrefab = valueMinus;
                    break;

                case '*':
                    valuePrefab = valueMultiply;
                    break;

                case '/':
                    valuePrefab = valueDivide;
                    break;

                default:
                    valuePrefab = valueDefault;
                    break;
            }

            // Returns the value prefab.
            return valuePrefab;
        }


    }
}