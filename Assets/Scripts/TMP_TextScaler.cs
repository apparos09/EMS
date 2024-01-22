using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RM_EM
{
    // Dynamically scales TMP_Text if it overflows (only works with wrapmode is disabled).
    public class TMP_TextScaler : MonoBehaviour
    {
        // The text.
        public TMP_Text text;

        // Width
        public float textWidth = 1;

        //// Height
        //public float textHeight = 1;

        [Header("Settings")]
        // Adjust the width.
        public bool adjustWidth = true;

        // // Adjust the height.
        // public bool adjustHeight = true;

        // Start is called before the first frame update
        void Start()
        {
            // Word wrapping should be disabled.
            text.enableWordWrapping = false;
        }

        // Scales the text (TODO: implement)
        public void ScaleText()
        {
            // If the text is empty.
            if (text.text == "")
                return;

            // SPACE
            float areaLeft = text.transform.position.x - (textWidth / 2);
            float areaRight = text.transform.position.x + (textWidth / 2);

            //float areaTop = text.transform.position.y + (textHeight / 2);
            //float areaBottom = text.transform.position.y - (textHeight / 2);

            // TEXT
            // Gets the text info of the text object.
            TMP_TextInfo tmpTextInfo = text.GetTextInfo(text.text);

            // Gets the first and last char info.
            TMP_CharacterInfo tmpCharInfoFirst = tmpTextInfo.characterInfo[0];
            TMP_CharacterInfo tmpCharInfoLast = tmpTextInfo.characterInfo[text.text.Length - 1];

            // Scale X
            if(tmpCharInfoFirst.topLeft.x < areaLeft || tmpCharInfoLast.topRight.x > areaRight)
            {
                // TODO: scale
            }
            else
            {
                text.transform.localScale = Vector3.one;
            }

            //// Scale Y
            //if (tmpCharInfoFirst.topLeft.y > areaTop || tmpCharInfoLast.bottomLeft.x < areaBottom)
            //{
            //    // TODO: scale
            //}


        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}