using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Assets.FootballGameEngine_Indie_.Scripts.Entities;
using Assets.FootballGameEngine_Indie.Scripts.Entities;

[System.Serializable]
public class ActionData
{
    public string team; // "team1" or "team2"
    public string action; // "MoveForward", "Shoot", "Pass", etc.
    public int player_index; // Index of the player in the team
}

[System.Serializable]
public class ActionDataArrayWrapper
{
    public ActionData[] array;  // Contains an array of ActionData
}

public class pythonInputRunner : MonoBehaviour
{
    // File path for the player_actions.json file in StreamingAssets/PythonScripts/
    private string filePath = Path.Combine(Application.streamingAssetsPath, "PythonScripts", "player_actions.json");

    // References to the lists of players (assign these in Inspector)
    public List<Player> _teamHomePlayers;
    public List<Player> _teamAwayPlayers;

    // This will be triggered when you press the "Run" button
    public void OnRunButtonPressed()
    {
        StartCoroutine(ReadFileAndProcessActions());
    }

    private IEnumerator ReadFileAndProcessActions()
    {
        // Continuously check the JSON file every second
        while (true)
        {
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                // Read the content of the file
                string json = File.ReadAllText(filePath);

                // Parse the JSON array of actions by wrapping it in the ActionDataArrayWrapper class
                ActionDataArrayWrapper wrapper = JsonUtility.FromJson<ActionDataArrayWrapper>("{\"array\":" + json + "}");

                // Process each action in the array
                foreach (var action in wrapper.array)
                {
                    ProcessMatchActions(action);
                }

                // Optional: Clear the file after reading it (if you only want to process the file once)
                // File.WriteAllText(filePath, string.Empty);  // Uncomment if you want to clear the file after processing

                break; // Exit the loop once actions are processed
            }

            // Wait for 1 second before checking the file again
            yield return new WaitForSeconds(1);
        }
    }

    // Method to process the actions and perform them in Unity
    private void ProcessMatchActions(ActionData action)
    {
        // Log the action to verify it's being processed
        Debug.Log($"Processing action: {action.team} - {action.action} for player {action.player_index}");

        // Determine the team and the player index
        List<Player> teamPlayers = (action.team == "team1") ? _teamHomePlayers : _teamAwayPlayers;

        // Ensure that the player index is valid
        if (action.player_index < 0 || action.player_index >= teamPlayers.Count)
        {
            Debug.LogError($"Invalid player index {action.player_index} for team {action.team}");
            return;
        }

        Player player = teamPlayers[action.player_index];

        // Execute the action based on the action type
        switch (action.action)
        {
            case "MoveForward":
                MovePlayerForward(player);
                break;

            case "Shoot":
                ShootBall(player);
                break;

            case "Pass":
                PassBall(player);
                break;

            case "MoveBackward":
                MovePlayerBackward(player);
                break;

            default:
                Debug.LogError($"Unknown action: {action.action}");
                break;
        }
    }

    // Example method to move the player forward (adjust for your actual player movement logic)
    private void MovePlayerForward(Player player)
    {
        player.RPGMovement.SetMoveDirection(player.transform.forward); // Moves the player forward
    }

    // Example method to shoot the ball (adjust based on your Player class)
    private void ShootBall(Player player)
    {
        player.CanScore(out Shot shot);  // Check if the player can score
        if (shot != null)
        {
            player.MakeShot(shot); // Pass the shot to the MakeShot function
        }
    }

    // Example method for making a pass (adjust based on your Player class)
    private void PassBall(Player player)
    {
        Pass pass;
        if (player.CanLongPassInDirection(player.transform.forward, out pass)) // Assuming this is a valid method in your Player class
        {
            player.MakePass(pass);  // Call the MakePass method to execute the pass
        }
    }

    // Example method to move the player backward (adjust for your actual player movement logic)
    private void MovePlayerBackward(Player player)
    {
        player.RPGMovement.SetMoveDirection(-player.transform.forward); // Moves the player backward
    }
}

// Helper class for deserializing JSON arrays
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        // Wrap JSON array in an object to parse it correctly
        string wrappedJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrappedJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
