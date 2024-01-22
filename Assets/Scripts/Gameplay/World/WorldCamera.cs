using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The world camera.
    public class WorldCamera : MonoBehaviour
    {
        // The world manager.
        public WorldManager manager;

        // The camera script.
        public new Camera camera;

        [Header("Transition")]
        // The camera's destination position.
        public Vector3 destPos;

        // Sets set to 'true', when the camera is transitioning to another position.
        public bool inTransition = false;

        // The camera's movement speed.
        public float speed = 10.0F;

        // Start is called before the first frame update
        void Start()
        {
            // Autoset manager.
            if (manager == null)
                manager = WorldManager.Instance;

            // If not set, try to get the camera component.
            if(camera == null)
            {
                // If that doesn't work, set it as the main camera.
                if(!TryGetComponent<Camera>(out camera))
                {
                    camera = Camera.main;
                }
            }

            // Set to camera's position by default.
            destPos = transform.position;
        }

        // Sets the position of the world camera (no transition).
        public void SetPosition(Vector3 newPos)
        {
            transform.position = newPos;
            destPos = newPos;
            inTransition = false;

            //// Makes a tutorial check.
            //TryStartTutorial();
        }

        // Sets the position of the world camera (no transition).
        public void SetPosition(GameObject dest)
        {
            SetPosition(dest.transform.position);
        }

        // Sets the destination position.
        public void Move(Vector3 newPos, bool instant = false)
        {
            // If the camera should instantly move to its given location.
            if(instant)
            {
                SetPosition(newPos);
            }
            else
            {
                destPos = newPos;
                inTransition = true;
            }
            
        }

        // Sets the destination position using an object.
        public void Move(GameObject dest, bool instant = false)
        {
            Move(dest.transform.position, instant);
        }

        //// Tries to start the tutorial.
        //public void TryStartTutorial()
        //{
        //    // Tutorial inactive, so do nothing.
        //    if (!manager.IsUsingTutorial())
        //        return;

        //    // Gets the instance.
        //    Tutorial tutorial = Tutorial.Instance;

        //    // Moved.
        //    //// Checks if the final match tutorial has been shown yet.
        //    //// This only happens once the transition to the final area has finished.
        //    //if (!tutorial.clearedFinalMatch && manager.IsTutorialAvailable() && manager.InFinalArea())
        //    //{
        //    //    manager.StartTutorial(tutorial.GetFinalMatchTutorial());
        //    //}
        //}

        // Update is called once per frame
        void Update()
        {
            // Checks if the camera is transitioning.
            if(inTransition)
            {
                // TODO: replace with easing lerp?
                
                // Generate the new position.
                Vector3 newPos = Vector3.MoveTowards(transform.position, destPos, speed * Time.deltaTime);

                // Set the new position.
                transform.position = newPos;

                // Destination reached.
                if(newPos == destPos)
                {
                    inTransition = false;

                    //// Tutorial check.
                    //TryStartTutorial();
                }
            }
        }
    }
}