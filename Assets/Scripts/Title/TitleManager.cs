using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using util;

namespace RM_EM
{
    // The title manager.
    public class TitleManager : MonoBehaviour
    {
        // The singleton instance.
        private static TitleManager instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // The title screen UI.
        public TitleUI titleUI;

        // The title audio for the game.
        public TitleAudio titleAudio;

        // The audio credits for the game.
        public AudioCredits audioCredits;

        // Constructor
        private TitleManager()
        {
            // ...
        }

        // Awake is called when the script is being loaded
        protected virtual void Awake()
        {
            // If the instance hasn't been set, set it to this object.
            if (instance == null)
            {
                instance = this;
            }
            // If the instance isn't this, destroy the game object.
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // Run code for initialization.
            if (!instanced)
            {
                instanced = true;

                // Checks if LOL SDK has been initialized.
                GameSettings settings = GameSettings.Instance;

                // Checks for initialization
                if (false)
                {
                    // NOTE: the buttons disappear for a frame if there is no save state.
                    // It doesn't effect anything, but it's jarring visually.
                    // As such, the Update loop keeps them both on.


                    //// Set up the game initializations.
                    //if (titleUI.newGameButton != null && titleUI.continueButton != null)
                    //    lolManager.saveSystem.Initialize(titleUI.newGameButton, titleUI.continueButton);

                    // Don't disable the continue button, otherwise the save data can't be loaded.
                    // Enables/disables the continue button based on if there is loaded data or not.
                    // continueButton.interactable = lolManager.saveSystem.HasLoadedData();
                    // Continue button is left alone.

                    // Since the player can't change the tutorial settings anyway when loaded from InitScene...
                    // These are turned off just as a safety precaution. 
                    // This isn't needed since the tutorial is activated by default if going from InitScene...
                    // And can't be turned off.
                    // overrideTutorial = true;
                    // continueTutorial = true;

                    // LOLSDK.Instance.SubmitProgress();
                }
                else
                {
                    // Debug.LogError("LOL SDK NOT INITIALIZED.");

                    // Chcks if the system manager has been instantiated.
                    if(SystemManager.Instantiated)
                    {
                        // Gets an instance of the LOL manager.
                        SystemManager sysManager = SystemManager.Instance;

                        // Loads the game.
                        sysManager.saveSystem.LoadGame();

                        // Changes continue button based on if there's loaded data or not.
                        // You can save and go back to the menu, so the continue button is usable under that circumstance.
                        if (sysManager.saveSystem.HasLoadedData()) // Game has loaded data.
                        {
                            // TODO: manage tutorial content.
                            titleUI.continueButton.interactable = true;
                        }
                        else // No loaded data.
                        {
                            // TODO: manage tutorial content.
                            titleUI.continueButton.interactable = false;
                        }
                    }

                    

                    // TODO: do you need this?
                    // // Have the button be turned on no matter what for testing purposes.
                    // titleUI.continueButton.interactable = true;

                    // Adjust the audio settings since the InitScene was not used.
                    settings.AdjustAllAudioLevels();
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // AUDIO CREDITS
            AudioCredits.AudioCredit credit;

            // TODO: remove direct links for audio components since they're super long.

            // Sound effects won't be credited unless they have to be, since they don't have proper names.

            // BGM //
            // Title
            credit = new AudioCredits.AudioCredit();
            credit.title = "Night in the Castle";
            credit.artist = "Kevin Macleod";
            credit.collection = "FreePD/Comedy";
            credit.source = "FreePD";
            credit.link1 = "https://freepd.com/comedy.php";
            credit.link2 = "-";
            credit.copyright = 
                "\"Night in the Castle\"\n" +
                "Kevin Macleod (incompetech.com)\n" +
                "Licensed under Creative Commons Zero: 1.0 Universal\n" + 
                "https://creativecommons.org/publicdomain/zero/1.0/deed.en";

            audioCredits.audioCredits.Add(credit);

            // World
            // Area
            credit = new AudioCredits.AudioCredit();
            credit.title = "Abracadabra";
            credit.artist = "Antti Luode";
            credit.collection = "Anttis Instrumentals";
            credit.source = "GameSounds.xyz";
            credit.link1 = "https://gamesounds.xyz/?dir=Anttis%20instrumentals/Songs";
            credit.link2 = "https://gamesounds.xyz/Anttis%20instrumentals/Songs/Anttis%20instrumentals%20-%20Abracadabra.mp3";
            credit.copyright =
                "\"Abracadabra\"\n" +
                "Antti Luode\n" +
                "Licensed under Creative Commons: By Attribution 3.0\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            audioCredits.audioCredits.Add(credit);

            // Match
            // Match - Normal
            credit = new AudioCredits.AudioCredit();
            credit.title = "Monsters in Hotel";
            credit.artist = "Rafael Krux";
            credit.collection = "FreePD/Comedy";
            credit.source = "FreePD";
            credit.link1 = "https://freepd.com/comedy.php";
            credit.link2 = "https://music.orchestralis.net/track/28566249";
            credit.copyright =
                "'Monsters in Hotel' by Rafael Krux (orchestralis.net)\n" +
                "Creative Commons 4.0 License.\n" +
                "https://creativecommons.org/licenses/by/4.0/";

            audioCredits.audioCredits.Add(credit);


            // For size reasons, the final boss track has been removed.
            // As such, the credit has also been removed.
            // Match - Boss
            credit = new AudioCredits.AudioCredit();
            credit.title = "Ghost Town";
            credit.artist = "Rafael Krux";
            credit.collection = "FreePD/Comedy";
            credit.source = "FreePD";
            credit.link1 = "https://freepd.com/comedy.php";
            credit.link2 = "https://music.orchestralis.net/track/28566240";
            credit.copyright =
                "'Ghost Town' by Rafael Krux (orchestralis.net)\n" +
                "Creative Commons 4.0 License.\n" +
                "https://creativecommons.org/licenses/by/4.0/";

            audioCredits.audioCredits.Add(credit);


            // Match - Results
            credit = new AudioCredits.AudioCredit();
            credit.title = "Funky Energy Loop";
            credit.artist = "Kevin Macleod";
            credit.collection = "FreePD/Scoring";
            credit.source = "FreePD";
            credit.link1 = "https://freepd.com/scoring.php";
            credit.link2 = "-";
            credit.copyright =
                "\"Funky Energy Loop\"\n" +
                "Kevin Macleod (incompetech.com)\n" +
                "Licensed under Creative Commons Zero: 1.0 Universal\n" +
                "https://creativecommons.org/publicdomain/zero/1.0/deed.en";

            audioCredits.audioCredits.Add(credit);

            // Game Results
            credit = new AudioCredits.AudioCredit();
            credit.title = "Beach Walk";
            credit.artist = "Antti Luode";
            credit.collection = "Anttis Instrumentals";
            credit.source = "GameSounds.xyz";
            credit.link1 = "https://gamesounds.xyz/?dir=Anttis%20instrumentals/Songs";
            credit.link2 = "https://gamesounds.xyz/Anttis%20instrumentals/Songs/Anttis%20instrumentals%20-%20Beach%20walk.mp3";
            credit.copyright =
                "\"Beach Walk\"\n" +
                "Antti Luode\n" +
                "Licensed under Creative Commons: By Attribution 3.0\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            audioCredits.audioCredits.Add(credit);



            // SOUND EFFECTS

            // PUZZLE VALUE SELECT (SFX)
            credit = new AudioCredits.AudioCredit();
            credit.title = "Blop";
            credit.artist = "Mark DiAngelo";
            credit.collection = "-";
            credit.source = "SoundBible";
            credit.link1 = "https://soundbible.com/2067-Blop.html";
            credit.link2 = "-";
            credit.copyright =
                "\"Blop\"\n" +
                "Mark DiAngelo\n" +
                "Licensed under Creative Commons: By Attribution 3.0\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            audioCredits.audioCredits.Add(credit);

            // POWER USE (SFX) [Formally Power Equip]
            credit = new AudioCredits.AudioCredit();
            credit.title = "Text Message Alert 5";
            credit.artist = "Daniel Simon";
            credit.collection = "-";
            credit.source = "SoundBible";
            credit.link1 = "https://soundbible.com/2158-Text-Message-Alert-5.html";
            credit.link2 = "-";
            credit.copyright =
                "\"Text Message Alert 5\"\n" +
                "Daniel Simon\n" +
                "Licensed under Creative Commons: By Attribution 3.0\n" +
                "https://creativecommons.org/licenses/by/3.0/";

            audioCredits.audioCredits.Add(credit);


        }

        // Gets the instance.
        public static TitleManager Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<TitleManager>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("Title Manager (singleton)");
                        instance = go.AddComponent<TitleManager>();
                    }

                }

