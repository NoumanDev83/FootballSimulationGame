using Assets.SimpleSteering.Scripts.Movement;
using UnityEngine;

public class PlayerControlWrapper : MonoBehaviour
{
    private RPGMovement rpgMovement;
    private bool isAIEnabled = true;

    void Start()
    {
        // Get the RPGMovement component (which is responsible for player movement)
        rpgMovement = GetComponent<RPGMovement>();
    }

    // Call this method to pause the AI
    public void PauseAI()
    {
        if (rpgMovement != null)
        {
            // Disable steering and tracking to stop the default AI behavior
            rpgMovement.SetSteeringOff();
            rpgMovement.SetTrackingOff();
        }
        isAIEnabled = false;
    }

    // Call this method to resume the AI
    public void ResumeAI()
    {
        if (rpgMovement != null)
        {
            // Enable steering and tracking to resume the default AI behavior
            rpgMovement.SetSteeringOn();
            rpgMovement.SetTrackingOn();
        }
        isAIEnabled = true;
    }

    // Optionally check if AI is enabled
    public bool IsAIEnabled()
    {
        return isAIEnabled;
    }

    // You can add more controls here if needed to provide finer control over movement or other AI behaviors.
}