using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // An area in the world.
    public class Area : MonoBehaviour
    {
        // The world manager.
        public WorldManager manager;

        // The number of the area.
        public int areaNumber = 0;

        // A game object used for positioning the camera.
        // This world position is used to change the camera's position.
        public GameObject cameraPos;

        [Header("Challengers")]
        // The list of challengers.
        public List<ChallengerWorld> challengers = new List<ChallengerWorld>();

        // If 'true', the challengers are auto-added if the list is empty, and they're children of the area.
        public bool autoAddChallengers = true;

        [Header("Events")]

        // The event for the area.
        public AreaSwitchEvent areaEvent;

        // Start is called before the first frame update
        void Start()
        {
            if (manager == null)
                manager = WorldManager.Instance;

            // Checks if challengers should be auto-added.
            if(autoAddChallengers && challengers.Count == 0)
            {
                GetComponentsInChildren<ChallengerWorld>(challengers);
            }
        }



        // Update is called once per frame
        void Update()
        {

        }
    }
}