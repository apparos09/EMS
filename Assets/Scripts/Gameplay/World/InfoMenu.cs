using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RM_EM
{
    // The Info Menu
    public class InfoMenu : MonoBehaviour
    {
        // THe INfo Entry
        public struct InfoEntry
        {
            // The Rule
            public exponentRule rule;
            
            // Name and Speak Key
            public string name;
            public string nameKey;

            // Description and Speak Key
            public string description;
            public string descKey;

            // The diagram
            public Sprite diagramSprite;

            // Description Speak Key

        }

        // The rule name.
        public TMP_Text ruleName;

        // The rule description.
        public TMP_Text ruleDesc;

        // The diagram image.
        public Image diagramImage;

        // The list of entries.
        public List<InfoEntry> entries = new List<InfoEntry>();

        // The entry index.
        public int entryIndex = 0;

        // The previous and next buttons.
        public Button prevButton;
        public Button nextButton;

        [Header("Rules")]
        // Exponents
        public bool clearedExponent;
        public bool clearedProduct;
        public bool clearedPowerOfAPower;
        
        public bool clearedPowerOfAProduct;
        public bool clearedZero;
        public bool clearedNegative;

        [Header("Diagrams")]
        // Diagrams
        public Sprite exponentDiagram;
        public Sprite productDiagram;
        public Sprite powerOfAPowerDiagram;
        
        public Sprite powerOfAProductDiagram;
        public Sprite zeroDiagram;
        public Sprite negativeDiagram;

        // Start is called before the first frame update
        void Start()
        {
            LoadEntries();
        }

        // Loads entries on enable.
        private void OnEnable()
        {
            LoadEntries();
        }

        // Loads the entries.
        public void LoadEntries()
        {
            // Checks if the tutorial is being used.
            bool usingTutorial = false;

            // Checks if the tutorial is instantiated.
            if (Tutorial.Instantiated)
            {
                // If the game settings exist, reference it to see if the tutorial is active.
                // If it doesn't exist, just set it to false.
                usingTutorial = GameSettings.Instantiated ? GameSettings.Instance.UseTutorial : false;
            }

            // // For testing purposes only.
            // usingTutorial = false;

            // Checks if the tutorial is being used.
            if (usingTutorial) // Only enable encountered rules.
            {
                // Gets the tutorial.
                Tutorial tutorial = Tutorial.Instance;

                clearedExponent = tutorial.clearedExponent;
                clearedProduct = tutorial.clearedProduct;
                clearedPowerOfAPower = tutorial.clearedPowerOfAPower;
                clearedPowerOfAProduct = tutorial.clearedPowerOfAProduct;
                clearedZero = tutorial.clearedZero;
                clearedNegative = tutorial.clearedNegative;
            }
            else // Not being used, so enable all.
            {
                clearedExponent = true;
                clearedProduct = true;
                clearedPowerOfAPower = true;
                clearedPowerOfAProduct = true;
                clearedZero = true;
                clearedNegative = true;
            }


            // Clears the list.
            entries.Clear();

            // A new entry.
            InfoEntry newEntry;

            // Creating Entries
            // Exponent
            if(clearedExponent)
            {
                newEntry = new InfoEntry();

                newEntry.rule = exponentRule.exponent;
                
                newEntry.name = PuzzleInfo.GetRuleName(exponentRule.exponent);
                newEntry.nameKey = PuzzleInfo.GetRuleNameSpeakKey(exponentRule.exponent);

                newEntry.description = PuzzleInfo.GetRuleDescription(exponentRule.exponent);
                newEntry.descKey = PuzzleInfo.GetRuleDescriptionSpeakKey(exponentRule.exponent);

                newEntry.diagramSprite = exponentDiagram;

                entries.Add(newEntry);
            }

            // Product
            if(clearedProduct)
            {
                newEntry = new InfoEntry();

                newEntry.rule = exponentRule.product;

                newEntry.name = PuzzleInfo.GetRuleName(exponentRule.product);
                newEntry.nameKey = PuzzleInfo.GetRuleNameSpeakKey(exponentRule.product);

                newEntry.description = PuzzleInfo.GetRuleDescription(exponentRule.product);
                newEntry.descKey = PuzzleInfo.GetRuleDescriptionSpeakKey(exponentRule.product);

                newEntry.diagramSprite = productDiagram;

                entries.Add(newEntry);
            }

            // Power of a Power
            if(clearedPowerOfAPower)
            {
                newEntry = new InfoEntry();

                newEntry.rule = exponentRule.powerOfAPower;

                newEntry.name = PuzzleInfo.GetRuleName(exponentRule.powerOfAPower);
                newEntry.nameKey = PuzzleInfo.GetRuleNameSpeakKey(exponentRule.powerOfAPower);

                newEntry.description = PuzzleInfo.GetRuleDescription(exponentRule.powerOfAPower);
                newEntry.descKey = PuzzleInfo.GetRuleDescriptionSpeakKey(exponentRule.powerOfAPower);

                newEntry.diagramSprite = powerOfAPowerDiagram;

                entries.Add(newEntry);
            }

            // Power of a Product
            if(clearedPowerOfAProduct)
            {
                newEntry = new InfoEntry();

                newEntry.rule = exponentRule.powerOfAProduct;
                
                newEntry.name = PuzzleInfo.GetRuleName(exponentRule.powerOfAProduct);
                newEntry.nameKey = PuzzleInfo.GetRuleNameSpeakKey(exponentRule.powerOfAProduct);

                newEntry.description = PuzzleInfo.GetRuleDescription(exponentRule.powerOfAProduct);
                newEntry.descKey = PuzzleInfo.GetRuleDescriptionSpeakKey(exponentRule.powerOfAProduct);

                newEntry.diagramSprite = powerOfAProductDiagram;

                entries.Add(newEntry);

            }

            // Zero
            if(clearedZero)
            {
                newEntry = new InfoEntry();

                newEntry.rule = exponentRule.zero;

                newEntry.name = PuzzleInfo.GetRuleName(exponentRule.zero);
                newEntry.nameKey = PuzzleInfo.GetRuleNameSpeakKey(exponentRule.zero);

                newEntry.description = PuzzleInfo.GetRuleDescription(exponentRule.zero);
                newEntry.descKey = PuzzleInfo.GetRuleDescriptionSpeakKey(exponentRule.zero);

                newEntry.diagramSprite = zeroDiagram;

                entries.Add(newEntry);
            }

            // Negative
            if(clearedNegative)
            {
                newEntry = new InfoEntry();

                newEntry.rule = exponentRule.negative;

                newEntry.name = PuzzleInfo.GetRuleName(exponentRule.negative);
                newEntry.nameKey = PuzzleInfo.GetRuleNameSpeakKey(exponentRule.negative);

                newEntry.description = PuzzleInfo.GetRuleDescription(exponentRule.negative);
                newEntry.descKey = PuzzleInfo.GetRuleDescriptionSpeakKey(exponentRule.negative);

                newEntry.diagramSprite = negativeDiagram;

                entries.Add(newEntry);
            }
            
            // Enabling/disabling the arrows
            if(entries.Count > 1)
            {
                prevButton.interactable = true;
                nextButton.interactable = true;
            }
            else
            {
                prevButton.interactable = false;
                nextButton.interactable = false;
            }

            // Loads an entry.
            SetEntry(0);
        }

        // Goes to the previous entry.
        public void PreviousEntry()
        {
            // Index
            int index = entryIndex - 1;

            // Boudns check.
            if (index < 0)
                index = entries.Count - 1;

            // Load entry.
            SetEntry(index);
        }

        // Goe to the next entry.
        public void NextEntry()
        {
            // Index
            int index = entryIndex + 1;

            // Boudns check.
            if (index >= entries.Count)
                index = 0;

            // Load entry.
            SetEntry(index);
        }


        // Sets the entry.
        public void SetEntry(int index)
        {
            // No entry to set.
            if (index < 0 || index >= entries.Count)
                return;

            // Sets the index.
            entryIndex = index;

            // Gets the entry.
            InfoEntry entry = entries[index];

            ruleName.text = entry.name;
            ruleDesc.text = entry.description;

            diagramImage.sprite = entry.diagramSprite;

            // If the LOL Manager has been instantiated.
            if(GameSettings.Instance.UseTextToSpeech)
            {
                // The LOL Manager
                LOLManager lolManager = LOLManager.Instance;

                // If there is a description key, read it.
                if (entry.descKey != "")
                    lolManager.SpeakText(entry.descKey);

            }
        }
    }
}