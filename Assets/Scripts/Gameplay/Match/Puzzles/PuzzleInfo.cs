using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // An instance that holds prefabs for all puzzle types.
    public class PuzzleInfo : MonoBehaviour
    {
        // The singleton instance.
        private static PuzzleInfo instance;

        // Gets set to 'true' when the singleton has been instanced.
        // This isn't needed, but it helps with the clarity.
        private bool instanced = false;

        [Header("Prefabs")]

        // The keypad mechanic.
        public KeypadMechanic keypad;

        // The bubbles mechanic.
        public BubbleMechanic bubble;

        // The sliding mechanic.
        public SlidingMechanic sliding;

        // The pinball mechanic.
        public PinballMechanic pinball;

        // Constructor
        private PuzzleInfo()
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
            }

            // Run code for initialization.
            if (!instanced)
            {
                instanced = true;
            }
        }

        // Gets the instance.
        public static PuzzleInfo Instance
        {
            get
            {
                // Checks if the instance exists.
                if (instance == null)
                {
                    // Tries to find the instance.
                    instance = FindObjectOfType<PuzzleInfo>(true);


                    // The instance doesn't already exist.
                    if (instance == null)
                    {
                        // Generate the instance.
                        GameObject go = new GameObject("Puzzle Prefabs (singleton)");
                        instance = go.AddComponent<PuzzleInfo>();
                    }

                }

                // Return the instance.
                return instance;
            }
        }

        // Returns 'true' if the object has been initialized.
        public bool Instantiated
        {
            get
            {
                return instanced;
            }
        }

        // Gets the rule name.
        public static string GetRuleName(exponentRule rule)
        {
            // Sets the rule name.
            string ruleName = "";

            // Checks the rule.
            switch (rule)
            {
                default:
                case exponentRule.none:
                    ruleName = "None";
                    break;

                case exponentRule.exponent:
                    ruleName = "Exponent";
                    break;

                case exponentRule.product:
                    ruleName = "Product";
                    break;

                case exponentRule.powerOfAPower:
                    ruleName = "Power of a Power";
                    break;

                case exponentRule.powerOfAProduct:
                    ruleName = "Power of a Product";
                    break;

                case exponentRule.zero:
                    ruleName = "Zero";
                    break;

                case exponentRule.negative:
                    ruleName = "Negative";
                    break;
            }

            return ruleName;
        }

        // Gets the rule name speak key
        public static string GetRuleNameSpeakKey(exponentRule rule)
        {
            // Sets the rule name speak key.
            string speakKey = "";

            // If the LOLManager wasn't instantiated, then neither was
            if (!SystemManager.IsLOLSDKInitialized())
                return "";

            // Checks the rule.
            switch (rule)
            {
                default:
                case exponentRule.none:
                    speakKey = "rle_none_nme";
                    break;

                case exponentRule.exponent:
                    speakKey = "rle_exponent_nme";
                    break;

                case exponentRule.product:
                    speakKey = "rle_product_nme";
                    break;

                case exponentRule.powerOfAPower:
                    speakKey = "rle_powerOfAPower_nme";
                    break;

                case exponentRule.powerOfAProduct:
                    speakKey = "rle_powerOfAProduct_nme";
                    break;

                case exponentRule.zero:
                    speakKey = "rle_zero_nme";
                    break;

                case exponentRule.negative:
                    speakKey = "rle_negative_nme";
                    break;
            }

            return speakKey;
        }

        // Gets the rule description.
        public static string GetRuleDescription(exponentRule rule)
        {
            // TODO: rewrite so that they make more sense.

            // Sets the rule description.
            string ruleDesc = "";

            // Checks the rule.
            switch (rule)
            {
                default:
                case exponentRule.none:
                    ruleDesc = "No exponent rule set.";
                    break;

                case exponentRule.exponent:
                    ruleDesc = "Multiplies a term by itself (n) number of times.";
                    break;

                case exponentRule.product:
                    ruleDesc = 
                        "If multiplying the same bases together, add their exponents together and apply the result to the base.";
                    break;

                case exponentRule.powerOfAPower:
                    ruleDesc =
                        "If a base has two exponents, multiply the exponents together and apply the result to the base.";
                    break;

                case exponentRule.powerOfAProduct:
                    ruleDesc = 
                        "If multiplying different bases with the same exponents together, multiply the bases, then apply the exponent to the result.";
                    break;

                case exponentRule.zero:
                    ruleDesc = 
                        "Anything to the exponent 0 always equals 1.";
                    break;

                case exponentRule.negative:
                    ruleDesc = 
                        "If using a negative exponent, take the reciprocal of the base, and apply the exponent to the denominator.";
                    break;
            }

            return ruleDesc;
        }

        // Gets the rule description speak key.
        public static string GetRuleDescriptionSpeakKey(exponentRule rule)
        {
            // Sets the rule description speak key.
            string speakKey = "";

            // If the LOLManager wasn't instantiated, then neither was
            if (!SystemManager.IsLOLSDKInitialized())
                return "";

            // Checks the rule.
            switch (rule)
            {
                default:
                case exponentRule.none:
                    speakKey = "rle_none_dsc";
                    break;

                case exponentRule.exponent:
                    speakKey = "rle_exponent_dsc";
                    break;

                case exponentRule.product:
                    speakKey = "rle_product_dsc";
                    break;

                case exponentRule.powerOfAPower:
                    speakKey = "rle_powerOfAPower_dsc";
                    break;

                case exponentRule.powerOfAProduct:
                    speakKey = "rle_powerOfAProduct_dsc";
                    break;

                case exponentRule.zero:
                    speakKey = "rle_zero_dsc";
                    break;

                case exponentRule.negative:
                    speakKey = "rle_negative_dsc";
                    break;
            }

            return speakKey;
        }
    }
}