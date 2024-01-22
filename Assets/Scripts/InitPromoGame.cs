using RM_EM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// The init script for the promo build of the game.
public class InitPromoGame : MonoBehaviour
{
    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // Unity Initialization
        Application.targetFrameRate = 30; // 30 FPS
        Application.runInBackground = false; // Don't run in the background.

        // Use the tutorial by default.
        GameSettings.Instance.UseTutorial = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Goes to the title scene.
        SceneManager.LoadScene("TitleScene");
    }
}
