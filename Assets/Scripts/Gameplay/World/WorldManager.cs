using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RM_EM
{
    // The manager for the world.
    public class WorldManager : GameplayManager
    {
        // The worldmanager singleton instance.
        private static WorldManager instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // Gets set to 'true', when post start has been called.
        private bool calledPostStart = false;

        [Header("World")]
        // The world UI.
        public WorldUI worldUI;

        // The world audio.
        public WorldAudio worldAudio;

        // The world animation.
        public WorldAnimation worldAnimation;

        // The camera the world uses.
        public WorldCamera worldCamera;

        // This isn't needed.
        // // The list for world events.
        // public List<GameEvent> worldEvents = new List<GameEvent>();

        // Used to pause the world.
        protected bool worldPaused = false;

        [Header("Player")]

        // The player in the game world.
        public PlayerWorld playerWorld;

        [Header("Areas, Challengers")]
        // The areas in the world.
        public List<Area> areas = new List<Area>();

        // THe current area index.
        public int currAreaIndex = 0;

        // A list of all challengers in the game.
        public List<ChallengerWorld> challengers;

        // The total number of challengers in the game.
        public const int CHALLENGER_COUNT = 9;

        // The first challenger for the game.
        public ChallengerWorld firstChallenger;

        // The final challenger of the game.
        public ChallengerWorld finalChallenger;

        // Constructor
        private WorldManager()
        {
            // ...
        }

        // Awake is called when the script is being loaded
        protected override void Awake()
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
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            // Calls the base function.
            base.Start();

            // If the challenger list isn't set, find all challengers.
            if(challengers.Count == 0)
            {
                // Gets the array.
                ChallengerWorld[] arr = FindObjectsOfType<ChallengerWorld>(true);
                challengers = new List<ChallengerWorld>(arr);
            }

            // Hide the challenger UI.
            worldUI.HideChallengeUI();

            // Sets the current area.
            SetArea(currAreaIndex, true);

            // The power menu can only be accessed if the player has powers.
            // TODO: once the game is done, you can take out these null checks since this should always be present.
            //if(worldUI.powerMenuUI != null)
            //{
            //    if(worldUI.powerMenuUI.menuButton != null)
            //    {
            //        worldUI.powerMenuUI.menuButton.interactable = playerWorld.HasPowers();
            //    }
            //}

            // StartTutorial(tutorial.GetTestPages());
        }

        // Gets the instance.
        public static WorldManager Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<WorldManager>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("WorldManager (singleton)");
                        instance = go.AddComponent<WorldManager>();
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

        // Post start function.
        private void PostStart()
        {
            // Check if the game is complete. Maybe load data here?

            //// This is being used for testing purposes. The game settings should be consulted for this value.
            //// Checks if the game settings have you use the tutorial.
            //bool useTutorial = true;

            // If the LOL Manager has been instantiated.
            if (LOLManager.Instantiated)
            {
                // Grabs the manager.
                LOLManager lolManager = LOLManager.Instance;

                // Checks for loaded data.
                if (lolManager.saveSystem.HasLoadedData())
                {
                    // Loads the data.
                    bool success = LoadGame();

                    // If the load was a success.
                    if(success)
                    {
                        // Checks for gameplay info being instantiated.
                        if(GameplayInfo.Instantiated)
                        {
                            GameplayInfo gameInfo = GameplayInfo.Instance;

                            // Says no data has been saved since new data has just been loaded.
                            gameInfo.worldDataSaved = false;
                            gameInfo.matchDataSaved = false;
                        }

                        // Since the load was a success, clear out the loaded data.
                        // This is to stop the data from being reloaded constantly.
                        lolManager.saveSystem.ClearLoadedData();
                    }
                }
            }

            // Checks if the info object has been instantiated to load content from it.
            if (GameplayInfo.Instantiated)
            {
                // Gets the instance.
                GameplayInfo gameInfo = GameplayInfo.Instance;

                // If there's saved world data, load it.
                if(gameInfo.worldDataSaved)
                {
                    // Load the world info.
                    gameInfo.LoadWorldInfo(this);

                    // Hide the world scene since you're immediately jumping to the results scene.
                    // This puts up a black screen, and turns off the BGM to hide the WorldScene.
                    if (IsFinalChallengerDefeated())
                    {
                        // Enable the black image.
                        worldUI.blackOverlay.gameObject.SetActive(true);

                        // Pause the BGM so that it doesn't play while waiting for the scene to switch.
                        if (worldAudio != null)
                            worldAudio.bgmSource.Pause();
                    }
                    else
                    {
                        // Make sure the black overlay is disabled.
                        worldUI.blackOverlay.gameObject.SetActive(false);
                    }
                        
                }

                // Autosaves if the player won the last round.
                if (gameInfo.pWinner == 1)
                {
                    // Saves the game.
                    SaveGame();

                    // Submits progress.
                    SubmitProgress();
                }
                    
            }


            // If the player has no powers, disable the power menu.
            if (worldUI.powersButton != null)
                worldUI.powersButton.interactable = playerWorld.powerList.Count != 0;


            // Sets the power icon to the player's power.
            worldUI.playerPowerIcon.SetPower(playerWorld.power);

            // If the game settings have been instantiated.
            if (worldUI.infoButton != null && GameSettings.Instantiated)
            {
                // If the tutorial is enabled, and the textbox isn't open.
                if (IsUsingTutorial() && IsTutorialAvailable())
                {
                    // The tutorial.
                    Tutorial tutorial = Tutorial.Instance;

                    // This is the first exponent tutorial. If it's been cleared, then there's something to show.
                    // If there isn't anything to show, then keep the button disabled.
                    worldUI.infoButton.interactable = tutorial.clearedExponent;


                    // Tutorial Triggers
                    // Opening not cleared, and the textbox isn't open.
                    if (!tutorial.clearedOpening)
                    {
                        // Gets the opening tutorial and opens it.
                        StartTutorial(tutorial.GetOpeningTutorial());
                    }
                    // Checks if the first match tutorial has happened yet.
                    // Also checks if the first challenger has been defeated.
                    else if(!tutorial.clearedFirstMatchWin && firstChallenger.IsChallengerDefeated())
                    {
                        // Gets the first match win tutorial and opens it.
                        StartTutorial(tutorial.GetFirstMatchWinTutorial());
                    }
                    // If the player hasn't gotten the first power tutorial, and they have powers.
                    else if (!tutorial.clearedFirstPower && playerWorld.HasPowers())
                    {
                        // Gets the first power tutorial and opens it.
                        StartTutorial(tutorial.GetFirstPowerTutorial());
                    }
                    // If the player hasn't gotten the final match tutorial.
                    else if(!tutorial.clearedFinalMatch && GetRemainingChallengersCount() == 1)
                    {
                        // Gets the final match tutorial and opens it.
                        StartTutorial(tutorial.GetFinalMatchTutorial());
                    }
                }
            }

            // Called post start.
            calledPostStart = true;
        }

        // SETTINGS //

        // PAUSING
        // Pauses the game, and the match overall.
        public override void SetPausedGame(bool paused)
        {
            base.SetPausedGame(paused);
            SetPausedWorld(paused);
        }

        // Pauses the match, and only the match.
        public void SetPausedWorld(bool paused)
        {
            // Set paused.
            worldPaused = paused;

            // Checks if paused or not.
            if (paused)
            {
                // ...
            }
            else
            {
                // ...
            }


        }

        // Checks if the world is paused.
        public bool WorldPaused
        {
            get
            {
                return worldPaused;
            }
        }

        // Pauses the world.
        public void PauseWorld()
        {
            SetPausedWorld(true);
        }

        // Unpauses the world.
        public void UnpauseWorld()
        {
            SetPausedWorld(false);
        }

        // Toggles the world being paused.
        public void TogglePausedWorld()
        {
            SetPausedWorld(!worldPaused);
        }


        // TUTORIAL //
        // Called when a tutorial is started.
        public override void OnTutorialStart()
        {
            base.OnTutorialStart();

            PauseWorld();
        }

        // Called when a tutorial is ended.
        public override void OnTutorialEnd()
        {
            base.OnTutorialEnd();

            UnpauseWorld();
        }


        // AREAS //
        // Sets the area.
        // TODO: implement skip transition for areas.
        public void SetArea(int newIndex, bool skipAnimation)
        {
            // Bounds check to see if the new index is valid.
            if(newIndex >= 0 && newIndex < areas.Count)
            {
                currAreaIndex = newIndex;
            }
            else
            {
                return;
            }

            // Grabs the new area.
            Area newArea = areas[currAreaIndex];

            // Transition the camera.
            if(newArea.cameraPos != null)
            {
                // Moves the camera.
                // If the animation should be skipped, the change is instant.
                worldCamera.Move(newArea.cameraPos, skipAnimation);

            }


            // Enable the buttons by default (will be disabled if unsable in event update).
            worldUI.prevAreaButton.interactable = true;
            worldUI.nextAreaButton.interactable = true;

            // If the area index is set to 0, disable the back button.
            if (currAreaIndex == 0)
            {
                worldUI.prevAreaButton.interactable = false;
            }

            // If the area index is at its max, disable the forward button.
            if(currAreaIndex == areas.Count - 1)
            {
                worldUI.nextAreaButton.interactable = false;
            }

            // If the next button is interactable, check if the area has been cleared.
            if(worldUI.nextAreaButton.interactable)
            {
                // TODO: for some reason, this isn't working when loadng save data.

                // If the area event is set.
                if(newArea.areaEvent != null)
                {
                    // If the area has not been cleared, disable the next button.
                    if(!newArea.areaEvent.cleared)
                    {
                        worldUI.nextAreaButton.interactable = false;

                    }
                }
            }

        }

        // Go to the next area.
        public void NextArea()
        {
            // Reduce index.
            int index = currAreaIndex + 1;

            // Bounds check.
            if(index >= areas.Count)
                index = 0;

            // Set the area index.
            SetArea(index, false);
        }
        
        // Go to the previous area.
        public void PreviousArea()
        {
            // Reduce index.
            int index = currAreaIndex - 1;

            // Bounds check.
            if (index < 0)
                index = areas.Count - 1;

            // Set the area index.
            SetArea(index, false);
        }

        // Checks if the player is in the final area.
        public bool InFinalArea()
        {
            return currAreaIndex == areas.Count - 1;
        }

        // Updates the area events.
        public void UpdateAreaEvents()
        {
            // Updates all area events.
            foreach (Area area in areas)
            {
                // Updates the events with the new changes.
                if (area.areaEvent != null)
                    area.areaEvent.UpdateEvent();
            }
        }


        // CHALLENGE //
        // Returns the index of the challenger.
        public int GetChallengerIndex(ChallengerWorld challenger)
        {
            // Sees if the challenger's in the list.
            if(challengers.Contains(challenger))
            {
                return challengers.IndexOf(challenger);
            }
            else
            {
                return -1;
            }
        }

        // Is this the first challenger?
        public bool IsFirstChallenger(ChallengerWorld challenger)
        {
            return challenger == firstChallenger;
        }

        // Is this the final challenger?
        public bool IsFinalChallenger(ChallengerWorld challenger)
        {
            return challenger == finalChallenger;
        }

        // Checks if the final challenger has been defeated.
        public bool IsFinalChallengerDefeated()
        {
            // Final challenger check.
            if(finalChallenger != null)
            {
                return finalChallenger.IsChallengerDefeated();
            }
            else
            {
                return false;
            }
        }

        // Gets the remaining challenger count.
        public int GetRemainingChallengersCount()
        {
            // The sum.
            int sum = 0;

            // Goes through all challengers.
            foreach(ChallengerWorld chal in challengers)
            {
                // If not defeated add to the sum.
                if (!chal.IsChallengerDefeated())
                    sum++;
            }

            // Return result.
            return sum;

        }

        // Shows the challenger UI.
        public void ShowChallengerUI(ChallengerWorld challenger, int index)
        {
            worldUI.ShowChallengerUI(challenger, index);
        }
        
        // Accept the challenge.
        public void AcceptChallenge(ChallengerWorld challenger)
        {
            // Sets the provided challenger.
            worldUI.challengeUI.challenger = challenger;

            // Goes to the match scene.
            ToMatchScene();
        }

        // Accept the challenge.
        public void AcceptChallenge()
        {
            AcceptChallenge(worldUI.challengeUI.challenger);
        }

        // Decline the challenge.
        public void DeclineChallenge()
        {
            worldUI.HideChallengeUI();
        }



        // MATCH //
        // Returns the match number, which is determined based on...
        // How many challengers are left.
        public int GetMatchNumber()
        {
            // The defeated count.
            int defeatedCount = 0;

            // Goes through every challenger.
            foreach(ChallengerWorld challenger in challengers)
            {
                // If the challenger has been defeated, increase the count.
                if (challenger.IsChallengerDefeated())
                    defeatedCount++;
            }

            // Adds +1 so that it works as the match count.
            return defeatedCount + 1;
        }

        // SAVING/LOADING
        // Generates the save data or the game.
        public EM_GameData GenerateSaveData()
        {
            // The data.
            EM_GameData data = new EM_GameData();

            // Save tutorial setting.
            data.useTutorial = IsUsingTutorial();


            // Saves the game time.
            data.gameTime = gameTime;

            // Saves the game score (this should be the same as what's in GameplayInfo)
            data.gameScore = gameScore;

            // If the game info is instantited.
            if(GameplayInfo.Instantiated)
            {
                GameplayInfo gameInfo = GameplayInfo.Instance;

                // Wrong Answers
                data.wrongAnswers = gameInfo.wrongAnswers;

                // Losses
                data.losses = gameInfo.p1Losses;
                data.recentLosses = gameInfo.p1RecentLosses;
            }
            else
            {
                data.wrongAnswers = 0;
                data.losses = 0;
                data.recentLosses = 0;
            }


            // Save the current area index.
            data.currAreaIndex = currAreaIndex;

            // Saves the player's current power.
            data.playerPower = playerWorld.power;

            // Stores the player's power information.
            for (int i = 0; i < data.playerPowerList.Length && i < playerWorld.powerList.Count; i++)
            {
                data.playerPowerList[i] = playerWorld.powerList[i];
            }

            // Stores information on the challengers that have been defeated.
            for (int i = 0; i < data.challengersDefeated.Length && i < challengers.Count; i++)
            {
                // Saves if the challenger has been defeated.
                // NOTE: you don't check challenger available, but I don't think you need to.
                data.challengersDefeated[i] = challengers[i].IsChallengerDefeated();
            }

            // Generates the tutorial data.
            data.tutorialData = Tutorial.Instance.GenerateTutorialData();

            // Checks if the game is completed.
            data.complete = IsGameComplete();

            // The data is valid.
            data.valid = true;

            // Returns 'true'.
            return data;
        }

        // Saves the data for the game.
        public bool SaveGame()
        {
            // If the LOL Manager does not exist, return false.
            if (!LOLManager.Instantiated)
            {
                Debug.LogError("The LOL Manager does not exist.");
                return false;
            }

            // Gets the save system.
            SaveSystem saveSys = LOLManager.Instance.saveSystem;

            // Checks if the save system exists.
            if (saveSys == null)
            {
                Debug.LogError("The save system could not be found.");
                return false;
            }
            

            // Set the world manager.
            if(saveSys.worldManager == null)
                saveSys.worldManager = this;

            // Saves the game.
            bool result = saveSys.SaveGame();

            // Return result.
            return result;
        }

        // Loads data, and return a 'bool' to show it was successful.
        public bool LoadGame()
        {
            // If the LOL Manager does not exist, return false.
            if(!LOLManager.Instantiated)
            {
                Debug.LogError("The LOL Manager does not exist.");
                return false;
            }

            // Gets the save system.
            SaveSystem saveSys = LOLManager.Instance.saveSystem;

            // Checks if the save system exists.
            if(saveSys == null)
            {
                Debug.LogError("The save system could not be found.");
                return false;
            }


            // Gets the loaded data.
            EM_GameData loadedData = saveSys.loadedData;

            // No data to load.
            if (loadedData == null)
            {
                Debug.LogError("The save data does not exist.");
                return false;
            }

            // Data invalid.
            if (loadedData.valid == false)
            {
                Debug.LogError("The save data is invalid.");
                return false;
            }

            // Game complete
            if(loadedData.complete)
            {
                // Changed from assertion to normal log.
                // Debug.LogAssertion("The game was completed, so the data hasn't been loaded.");
                Debug.Log("The game was completed, so the data hasn't been loaded.");
                return false;
            }


            // LOADING THE DATA

            // Tutorial settings.
            SetUsingTutorial(loadedData.useTutorial);

            // Loads the game time.
            gameTime = loadedData.gameTime;

            // Loads the game score
            gameScore = loadedData.gameScore;

            // If the game info is instantited.
            if (GameplayInfo.Instantiated)
            {
                GameplayInfo gameInfo = GameplayInfo.Instance;

                // Wrong Answers
                gameInfo.wrongAnswers = loadedData.wrongAnswers;

                // Load Losses
                gameInfo.p1Losses = loadedData.losses;
                gameInfo.p1RecentLosses = loadedData.recentLosses;
            }

            // NOTE: moved set area to the end.

            // Clears the power list.
            playerWorld.powerList.Clear();

            // Gives the player their powers.
            for (int i = 0; i < loadedData.playerPowerList.Length; i++)
            {
                // Gives the player the power.
                playerWorld.GivePower(loadedData.playerPowerList[i], false);
            }

            // Sets the player's current power.
            playerWorld.SetPower(loadedData.playerPower);

            // Sets if challengers have been defeated or not.
            for (int i = 0; i < loadedData.challengersDefeated.Length && i < challengers.Count; i++)
            {
                // Sets if the challenger has been defeated.
                challengers[i].SetChallengerDefeated(loadedData.challengersDefeated[i]);
            }

            // Load the tutorial data.
            Tutorial.Instance.LoadTutorialData(loadedData.tutorialData);

            // Updates the area events.
            // UpdateAreaEvents();

            // Save the current area index.
            SetArea(loadedData.currAreaIndex, true);

            // The data has been loaded successfully.
            return true;
        }


        // GAME PROGRESS

        // Check if the game is complete.
        public bool IsGameComplete()
        {
            return finalChallenger.IsChallengerDefeated();
        }

        // Gets the game progress.
        public int GetGameProgress()
        {
            // Progress
            int progress = 0;

            // Increases progress for every defeated challenger.
            for(int i = 0; i < challengers.Count; i++)
            {
                if (challengers[i].IsChallengerDefeated())
                    progress++;
            }

            // Returns the progress.
            return progress;
        }

        // Submits the current game progress.
        public void SubmitProgress()
        {
            // If the LOLManager is instantiated.
            if (LOLManager.Instantiated)
                LOLManager.Instance.SubmitProgress(gameScore, GetGameProgress());
        }

        // Submits the game progress complete.
        public void SubmitProgressComplete()
        {
            if (LOLManager.Instantiated)
                LOLManager.Instance.SubmitProgressComplete(gameScore);
        }



        // SCENES //
        // Goes to the match scene. Call AcceptChallenge() if a match info object should be created.
        public void ToMatchScene()
        {
            // Gets the game info.
            GameplayInfo gameInfo = GameplayInfo.Instance;

            // Save the world and match info to the game info instance.
            gameInfo.SaveWorldInfo(this);
            gameInfo.SaveMatchInfo(this);

            // Makes sure the game is unpaused.
            UnpauseGame();

            // TODO: add loading screen.
            SceneManager.LoadScene("MatchScene");
        }


        // GAME COMPLETE
        // Called when the game is completed.
        public override void OnGameComplete()
        {
            // Creates the results data.
            GameObject temp = new GameObject("Results Data");
            ResultsData data = temp.AddComponent<ResultsData>();
            DontDestroyOnLoad(temp);

            // Saves the game.
            SaveGame();

            // Results Values
            // Game Time
            data.gameTime = gameTime;

            // Game Score
            data.gameScore = gameScore;

            // Checks if the gameplay info has been instantiated.
            if(GameplayInfo.Instantiated)
            {
                // Grabs the instance.
                GameplayInfo gameInfo = GameplayInfo.Instance;

                // Sets the wrong answers.
                data.wrongAnswers = gameInfo.wrongAnswers;

                // Add to the P1 losses.
                data.losses = gameInfo.p1Losses;
            }

            // Submit progress complete.
            SubmitProgressComplete();

            // Goes to the scene.
            base.OnGameComplete();
        }


        // Update is called once per frame
        protected override void Update()
        {
            // Calls post start if it hasn't been called yet.
            if (!calledPostStart)
                PostStart();

            // Calls the base update function.
            base.Update();
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