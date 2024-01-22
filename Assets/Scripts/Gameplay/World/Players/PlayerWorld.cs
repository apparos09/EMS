using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The player in the world scene.
    public class PlayerWorld : MonoBehaviour
    {
        // The match manager.
        public WorldManager manager;

        // The power the player has.
        public Power.powerType power;

        // The list of powers the player has.
        public List<Power.powerType> powerList = new List<Power.powerType>();

        // Start is called before the first frame update
        void Start()
        {
            if (manager == null)
                manager = WorldManager.Instance;
        }

        // Sorts the power list.
        public void SortPowerList()
        {
            // Sorts the power list.
            if (powerList.Count > 0)
                powerList.Sort();
        }


        // Checks if the player has the provided power.
        public bool HasPower(Power.powerType powerType)
        {
            if (powerList.Contains(powerType))
                return true;
            else
                return false;
        }

        // Checks if the player has powers.
        public bool HasPowers()
        {
            return powerList.Count > 0;
        }

        // Gets the player's current power.
        public Power.powerType GetPower()
        {
            return power;
        }

        // Selects a power by type.
        // If 'savePower' is true, the power is saved to the list if it's not already in the list.
        public void SetPower(Power.powerType newPower, bool savePower = true)
        {
            // If the power should be saved.
            if(savePower)
            {
                // Checks if the player's has the power in their list.
                if (!powerList.Contains(newPower) && newPower != Power.powerType.none)
                {
                    powerList.Add(newPower);
                }
            }
            

            // Set the power.
            power = newPower;
        }

        // Sets the power using the index.
        public void SetPower(int index)
        {
            if (index > 0 && index < powerList.Count)
                power = powerList[index];
        }

        // Removes the player's power.
        public void RemovePower()
        {
            SetPower(Power.powerType.none, false);
        }

        // Gives a power to the player.
        // If 'selectPower' is true, the power is also automatically selected.
        public void GivePower(Power.powerType newPower, bool selectPower)
        {
            // If the new power is not equal to none.
            if(newPower != Power.powerType.none)
            {
                // If the power list does not contain the provided power, add it.
                if (!powerList.Contains(newPower))
                    powerList.Add(newPower);
            }

            // Sorts the power list.
            SortPowerList();

            // If the power should also be selected.
            if(selectPower)
            {
                SetPower(newPower);
            }
        }

        // Gets the index of the player's equipped power.
        public int GetPowerIndex()
        {
            return powerList.IndexOf(power);
        }
    }
}