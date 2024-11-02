using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RM_EM
{
    // The tutorial class. This is a singleton that gets gets consulted for tutorial text.
    public class Tutorial : MonoBehaviour
    {
        [System.Serializable]
        public struct TutorialData
        {
            // Bools for clearing certain tutorials.
            public bool clearedOpening;
            public bool clearedFirstMatchLoss;
            public bool clearedFirstMatchWin;

            public bool clearedFirstPower;
            public bool clearedFinalMatch;
            public bool clearedGameOver;

            // Exponents
            public bool clearedExponent;
            public bool clearedProduct;
            public bool clearedPowerOfAPower;
            public bool clearedProductOfAProduct;

            public bool clearedZero;
            public bool clearedNegative;
        }

        // The singleton instance.
        private static Tutorial instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private static bool instanced = false;

        [Header("Triggers")]

        // TODO: maybe seperate them based on the tutorial type (world, match). Maybe re-arrange the function.
        // Bools for clearing certain tutorials.
        public bool clearedOpening = false;
        public bool clearedFirstMatchLoss = false;
        public bool clearedFirstMatchWin = false;

        public bool clearedFirstPower = false;
        public bool clearedFinalMatch = false;
        public bool clearedGameOver = false;

        [Header("Triggers/Exponents")]
        public bool clearedExponent = false;
        public bool clearedProduct = false;
        public bool clearedPowerOfAPower = false;
        public bool clearedPowerOfAProduct = false;

        public bool clearedZero = false;
        public bool clearedNegative = false;

        // Constructor
        private Tutorial()
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
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // Don't destroy this game object on load.
            DontDestroyOnLoad(gameObject);
        }

        // Gets the instance.
        public static Tutorial Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<Tutorial>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("Tutorial (singleton)");
                        instance = go.AddComponent<Tutorial>();
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

        // Gets the test pages.
        public List<Page> GetTestPages()
        {
            // The test pages.
            List<Page> pages = new List<Page>()
            {
                new Page("This is a test."),
                new Page("This is only a test.")
            };

            // Returns the pages.
            return pages;
        }

        // The opening tutorial.
        public List<Page> GetOpeningTutorial()
        {
            // Pages
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                //pages.Add(new Page(defs["trl_opening_00"], "trl_opening_00"));
                //pages.Add(new Page(defs["trl_opening_01"], "trl_opening_01"));
                //pages.Add(new Page(defs["trl_opening_02"], "trl_opening_02"));
                //pages.Add(new Page(defs["trl_opening_03"], "trl_opening_03"));
                //pages.Add(new Page(defs["trl_opening_04"], "trl_opening_04"));
            }
            else
            {
                pages.Add(new Page("Welcome to the Exponent Club! I'm Minraul, and as you can guess, we play the exponent game here! Oh, you've never played the game before? Well, I guess I'll just have to teach you then!"));
                pages.Add(new Page("In the exponent game, you answer exponent questions as fast as you can to gain points and reach the goal before your opponent. The questions all have missing values, which you need to fill in correctly and in the right order."));
                pages.Add(new Page("The game will give you values to choose from to try and fill in empty slots. The way this is handled can vary by match, but they're all self-explanatory. Outside of selecting values, you can also use skips and powers."));
                pages.Add(new Page("Skips allow you to skip your current question and get a new one. Naturally, this means you get no points since you didn't finish the question. Skipping also has a cooldown to it, so it's best not to use it constantly."));
                pages.Add(new Page("As for powers, I'll talk more about them later. With all that explained, issue a challenge to me to start your first match! You can do so by selecting my icon in the world area."));
            }


            // If the world manager is instantiated.
            if (WorldManager.Instantiated)
            {
                // Gets the instance.
                WorldManager worldManager = WorldManager.Instance;

                // Show and Hide Diagram
                pages[3].OnPageOpenedAddCallback(worldManager.worldUI.tutorialTextBox.ShowSkipDiagram);
                pages[3].OnPageClosedAddCallback(worldManager.worldUI.tutorialTextBox.HideDiagram);
            }


            // Cleared the opening.
            clearedOpening = true;

            return pages;
        }


        // The first match loss tutorial.
        public List<Page> GetFirstMatchLossTutorial()
        {
            // The pages list.
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                // pages.Add(new Page(defs["trl_firstMatchLoss_00"], "trl_firstMatchLoss_00"));
            }
            else
            {
                pages.Add(new Page("You lost the match. You need to learn the basics before you can take on the other club members, so please try again."));
            }

            clearedFirstMatchLoss = true;

            return pages;
        }

        // The first match win/post-first match tutorial.
        public List<Page> GetFirstMatchWinTutorial()
        {
            // The pages list.
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                //pages.Add(new Page(defs["trl_firstMatchWin_00"], "trl_firstMatchWin_00"));
                //pages.Add(new Page(defs["trl_firstMatchWin_01"], "trl_firstMatchWin_01"));
            }
            else
            {
                pages.Add(new Page("You won your first match! Now that you're familiar with the rules, you can take on the other club members. Some of them won't accept your challenge until you become more experienced, so your options are restricted."));
                pages.Add(new Page("Some club members utilize exponent rules that you haven't learned yet. When you encounter a new rule, I'll be there to explain it. Feel free to check the 'info menu' as well if you ever need a refresher. Good luck!"));
            }

            clearedFirstMatchWin = true;

            return pages;
        }

        // The first power tutorial.
        public List<Page> GetFirstPowerTutorial()
        {
            // The pages list.
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                //pages.Add(new Page(defs["trl_firstPower_00"], "trl_firstPower_00"));
                //pages.Add(new Page(defs["trl_firstPower_01"], "trl_firstPower_01"));
                //pages.Add(new Page(defs["trl_firstPower_02"], "trl_firstPower_02"));
            }
            else
            {
                pages.Add(new Page("You just got a match power! A match power is a power-up that can be used during a match, but only after you charge it up. Powers charge up when you answer questions correctly, so just playing the game like usual is enough."));
                pages.Add(new Page("After a power is fully charged, you can activate it with the 'power button' at anytime. The power's effects remain active until the power's energy runs out, after which you need to charge it up again."));
                pages.Add(new Page("Unlocked powers can be viewed and equipped in the 'powers menu'. Your opponents can use powers too, so keep that in mind. Good luck!"));
            }

            clearedFirstPower = true;

            return pages;
        }

        // The final match tutorial.
        public List<Page> GetFinalMatchTutorial()
        {
            // The pages list.
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                //pages.Add(new Page(defs["trl_finalMatch_00"], "trl_finalMatch_00"));
            }
            else
            {
                pages.Add(new Page("You've learned all the exponent rules and defeated all the other club members, so now it's time for your final match! A rematch against me! Select my icon in the next area to challenge me like before."));
            }

            clearedFinalMatch = true;

            return pages;
        }

        // The game over tutorial.
        public List<Page> GetGameOverTutorial()
        {
            // The pages list.
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                //pages.Add(new Page(defs["trl_gameOver_00"], "trl_gameOver_00"));
            }
            else
            {
                pages.Add(new Page("You lost the match. You can rematch the opponent now or return to the world area. Every club member must be defeated for you to complete the challenge, but you can take as many tries as you like."));
            }

            clearedGameOver = true;

            return pages;
        }



        // EXPONENTS //
        // The exponent basics tutorial.
        public List<Page> GetExponentTutorial()
        {
            // Pages
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                //pages.Add(new Page(defs["trl_exponent_00"], "trl_exponent_00"));
            }
            else
            {
                pages.Add(new Page("This match only uses exponent basics. Exponents are math operations that multiply a base by itself a certain number of times. Check the info window if you ever want a refresh on the exponent rules."));
            }


            // If the world manager is instantiated.
            if (WorldManager.Instantiated)
            {
                // Gets the instance.
                WorldManager worldManager = WorldManager.Instance;

                // Makes the info button interactable.
                pages[0].OnPageClosedAddCallback(worldManager.worldUI.SetInfoButtonInteractable);

                // Show and Hide Diagram
                pages[0].OnPageOpenedAddCallback(worldManager.worldUI.tutorialTextBox.ShowExponentDiagram);
                pages[0].OnPageClosedAddCallback(worldManager.worldUI.tutorialTextBox.HideDiagram);
            }


            // Cleared the tutorial.
            clearedExponent = true;

            return pages;
        }

        // The product rule tutorial.
        public List<Page> GetProductRuleTutorial()
        {
            // The pages list.
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                //pages.Add(new Page(defs["trl_product_00"], "trl_product_00"));
            }
            else
            {
                pages.Add(new Page("This match uses the product rule. When the same bases with different exponents are multiplied together, the exponents can be summed together and applied to said base to simplify the operation."));
            }


            // If the world manager is instantiated.
            if (WorldManager.Instantiated)
            {
                // Gets the instance.
                WorldManager worldManager = WorldManager.Instance;

                // Show and Hide Diagram
                pages[0].OnPageOpenedAddCallback(worldManager.worldUI.tutorialTextBox.ShowProductDiagram);
                pages[0].OnPageClosedAddCallback(worldManager.worldUI.tutorialTextBox.HideDiagram);
            }

            
            clearedProduct = true;

            return pages;
        }

        // The power of a power rule tutorial.
        public List<Page> GetPowerOfAPowerRuleTutorial()
        {
            // The pages list.
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                //pages.Add(new Page(defs["trl_powerOfAPower_00"], "trl_powerOfAPower_00"));
            }
            else
            {
                pages.Add(new Page("This match uses the power of a power rule. If a base is having two exponents applied to it, then said exponents can be multiplied together, with the base being raised to the product of that operation."));
            }


            // If the world manager is instantiated.
            if (WorldManager.Instantiated)
            {
                // Gets the instance.
                WorldManager worldManager = WorldManager.Instance;

                // Show and Hide Diagram
                pages[0].OnPageOpenedAddCallback(worldManager.worldUI.tutorialTextBox.ShowPowerOfAPowerDiagram);
                pages[0].OnPageClosedAddCallback(worldManager.worldUI.tutorialTextBox.HideDiagram);
            }


            clearedPowerOfAPower = true;

            return pages;
        }

        // The product of a product rule tutorial.
        public List<Page> GetPowerOfAProductRuleTutorial()
        {
            // The pages list.
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                //pages.Add(new Page(defs["trl_powerOfAProduct_00"], "trl_powerOfAProduct_00"));
            }
            else
            {
                pages.Add(new Page("This match uses the power of a product rule. If two bases with the same exponents are multiplied together, it can be simplified to the multiplication of said bases, with the result being raised to the shared exponent."));
            }


            // If the world manager is instantiated.
            if (WorldManager.Instantiated)
            {
                // Gets the instance.
                WorldManager worldManager = WorldManager.Instance;

                // Show and Hide Diagram
                pages[0].OnPageOpenedAddCallback(worldManager.worldUI.tutorialTextBox.ShowPowerOfAProductDiagram);
                pages[0].OnPageClosedAddCallback(worldManager.worldUI.tutorialTextBox.HideDiagram);
            }


            clearedPowerOfAProduct = true;

            return pages;
        }

        // The zero exponent rule tutorial.
        public List<Page> GetZeroExponentRuleTutorial()
        {
            // The pages list.
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                //pages.Add(new Page(defs["trl_zero_00"], "trl_zero_00"));
            }
            else
            {
                pages.Add(new Page("This match uses the zero rule. Any number raised to the exponent 0 will return a result of 1."));
            }


            // If the world manager is instantiated.
            if (WorldManager.Instantiated)
            {
                // Gets the instance.
                WorldManager worldManager = WorldManager.Instance;

                // Show and Hide Diagram
                pages[0].OnPageOpenedAddCallback(worldManager.worldUI.tutorialTextBox.ShowZeroDiagram);
                pages[0].OnPageClosedAddCallback(worldManager.worldUI.tutorialTextBox.HideDiagram);
            }


            clearedZero = true;

            return pages;
        }

        // The negative exponent rule tutorial.
        public List<Page> GetNegativeExponentRuleTutorial()
        {
            // The pages list.
            List<Page> pages = new List<Page>();

            // Loads the pages
            if (false) // Translation
            {
                //pages.Add(new Page(defs["trl_negative_00"], "trl_negative_00"));
            }
            else
            {
                pages.Add(new Page("This match uses the negative rule. A number raised to a negative exponent will become its reciprocal, and have the exponent be applied to the denominator."));
            }


            // If the world manager is instantiated.
            if (WorldManager.Instantiated)
            {
                // Gets the instance.
                WorldManager worldManager = WorldManager.Instance;

                // Show and Hide Diagram
                pages[0].OnPageOpenedAddCallback(worldManager.worldUI.tutorialTextBox.ShowNegativeDiagram);
                pages[0].OnPageClosedAddCallback(worldManager.worldUI.tutorialTextBox.HideDiagram);
            }


            clearedNegative = true;

            return pages;
        }


        // OTHER //
        // Generates tutorial data.
        public TutorialData GenerateTutorialData()
        {
            TutorialData data = new TutorialData();

            // Saving the data
            data.clearedOpening = clearedOpening;
            data.clearedFirstMatchLoss = clearedFirstMatchLoss;
            data.clearedFirstMatchWin = clearedFirstMatchWin;

            data.clearedFirstPower = clearedFirstPower;
            data.clearedFinalMatch = clearedFinalMatch;
            data.clearedGameOver = clearedGameOver;

            data.clearedExponent = clearedExponent;
            data.clearedProduct = clearedProduct;
            data.clearedPowerOfAPower = clearedPowerOfAPower;
            data.clearedProductOfAProduct = clearedPowerOfAProduct;

            data.clearedZero = clearedZero;
            data.clearedNegative = clearedNegative;

            return data;
        }

        // Loads tutorial data.
        public void LoadTutorialData(TutorialData data)
        {
            // Loading the data
            clearedOpening = data.clearedOpening;
            clearedFirstMatchLoss = data.clearedFirstMatchLoss;
            clearedFirstMatchWin = data.clearedFirstMatchWin;

            clearedFirstPower = data.clearedFirstPower;
            clearedFinalMatch = data.clearedFinalMatch;
            clearedGameOver = data.clearedGameOver;

            clearedExponent = data.clearedExponent;
            clearedProduct = data.clearedProduct;
            clearedPowerOfAPower = data.clearedPowerOfAPower;
            clearedPowerOfAProduct = data.clearedProductOfAProduct;

            clearedZero = data.clearedZero;
            clearedNegative = data.clearedNegative;
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