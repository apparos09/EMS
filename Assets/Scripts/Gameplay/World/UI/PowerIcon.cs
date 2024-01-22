using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RM_EM
{
    // The power icon.
    public class PowerIcon : MonoBehaviour
    {
        // The power being displayed.
        private Power.powerType power;

        // The image for the power.
        public Image symbolImage;

        // Hides the symbol if the power is set to none.
        [Tooltip("Hides the symbol if no power is selected.")]
        public bool hideIfNone = true;

        // Gets the power.
        public Power.powerType GetPower()
        {
            return power;
        }

        // Sets the power.
        public void SetPower(Power.powerType newPower)
        {
            // Sets the power.
            power = newPower;

            // Turn on the object.
            symbolImage.gameObject.SetActive(true);

            // Sets the sprite
            symbolImage.sprite = PowerInfo.Instance.GetPowerSymbol(power);


            // Checks if the power symbol should be hidden.
            if (hideIfNone && power == Power.powerType.none)
                symbolImage.gameObject.SetActive(false);
        }

    }
}