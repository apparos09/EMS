using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RM_EM
{
    // The UI for the power menu.
    public class PowerMenu : MonoBehaviour
    {
        // The power entry.
        public struct PowerEntry
        {
            // The power type.
            public Power.powerType power;

            // Symbol
            public Sprite symbol;

            // Name and Speak Key
            public string name;
            public string nameKey;

            // Description
            public string description;
            public string descKey;
        }

        // The world manager.
        public WorldManager worldManager;

        // The world UI.
        public WorldUI worldUI;

        // The power symbols.
        public PowerInfo powerSymbols;

        // The player.
        public PlayerWorld playerWorld;

        // The power entries the player can select.
        public List<PowerEntry> powerEntryList = new List<PowerEntry>();

        // The entry page index (the item at the index is the first one displayed).
        [Tooltip("The entry page index. The entry at this index is the first one in the display.")]
        public int entryPageIndex = -1;
        
        // The selected index.
        public int selectedEntryIndex = -1;

        // The selected entry.
        public PowerEntry selectedEntry;


        [Header("Entries (UI)")]
        // Menu entry 0
        public PowerMenuEntryUI powerMenuEntry0;

        // Menu entry 1
        public PowerMenuEntryUI powerMenuEntry1;

        // Menu entry 2
        public PowerMenuEntryUI powerMenuEntry2;

        // The power menu UI entry count.
        public const int POWER_MENU_ENTRY_UI_COUNT = 3;

        // The previous entry button.
        public Button prevEntryButton;

        // The next entry button.
        public Button nextEntryButton;

        // The button for equipping powers.
        public Button equipButton;

        [Header("Entries (UI)/Selected")]

        // The selected power symbol.
        public Image selectedPowerSymbol;

        // The selected power name.
        public TMP_Text selectedPowerName;

        // The selected power description.
        public TMP_Text selectedPowerDesc;

        [Header("Other")]

        // The player power icon.
        public PowerIcon playerPowerIcon;

        // Start is called before the first frame update
        void Start()
        {
            // Manager
            if (worldManager == null)
                worldManager = WorldManager.Instance;

            // World UI
            if (worldUI == null)
                worldUI = worldManager.worldUI;

            // Power Info
            if (powerSymbols == null)
                powerSymbols = PowerInfo.Instance;

            // Player World
            if (playerWorld == null)
                playerWorld = worldManager.playerWorld;

            // Loads the player power list.
            LoadPlayerPowerList();
        }

        // OnEnable
        private void OnEnable()
        {
            LoadPlayerPowerList();
        }

        // Generates a power none entry.
        public PowerEntry GeneratePowerEntry(Power.powerType power)
        {
            // Entry
            PowerEntry entry = new PowerEntry();

            // Power and Symbol
            entry.power = power;
            entry.symbol = PowerInfo.Instance.GetPowerSymbol(entry.power);

            // Name and Key
            entry.name = PowerInfo.GetPowerTypeName(entry.power);
            entry.nameKey = PowerInfo.GetPowerTypeNameSpeakKey(entry.power);

            // Description and Key
            entry.description = PowerInfo.GetPowerTypeDescription(entry.power);
            entry.descKey = PowerInfo.GetPowerTypeDescriptionSpeakKey(entry.power);

            return entry;
        }

        // Loads the player power list.
        public void LoadPlayerPowerList()
        {
            // Selected entry index.
            selectedEntryIndex = 0;

            // Sorts the power list.
            playerWorld.SortPowerList();

            // Clears the list.
            powerEntryList.Clear();

            // Generates a none entry as the first spot (used to deselect powers).
            powerEntryList.Add(GeneratePowerEntry(Power.powerType.none));

            // Default to first index (nothing power).
            selectedEntryIndex = 0;

            // Goes through all of the player's powers.
            for (int i = 0; i < playerWorld.powerList.Count; i++)
            {
                // New entry.
                PowerEntry entry = new PowerEntry();

                // Saves the power information
                // Power and Symbol
                entry.power = playerWorld.powerList[i];
                entry.symbol = PowerInfo.Instance.GetPowerSymbol(entry.power);

                // Name and Speak Key
                entry.name = PowerInfo.GetPowerTypeName(entry.power);
                entry.nameKey = PowerInfo.GetPowerTypeNameSpeakKey(entry.power);

                // Description and Speak Key
                entry.description = PowerInfo.GetPowerTypeDescription(entry.power);
                entry.descKey = PowerInfo.GetPowerTypeDescriptionSpeakKey(entry.power);


                // Adds the entry to the list.
                powerEntryList.Add(entry);

                // If this is the player's current power, set this as the selected power.
                if (playerWorld.powerList[i] == playerWorld.power)
                {
                    selectedEntryIndex = powerEntryList.Count - 1;
                    selectedEntry = powerEntryList[i];
                }
            }


            // Sets to the first page.
            entryPageIndex = 0;

            // Loads the entry into the UI.
            LoadEntriesIntoUI(entryPageIndex);

            // If not available, set to defaults.
            {
                // TODO: this could be optimized.
                
                // The speak key to be used.
                string speakKey = "";

                // Checks if the player has a power equipped.
                if (playerWorld.power != Power.powerType.none)
                {
                    // Set UI
                    SetSelectedPowerUI(playerWorld.power);

                    // Sets the speak key.
                    speakKey = PowerInfo.GetPowerTypeDescriptionSpeakKey(playerWorld.power);
                }
                else // None power
                {
                    // Set UI
                    SetSelectedPowerUI(powerEntryList[0].power);

                    // Sets the speak key.
                    speakKey = PowerInfo.GetPowerTypeDescriptionSpeakKey(powerEntryList[0].power);
                }



                // Disable the equip button since the player's power is being disabled.
                equipButton.interactable = false;

                // Text-To-Speech
                if (GameSettings.Instance.UseTextToSpeech && SystemManager.IsLOLSDKInitialized())
                {
                    SystemManager.Instance.SpeakText(speakKey);
                }
            }
            
            

            // Make buttons interactable.
            prevEntryButton.interactable = true;
            nextEntryButton.interactable = true;

            // If there are no other pages...
            if (powerEntryList.Count <= 3)
            {
                prevEntryButton.interactable = false;
                nextEntryButton.interactable = false;
            }

        }

        // Loads the entries into the UI.
        public void LoadEntriesIntoUI()
        {
            // Makes sure index is valid.
            entryPageIndex = Mathf.Clamp(entryPageIndex, 0, powerEntryList.Count - 1);

            // Puts the entries into an array.
            PowerMenuEntryUI[] entryArr = new PowerMenuEntryUI[3] { powerMenuEntry0, powerMenuEntry1, powerMenuEntry2 };

            // The current index.
            int currIndex = entryPageIndex;

            // Goes through each index.
            foreach(PowerMenuEntryUI entryUI in entryArr) 
            {
                // Activate the object.
                if(!entryUI.gameObject.activeSelf)
                    entryUI.gameObject.SetActive(true);

                // If the index is valid, load the entry.
                if (currIndex >= 0 && currIndex < powerEntryList.Count)
                {
                    entryUI.SetEntry(powerEntryList[currIndex]);
                }
                else // Invalid, so clear entry.
                {
                    // Clear the entry.
                    entryUI.ClearEntry();

                    // Hide the object since it won't be used.
                    entryUI.gameObject.SetActive(false);
                }

                // Increase the index.
                currIndex++;

                // Reset the currIndex variable.
                if (currIndex >= powerEntryList.Count)
                    currIndex = 0;
            }

        }

        // Sets thei ndex and loads the entries.
        public void LoadEntriesIntoUI(int index)
        {
            entryPageIndex = index;
            LoadEntriesIntoUI();
        }

        // Goes to the previous page.
        public void PreviousPage()
        {
            // If there's 3 entries or less (i.e., all of them can appear on screen)...
            // Then don't allow page switching.
            if (powerEntryList.Count <= POWER_MENU_ENTRY_UI_COUNT)
                return;

            // Shift by -1.
            int index = entryPageIndex - 1;

            // Bounds check.
            if (index < 0)
                index = powerEntryList.Count - 1;

            // Load the entries.
            LoadEntriesIntoUI(index);
        }

        // Goe to the next page.
        public void NextPage()
        {
            // If there's 3 entries or less (i.e., all of them can appear on screen)...
            // Then don't allow page switching.
            if (powerEntryList.Count <= POWER_MENU_ENTRY_UI_COUNT)
                return;

            // Shift by +1.
            int index = entryPageIndex + 1;

            // Bounds checks.
            if (index >= powerEntryList.Count)
                index = 0;

            // Load the entries.
            LoadEntriesIntoUI(index);
        }

        // Set the selected entry.
        public void SetSelectedEntry(PowerMenuEntryUI entryUI)
        {
            // Set the index.
            int entryIndex = -1;

            // Checks each entry to see which one the provided entry is.
            if(powerMenuEntry0 == entryUI)
            {
                entryIndex = 0;
            }
            else if(powerMenuEntry1 == entryUI)
            {
                entryIndex = 1;
            }
            else if(powerMenuEntry2 == entryUI)
            {
                entryIndex = 2;
            }

            // If the entry index is greater than 0, set the index.
            // The page index aligns with the first entry in the list.
            if(entryIndex > 0)
                entryIndex = entryPageIndex + entryIndex;

            // Sets the selected entry index.
            selectedEntryIndex = entryIndex;

            // Selects this entry.
            selectedEntry = entryUI.entry;
            selectedEntry.power = entryUI.entry.power;

            // Set the symbol.
            selectedPowerSymbol.sprite = selectedEntry.symbol;

            // The selected power's name.
            selectedPowerName.text = selectedEntry.name;
            selectedPowerDesc.text = selectedEntry.description;


            // Set button to be interactable.
            equipButton.interactable = true;

            // If the player has this power, disable the equip button.
            if(playerWorld.power == selectedEntry.power)
            {
                equipButton.interactable = false;
            }

            // If text-to-speech should be used.
            if(GameSettings.Instance.UseTextToSpeech && SystemManager.IsLOLSDKInitialized())
            {
                // Gets the manager.
                SystemManager lolManager = SystemManager.Instance;

                // Speak the text.
                lolManager.SpeakText(selectedEntry.descKey);
            }
        }

        // Sets the UI using the provided power (does not effect selectedIndex).
        public void SetSelectedPowerUI(Power.powerType power)
        {
            selectedPowerSymbol.sprite = PowerInfo.Instance.GetPowerSymbol(power);
            selectedPowerName.text = PowerInfo.GetPowerTypeName(power);
            selectedPowerDesc.text = PowerInfo.GetPowerTypeDescription(power);
        }


        // EQUIP / UNEQUIP
        // Equips the selected power.
        public void EquipPower()
        {
            // The selected entry.
            playerWorld.SetPower(selectedEntry.power);

            // Sets the equip button.
            equipButton.interactable = true;

            // If the power was selected, disable the equip button.
            if(playerWorld.power == selectedEntry.power)
            {
                equipButton.interactable = false;
            }

            // Set the new power icon.
            if (playerPowerIcon != null)
                playerPowerIcon.SetPower(playerWorld.power);
        }

        // Unequips the current power.
        public void UnequipPower()
        {
            // Remove the power.
            playerWorld.RemovePower();

            // Sets to the 'none' power.
            // TODO: streamline this
            selectedPowerSymbol.sprite = powerEntryList[0].symbol;
            selectedPowerName.text = powerEntryList[0].name;
            selectedPowerDesc.text = powerEntryList[0].description;

            // Set the power icon.
            if (playerPowerIcon != null)
                playerPowerIcon.SetPower(powerEntryList[0].power);
        }

        //// Update is called once per frame
        //void Update()
        //{
        //    // ...
        //}
    }
}