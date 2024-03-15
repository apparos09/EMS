using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RM_EM
{
    // The save menu for the game.
    public class SaveMenu : MonoBehaviour
    {
        // The button for the save menu.
        public Button saveContinueButton;
        public Button saveQuitButton;
        public Button quitNoSaveButton;

        // Start is called before the first frame update
        void Start()
        {
            // If the application platform is equal to WebGL, disable the save functions.
            if(Application.platform == RuntimePlatform.WebGLPlayer)
            {
                // Disable the saving.
                saveContinueButton.interactable = false;
                saveQuitButton.interactable = false;
            }
            //else
            //{
            //    // Enable the saving.
            //    saveContinueButton.interactable = true;
            //    saveQuitButton.interactable = true;
            //}
        
        }

    }
}