                // Return the instance.
                return instance;
            }
        }

        // Returns 'true' if the object has been initialized.
        public static bool Instantiated
        {
            get
            {
                return instanced;
            }
        }

        // Starts the game (general function for moving to the GameScene).
        public void StartGame()
        {
            // TODO: add transition.
            SceneManager.LoadScene("WorldScene");
        }

        // Starts a new game.
        public void StartNewGame()
        {
            // Clear out the loaded data and last save if the LOLSDK has been initialized.
            if(SystemManager.Instantiated)
                SystemManager.Instance.saveSystem.ClearLoadedAndLastSaveData(false);

            // Start the game.
            StartGame();
        }

        // Continues a saved game.
        public void ContinueGame()
        {
            // New
            // NOTE: a callback is setup onclick to load the save data.
            // Since that might happen after this function is processed...
            // Loaded data does not need to be checked for at this stage.


            // TODO: load data.
            // // If the system manager has been instantiated.
            // if(SystemManager.Instantiated)
            // {
            //     if(SystemManager.Instance.saveSystem.HasLoadedData())
            //         SystemManager.Instance.saveSystem.Load
            // }

            //// If the user's tutorial settings should be overwritten, do so.
            //if (overrideTutorial)
            //    GameSettings.Instance.UseTutorial = continueTutorial;

            // Starts the game.
            StartGame();
        }


        // Clears out the save.
        // TODO: This is only for testing, and the button for this should not be shown in the final game.
        public void ClearSave()
        {
            SystemManager.Instance.saveSystem.lastSave = null;
            SystemManager.Instance.saveSystem.loadedData = null;

            titleUI.continueButton.interactable = false;
        }

        // Quits the game (will not be used in LOL version).
        public void QuitGame()
        {
            Application.Quit();
        }

        // Update is called once per frame
        void Update()
        {
            // ...
        }

        // This function is called when the MonoBehaviour will be destroyed.
        private void OnDestroy()
        {
            // If the saved instance is being deleted, set 'instanced' to false.
            if (instance == this)
            {
                instanced = false;
            }
        }
    }
}