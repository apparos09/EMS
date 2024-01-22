using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace RM_EM
{
    // The manager for matches.
    public class MatchManager : GameplayManager
    {
        // The matchamanger instance.
        private static MatchManager instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        // Gets set to 'true', when post start has been called.
        private bool calledPostStart = false;

        [Header("Match")]

        // The UI for the match.
        public MatchUI matchUI;

        // The match audio.
        public MatchAudio matchAudio;

        // The match number.
        public int matchNumber = 0;

        // The point goal for the game.
        public int pointGoal = 999;

        // If 'true', the point goal is used.
        public bool usePointGoal = true;

        // The current time for the match.
        public float matchTime = 0;

        // The winner of the match.
        public int matchWinner = -1;

        // Checks if the match is paused.
        protected bool matchPaused = false;

        [Header("Match/Backgrounds")]

        // The background for the match.
        public SpriteRenderer background;

        // The match background list.
        public List<Sprite> backgrounds;

        // PLAYERS
        [Header("Match/Player 1")]
        // P1
        public PlayerMatch p1;
        
        // P1 Puzzle
        public Puzzle p1Puzzle;

        // P1 Game Score
        public int p1Score = 0;

        // The wrong answers from p1.
        public int p1WrongAnswers = 0;

        [Header("Match/Player 2")]
        // P2
        public PlayerMatch p2;
        
        // P2 Puzzle
        public Puzzle p2Puzzle;

        [Header("Match/Other")]

        // Uses the match start animation if set to true.
        public bool useMatchStartAnim = true;

        // Constructor
        private MatchManager()
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
            // Checks if game info has been initialized already.
            bool gameInfoInit = GameplayInfo.Instantiated;

            // Calls base start.
            base.Start();

            // Checks if the info has been instantiated.
            if(gameInfoInit && GameplayInfo.Instantiated)
            {
                // Gets the instance.
                GameplayInfo gameInfo = GameplayInfo.Instance;

                // Load the match info.
                gameInfo.LoadMatchInfo(this);
            }
        }

        // Gets the instance.
        public static MatchManager Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<MatchManager>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("MatchManager (singleton)");
                        instance = go.AddComponent<MatchManager>();
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
            // Generates the equations.
            p1Puzzle.GenerateEquation();
            p2Puzzle.GenerateEquation();

            // Updates all the player displays.
            matchUI.UpdateAllPlayerUI();

            // Called when the equations have been generated.
            p1.OnEquationGenerated();
            p2.OnEquationGenerated();


            // If the start animation should be played.
            if (useMatchStartAnim && matchUI.matchStart != null)
            {
                // Plays the start animation.
                matchUI.matchStart.PlayStartAnimation();
            }


            //// TUTORIAL
            //bool useTutorial = true;
            //// if(GameSettings.Instance.UseTutorial)
            //if (useTutorial)
            //{
            //    Tutorial tutorial = Tutorial.Instance;

            //    // First match not cleared, and the textbox isn't open.
            //    if (!tutorial.clearedFirstMatch && !matchUI.tutorialTextBox.IsVisible())
            //    {
            //        // Gets the opening tutorial.
            //        StartTutorial(tutorial.GetFirstMatchTutorial());
            //        // Input.
            //    }
            //}

            // Post start has been called.
            calledPostStart = true;
        }


        // SETTINGS //

        // PAUSING
        // Pauses the game, and the match overall.
        public override void SetPausedGame(bool paused)
        {
            base.SetPausedGame(paused);
            SetPausedMatch(paused);
        }

        // Pauses the match, and only the match.
        public void SetPausedMatch(bool paused)
        {
            // Set paused.
            matchPaused = paused;

            // Checks if paused or not.
            if(paused)
            {
                // Disable the players.
                p1.enabled = false;
                p2.enabled = false;

                // Set time scale to 0.
                Time.timeScale = 0;

                // Disable the mouse touch.
                mouseTouch.gameObject.SetActive(false);
            }
            else
            {
                // Set not paused.
                matchPaused = false;

                // Enable the players.
                p1.enabled = true;
                p2.enabled = true;

                // Set time scale to 1.
                Time.timeScale = 1;

                // Enable the mouse touch.
                mouseTouch.gameObject.SetActive(true);
            }

            
        }

        // Checks if the match is paused.
        public bool MatchPaused
        {
            get
            {
                return matchPaused;
            }
        }

        // Pauses the match.
        public void PauseMatch()
        {
            SetPausedMatch(true);
        }

        // Unpauses the match.
        public void UnpauseMatch()
        {
            SetPausedMatch(false);
        }

        // Toggles the match being paused.
        public void TogglePausedMatch()
        {
            SetPausedMatch(!matchPaused);
        }

        // Sets the background by the number.
        public void SetBackground(int bgIndex)
        {
            // No background set, so don't do anything.
            if(background == null)
            {
                return;
            }

            // The index is out of bounds.
            if(bgIndex >= backgrounds.Count || bgIndex < 0)
            {
                return;
            }

            // Set the background.
            background.sprite = backgrounds[bgIndex];
        }


        // TUTORIAL //
        // Called when a tutorial is started.
        public override void OnTutorialStart()
        {
            base.OnTutorialStart();

            // Pause match.
            PauseMatch();
        }

        // Called when a tutorial is ended.
        public override void OnTutorialEnd()
        {
            base.OnTutorialEnd();

            // If there's no window open, and the match isn't over, unpause the match.
            if(!matchUI.IsWindowOpen() && !IsMatchOver())
                UnpauseMatch();
        }



        // MECHANICS //

        // POWER
        // Called when a power is used.
        public void OnPowerStarted(Power power)
        {
            // ...
            // power.playerMatch.puzzle.ApplyPower(power);

            // Checks which player the puzzle belongs to.
            if (power.playerMatch == p1) // P1
            {
                // Nothing
            }
            else if (power.playerMatch == p2) // P2
            {
                // Nothing
            }
        }

        // Called when a power is active.
        public void OnPowerActive(Power power)
        {
            // Checks which player the puzzle belongs to.
            if (power.playerMatch == p1) // P1
            {
                matchUI.UpdatePlayer1PowerBarFill();
            }
            else if (power.playerMatch == p2) // P2
            {
                matchUI.UpdatePlayer2PowerBarFill();
            }
        }

        // Called when a power is finished.
        public void OnPowerFinished(Power power)
        {
            // Checks which player the puzzle belongs to.
            if (power.playerMatch == p1) // P1
            {
                matchUI.UpdatePlayer1PowerBarFill();
            }
            else if (power.playerMatch == p2) // P2
            {
                matchUI.UpdatePlayer2PowerBarFill();
            }
        }


        // SKIPPING
        // Called when an equation is skipped.
        public void OnEquationSkipped(Puzzle puzzle)
        {
            // Checks which player the puzzle belongs to.
            if (puzzle.playerMatch == p1) // P1
            {
                matchUI.UpdatePlayer1EquationDisplay();
            }
            else if(puzzle.playerMatch == p2) // P2
            {
                matchUI.UpdatePlayer2EquationDisplay();
            }
        }

        // Called when the equation has been answered.
        // player: the player that answered the question.
        // equationFilled: the filled in equation.
        // rulesUsed: the number of rules used for the equation.
        // missingValuesCountStart: the number of blanks to fill in at the start.
        // answerTime: the time it took to answer the question.
        public void OnEquationComplete(Puzzle puzzle, PlayerMatch player, string equationFilled, 
            List<exponentRule> rulesUsed, int missingValuesCountStart, float answerTime)
        {
            // CALCULATING PLAYER POINTS //
            // Base points gained.
            int points = 20; // 10

            // Increase poitns by number of rules used.
            points += 7 * rulesUsed.Count;

            // Add points based on the number of missing values.
            points += 12 * missingValuesCountStart;

            // Points for answer speed.
            points += puzzle.GetPointsForAnswerSpeed(1, 15);

            // Applies the points multiplier.
            points = Mathf.CeilToInt(points * player.pointsMultiplier);

            // Add points.
            player.points += points;


            // CALCULATING POWER ENERGY INCREASE //
            if(player.HasPower() && !player.IsPowerActive())
            {
                // Increases the power energy by a set amount.
                // player.power.IncreasePowerEnergy(); // Old

                // New - adds extra for rules used and missing values.
                player.power.IncreasePowerEnergy(rulesUsed.Count, missingValuesCountStart);
            }


            // Called when the equation has been completed.
            player.OnEquationComplete();

            // Generates a new equation if no one has won yet.
            puzzle.GenerateEquation();

            // Called when an equation has been generated.
            player.OnEquationGenerated();

            // Updates the UI that changes when an equation is completed.
            matchUI.UpdateOnEquationCompleteUI(player);

            // Checks if the player has won. If so, call the game finished function.
            if (HasPlayerWon(player))
            {
                OnMatchOver();
            }
        }

        // Checks if the provided player has won.
        public bool HasPlayerWon(PlayerMatch player)
        {
            bool result = player.points >= pointGoal;
            return result;
        }

        // Has Player 1 Won
        public bool HasPlayer1Won()
        {
            return HasPlayerWon(p1);
        }

        // Has Player 2 Won
        public bool HasPlayer2Won()
        {
            return HasPlayerWon(p2);
        }

        // Checks if the match is over.
        public bool IsMatchOver()
        {
            return matchUI.IsMatchEndActive();
        }

        // Called when the match is finished.
        public void OnMatchOver()
        {
            // Gets set to 'true' if p1 has one.
            PlayerMatch winner;

            // Checks if p1 has reached the points goal.
            if(HasPlayer1Won())
            {
                winner = p1;
                matchWinner = 1;


                // SCORE //

                // Increase game score.
                p1Score = 500; // Base game score increase.

                // Time Bonus
                // The bonus for match speed.
                float timeBonusLimit = 150.0F;

                // Checks the match time for extra points.
                if(matchTime < timeBonusLimit) // < 2.5 minutes
                {
                    // A max of 1000 points for a time bonus.
                    p1Score += Mathf.CeilToInt(1000.0F * (1.0F - (Mathf.Clamp(matchTime, 0, timeBonusLimit) / timeBonusLimit)));
                }
                else // > 2.5 minutes
                {
                    p1Score += 10;
                }

                // Wrong Answer Bonus
                // The wrong answer limit. TODO: account for a max of zero.
                int wrongAnswerLimit = 15 * p1Puzzle.missingValuesMax;

                // Checks the wrong answers for extra points.
                if (p1WrongAnswers < wrongAnswerLimit) // < Under wrong answer limit
                {
                    // A max of 1000 points for a time bonus.
                    p1Score += Mathf.CeilToInt(800.0F * (1.0F - (Mathf.Clamp(p1WrongAnswers, 0, wrongAnswerLimit) / wrongAnswerLimit)));
                }
                else // Over wrong answer limit
                {
                    p1Score += 5;
                }

                // Add to the game score (will get saved in GameInfo).
                gameScore += p1Score;
            }
            // Checks if p2 has reached the points goal.
            else if(HasPlayer2Won())
            {
                winner = p2;
                matchWinner = 2;

                // If the tutorial is active.
                if(IsUsingTutorial() && IsTutorialAvailable())
                {
                    // The tutorial instance.
                    Tutorial tutorial = Tutorial.Instance;

                    // If the player lost the first match.
                    if(!tutorial.clearedFirstMatchLoss && matchNumber <= 1) // First Match Loss
                    {
                        // First Match Loss
                        StartTutorial(tutorial.GetFirstMatchLossTutorial());
                    }
                    else if(!tutorial.clearedGameOver) // Game Over
                    {
                        // Game Over
                        StartTutorial(tutorial.GetGameOverTutorial());
                    }
                }
            }
            else
            {
                winner = null;
                matchWinner = 0;
            }

            // // The winner couldn't be set.
            // if(winner == null)
            // {
            //     Debug.LogWarning("The winner cannot be found.");
            //     return;
            // }

            // Pause the match to stop player inputs and AI calculations.
            PauseMatch();

            // Show the match end content.
            matchUI.ShowMatchEnd();

            // Play the results audio.
            if (matchAudio != null)
                matchAudio.PlayResultsBgm();
        }

        // Resets the match.
        public void ResetMatch()
        {
            // Resets Player
            p1.ResetPlayer();
            p2.ResetPlayer();

            // Reset Puzzle
            p1Puzzle.ResetPuzzle();
            p2Puzzle.ResetPuzzle();

            // Generate new equations (P1)
            p1Puzzle.GenerateEquation();
            p1.OnEquationGenerated();

            // Generate new equations (P2)
            p2Puzzle.GenerateEquation();
            p2.OnEquationGenerated();

            // Reset time.
            matchTime = 0;

            // Resets the match UI.
            matchUI.ResetMatchUI();

            // Plays the match BGM.
            if (matchAudio != null)
                matchAudio.PlayMatchBgm();


            // Unpause the match.
            UnpauseMatch();


            // TODO: should the animation be played when you hit rematch?
            // If the match start animation should be used.
            if(useMatchStartAnim && matchUI.matchStart != null)
            {
                // Plays the start animation.
                matchUI.matchStart.PlayStartAnimation();
            }
        }


        // SCENES //

        // Goes to the world scene.
        public void ToWorldScene()
        {
            // Gets the game info instance.
            GameplayInfo gameInfo = GameplayInfo.Instance;

            // Save the world and match info changes from the match.
            gameInfo.SaveMatchInfo(this);
            gameInfo.SaveWorldInfo(this);

            // Makes sure the game is unpaused.
            UnpauseGame();

            // TODO: add loading screen.
            SceneManager.LoadScene("WorldScene");
        }

        // GameComplete should not be called from the MatchManager. Only call it from the WorldManager.

        // Update is called once per frame
        protected override void Update()
        {
            // Calls post start if it hasn't been called yet.
            if (!calledPostStart)
                PostStart();

            // Calls the base update function.
            base.Update();

            // The match isn't paused.
            if (!MatchPaused)
            {
                matchTime += Time.unscaledDeltaTime;

                // TODO: move this to the matchUI script?
            }
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