using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RM_EM
{
    // The UI for the challenger.
    public class ChallengerUI : MonoBehaviour
    {
        // The world manager.
        public WorldManager manager;

        // The challenger the challenge is being issued by.
        public ChallengerWorld challenger;

        // The index of the challenger.
        public int challengerIndex = -1;

        [Header("Images")]

        // The renderer of the challenger art.
        public Image challengerRenderer;

        // The background of the UI.
        public Image background;

        // The default colour.
        public Color defaultBgClr = new Color(0.7009641F, 0.08330365F, 0.8490566F);

        // If set to 'true', the default colour is autoset.
        public bool autoSetDefaultBgClr = true;

        // If set to 'true', the background's colour changes based on the challenger.
        public bool useCustomBgClr = true;

        [Header("Text")]

        // The name text.
        public TMP_Text nameText;

        // The species text.
        public TMP_Text speciesText;

        // The quote text.
        public TMP_Text quoteText;

        // The versus challenger text.
        public TMP_Text vsChallengerText;

        // The rules text for the UI.
        public TMP_Text rulesText;

        [Header("Buttons")]

        // Accept Button
        public Button acceptButton;

        // Decline Button
        public Button declineButton;


        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            // If the default BG colour should be autoset...
            if(autoSetDefaultBgClr && background != null)
            {
                // Sets the BG color.
                defaultBgClr = background.color;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // Manager set.
            if (manager == null)
                manager = WorldManager.Instance;
        }

        // This function is called when the object becomes enabled and active
        private void OnEnable()
        {
            // May not be needed.
            // EnableButtons();

            // Grabs the instance.
            if(manager == null)
                manager = WorldManager.Instance;

            // Plays the button SFX upon being activated.s
            if (manager.worldAudio != null)
                manager.worldAudio.PlayButtonSfx();

        }

        // TODO: add clear challenger option?
        // Sets the challenger.
        public void SetChallenger(ChallengerWorld newChallenger, int index)
        {
            challenger = newChallenger;
            challengerIndex = index;

            // Original
            UpdateChallengerSprite();
            UpdateNameText();
            UpdateSpeciesText();
            UpdateQuoteText();
            UpdateRulesText();

            // New
            // Gets the versus text.
            string versus = LOLManager.IsLOLSDKInitialized() ?
                LOLManager.Instance.GetLanguageText("kwd_versus") :
                "Vs.";

            // Gets the challenger name.
            string chalName = nameText.text;

            // Sets the versus challenger text.
            // This isn't efficient, but I'm not sure if I want to delete everything. I probably should.
            // TODO: remove the old system to optimize how this is set.
            vsChallengerText.text = versus + " " + chalName;

            
            // Should a custom colour be used, and are the assets set?
            if(useCustomBgClr && background != null && challenger != null)
            {
                background.color = challenger.challengerColour;
            }
            else
            {
                background.color = defaultBgClr;
            }
        }

        // Updates the Challenger Sprite
        public void UpdateChallengerSprite()
        {
            if(challenger != null) // Set sprite.
            {
                challengerRenderer.sprite = challenger.challengeSprite;
            }
            else // Clear sprite.
            {
                challengerRenderer.sprite = null;
            }
            
        }

        // Updates the name text.
        public void UpdateNameText()
        {
            // Checks if the challenger is set.
            if (challenger != null)
                nameText.text = challenger.challengerName;
            else
                nameText.text = "-";
        }

        // Updates the species text.
        public void UpdateSpeciesText()
        {
            // Checks if the challenger is set.
            if (challenger != null)
                speciesText.text = challenger.challengerSpecies;
            else
                speciesText.text = "-";
        }

        // Updates the quote text.
        public void UpdateQuoteText()
        {
            // Checks if the challenger is set.
            if (challenger != null)
                quoteText.text = "\"" + challenger.challengerQuote + "\"";
            else
                quoteText.text = "\"-\"";
        }

        // Updates the rules text.
        public void UpdateRulesText()
        {
            // If the challenger isn't set, clear the rules and return.
            if(challenger == null)
            {
                rulesText.text = string.Empty;
                return;
            }

            // Rule names list.
            List<string> ruleNames = new List<string>();

            // Contains a list of all rules.
            string rules = "";

            // The rule count.
            const int RULE_COUNT_MAX = 6;

            // Exponent Rule
            if (challenger.exponentRate > 0)
                ruleNames.Add(PuzzleInfo.GetRuleName(exponentRule.exponent));

            // Product Rule
            if (challenger.productRate> 0)
                ruleNames.Add(PuzzleInfo.GetRuleName(exponentRule.product));

            // Power of a Power
            if (challenger.powerOfAPowerRate > 0)
                ruleNames.Add(PuzzleInfo.GetRuleName(exponentRule.powerOfAPower));

            // Power of a Product
            if (challenger.powerOfAProductRate > 0)
                ruleNames.Add(PuzzleInfo.GetRuleName(exponentRule.powerOfAProduct));

            // Zero Rule
            if (challenger.zeroRate > 0)
                ruleNames.Add(PuzzleInfo.GetRuleName(exponentRule.zero));

            // Negative Rule
            if (challenger.negativeRate > 0)
                ruleNames.Add(PuzzleInfo.GetRuleName(exponentRule.negative));


            // Checks for valid rules.
            if(ruleNames.Count != 0)
            {
                // Set to rule count max, so just list as "ALL"
                if(ruleNames.Count == RULE_COUNT_MAX)
                {
                    // Checks if the LOLSDK is initialized.
                    if(LOLManager.IsLOLSDKInitialized())
                    {
                        rules = "All";
                    }
                    else
                    {
                        // Set to 'All'.
                        rules = "All";
                    }
                }
                else // Add the rule names.
                {
                    // Adds all the rules to the string.
                    for (int i = 0; i < ruleNames.Count; i++)
                    {
                        // Add the rule.
                        rules += ruleNames[i];

                        // If this isn't the last rule, add a slash.
                        if (i + 1 < ruleNames.Count)
                        {
                            rules += "/";
                        }
                    }
                }
                
            }
            else
            {
                rules = "-";
            }

            // Set the rules text.
            rulesText.text = rules;
        }

        // Accepts the challenge.
        public void AcceptChallenge()
        {
            manager.AcceptChallenge();
        }

        // Declines the challenge.
        public void DeclineChallenge()
        {
            manager.DeclineChallenge();
        }

        // Enables the buttons for the challenger UI.
        public void EnableButtons()
        {
            acceptButton.interactable = true;
            declineButton.interactable = true;
        }

        // Disables the buttons for the challenger UI.
        public void DisableButtons()
        {
            acceptButton.interactable = false;
            declineButton.interactable = false;
        }
    }
}