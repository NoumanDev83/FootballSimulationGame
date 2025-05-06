using UnityEngine;
using TMPro;

public class PythonScriptNameDisplay : MonoBehaviour
{
    // Text fields for both teams
    public TMP_Text teamOneScriptNameText;  // To display Team 1's uploaded script name
    public TMP_Text teamTwoScriptNameText;  // To display Team 2's uploaded script name

    // Method to update Team 1's script name
    public void UpdateTeamOneScriptName(string scriptName)
    {
        if (teamOneScriptNameText != null)
        {
            teamOneScriptNameText.text = $"Team 1 Script: {scriptName}";
        }
    }

    // Method to update Team 2's script name
    public void UpdateTeamTwoScriptName(string scriptName)
    {
        if (teamTwoScriptNameText != null)
        {
            teamTwoScriptNameText.text = $"Team 2 Script: {scriptName}";
        }
    }
}