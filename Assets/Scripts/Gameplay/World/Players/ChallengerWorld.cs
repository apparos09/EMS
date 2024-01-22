using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // A challenger that's encountered in the game world.
    // The 'World' part is added to make it clearer that it belongs to the world area, not the match area.
    public class ChallengerWorld : MonoBehaviour
    {
        // World manager.
        public WorldManager manager;

        // The collider for the challenger.
        public new BoxCollider2D collider;

        // The sprite renderer for the the challenger.
        public SpriteRenderer spriteRenderer;

        // The sprite for the challenger prompt.
        [Tooltip("The sprite for the challenge prompt.")]
        public Sprite challengeSprite;

        // The challenger's colour (defaults to challenger UI BG colour)
        public Color challengerColour = new Color(0.7009641F, 0.08330365F, 0.8490566F);

        [Header("Info")]

        // The challenger's name.
        public string challengerName = string.Empty;

        // The name language key.
        [Tooltip("The language key for the name.")]
        public string nameKey = string.Empty;

        // The challenger species.
        public string challengerSpecies = string.Empty;

        // The species key.
        public string speciesKey = string.Empty;

        // The challenger's quote.
        public string challengerQuote = string.Empty;

        // The quote language key.
        [Tooltip("THe language key for the quote.")]
        public string quoteKey = string.Empty;

        // The difficulty of the challenger.
        // TODO: set to private
        public int difficulty = 0;

        // Sets if the challenger is available for a match.
        // Set to public for testing purposes.
        private bool available = true;

        // Gets set to 'true' when the challenger has been defeated.
        // Set to public for testing purposes.
        private bool defeated = false;

        [Header("Puzzle")]
        // The puzzle type.
        public Puzzle.puzzleType puzzle = Puzzle.puzzleType.unknown;

        // The power the challenger has.
        public Power.powerType power = Power.powerType.none;

        [Header("Exponents")]
        // The exponent rates.
        public float exponentRate = 1.0F;

        // Rate for product rule/multiplicaton (same bases) exponents.
        public float productRate = 1.0F;

        // Rate for power of a power/exponent by exponent exponents.
        public float powerOfAPowerRate = 1.0F;

        // Rate for power of a product/multplication (different bases) exponents.
        public float powerOfAProductRate = 1.0F;

        // Rate for zero exponent rule.
        public float zeroRate = 1.0F;

        // Rate for negative exponent rule.
        public float negativeRate = 1.0F;

        [Header("Terms")]

        // The minimum number of terms for the challenger's questions.
        [Tooltip("The minimum number of equation terms.")]
        public int equationTermsMin = 1;

        // The maximum number of terms for the challenger's questions.
        [Tooltip("The maximum number of equation terms.")]
        public int equationTermsMax = 1;

        // The exponent term min must be at least 2.
        // The minimum number of terms for the base exponent rule.
        [Tooltip("The minimum number of terms for the base exponent rule (combined rules only).")]
        public int exponentTermsMin = 2;

        // The maximum number of terms for the base exponent rule.
        [Tooltip("The maximum number of terms for the base exponent rule (combined rules only).")]
        public int exponentTermsMax = 2;

        // The minimum number of missing values.
        [Tooltip("The minimum number of missing values.")]
        public int missingValuesMin = 1;

        // The maximum number of missing values.
        [Tooltip("The maximum number of missing values.")]
        public int missingValuesMax = 1;


        // Start is called before the first frame update
        void Start()
        {
            // Manager.
            if (manager == null)
                manager = WorldManager.Instance;

            // Checks for the collider.
            if (collider == null)
            {
                // Tries to get the collider (no longer checks children for misinput concerns).
                collider = GetComponent<BoxCollider2D>();
            }

            // Checks for the sprite renderer.
            if (spriteRenderer == null)
            {
                // Tries to get the component (no longer checks children for misinput concerns).
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

        }

        // MouseDown
        private void OnMouseDown()
        {
            // Grabs the instance if it's not set.
            if (manager == null)
                manager = WorldManager.Instance;

            // Show the challenger prompt if no window is open, and if the tutorial text box isn't open.
            if(!manager.worldUI.IsWindowOpen() && !manager.worldUI.IsTutorialTextBoxOpen())
            {
                // Shows the challenge UI.
                ShowChallengeUI();
            }
            
        }

        // Returns 'true' if the challenger is available.
        public bool IsChallengerAvailable()
        {
            return available;
        }

        // Sets if the challenger is locked or not.
        public void SetChallengerAvailable(bool avail)
        {
            available = avail;

            // Checks if the challenger is available.
            if (available) // Available
            {
                collider.enabled = true;
                spriteRenderer.enabled = true;
            }
            else // Unavailable.
            {
                collider.enabled = false;
                spriteRenderer.enabled = false;
            }
        }

        // Sets the challenger to be available.
        public void SetChallengerToAvailable()
        {
            SetChallengerAvailable(true);
        }

        // Sets the challenger to be unavailable.
        public void SetChallengerToUnavailable()
        {
            SetChallengerAvailable(false);
        }

        // Checks if the challenger has been defeated.
        public bool IsChallengerDefeated()
        {
            return defeated;
        }

        // Sets if the challenger is defeated.
        public void SetChallengerDefeated(bool defeat)
        {
            defeated = defeat;

            // Checks if the sprite renderer is enabled.
            bool rendererEnabled = spriteRenderer.enabled;

            // Changes the colour based on if the challenger is defeated or not.
            if (defeated && spriteRenderer.color != Color.grey) // Greyed Out
            {
                spriteRenderer.enabled = true;
                spriteRenderer.color = Color.grey;
                spriteRenderer.enabled = rendererEnabled;
            }
            else if(!defeated && spriteRenderer.color != Color.white) // Regular Colour
            {
                spriteRenderer.enabled = true;
                spriteRenderer.color = Color.white;
                spriteRenderer.enabled = rendererEnabled;
            }
        }

        // Tries to show the challenge UI and loads in the content.
        public void ShowChallengeUI()
        {
            // Checks if the challenger is available.
            if(available)
            {
                // Checks if the challenger has been defeated yet. If not, allow the challenge.
                if(!defeated)
                {
                    // If the challenge UI is not active.
                    if (!manager.worldUI.IsChallengerUIActive())
                    {
                        manager.ShowChallengerUI(this, manager.GetChallengerIndex(this));
                    }
                }    
            }
        }

        //// Update is called once per frame
        //void Update()
        //{

        //}
    }
}