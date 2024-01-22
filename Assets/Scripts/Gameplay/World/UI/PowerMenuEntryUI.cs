using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RM_EM
{
    // The power menu entry.
    public class PowerMenuEntryUI : MonoBehaviour
    {
        // The power menu.
        public PowerMenu powerMenu;

        // The power info.
        public PowerInfo powerInfo;

        // The power image.
        public Image powerSymbolImage;

        // The power name (shorthand)?
        public TMP_Text powerNameText;

        // The power entry from the UI.
        public PowerMenu.PowerEntry entry;



        // Start is called before the first frame update
        void Start()
        {
            // Gets the instance.
            if (powerInfo == null)
                powerInfo = PowerInfo.Instance;
        }

        // Loads the entry.
        public void SetEntry(PowerMenu.PowerEntry newEntry)
        {
            entry = newEntry;

            // TODO: get symbol by type.
            powerSymbolImage.sprite = newEntry.symbol;
            powerNameText.text = newEntry.name;
        }

        // Clears the entry.
        public void ClearEntry()
        {
            // Empty.
            entry = new PowerMenu.PowerEntry();

            powerSymbolImage.sprite = powerInfo.defaultSymbol;
            powerNameText.text = "-";
        }

        // Selects this entry.
        public void SelectEntry()
        {
            powerMenu.SetSelectedEntry(this);
        }
    }
}