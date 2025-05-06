#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // For TextMeshPro
using System.IO;

public class PythonScriptUploader : MonoBehaviour
{
    public Button uploadButton, teamTwoUploadBtn;
    public TMP_Text teamOneScriptNameText;  // Text field to display the script name for Team 1
    public TMP_Text teamTwoScriptNameText;  // Text field to display the script name for Team 2
    private string saveFolder;

    void Start()
    {
        // Use persistent path for both editor and build
        saveFolder = Path.Combine(Application.persistentDataPath, "PythonScripts");

        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
            Debug.Log("Created folder: " + saveFolder);
        }

        uploadButton.onClick.AddListener(() => UploadScriptForTeam(1));  // Upload for Team 1
        teamTwoUploadBtn.onClick.AddListener(() => UploadScriptForTeam(2));  // Upload for Team 2
    }

    void UploadScriptForTeam(int teamNumber)
    {
#if UNITY_EDITOR
        string path = EditorUtility.OpenFilePanel($"Upload Python Script for Team {teamNumber}", "", "py");

        if (!string.IsNullOrEmpty(path))
        {
            if (Path.GetExtension(path).ToLower() == ".py")
            {
                string fileName = Path.GetFileName(path);
                string savePath = Path.Combine(saveFolder, fileName);

                File.Copy(path, savePath, true);
                Debug.Log($"✅ Team {teamNumber} uploaded script: {fileName}");
                Debug.Log($"📁 Saved at: {savePath}");

                // Update the respective team's script name display
                if (teamNumber == 1)
                {
                    if (teamOneScriptNameText != null)
                    {
                        teamOneScriptNameText.text = fileName;  // Update the text field for Team 1
                    }
                }
                else if (teamNumber == 2)
                {
                    if (teamTwoScriptNameText != null)
                    {
                        teamTwoScriptNameText.text = fileName;  // Update the text field for Team 2
                    }
                }
            }
            else
            {
                Debug.LogWarning("⛔ Please upload a valid .py file.");
            }
        }
#else
        Debug.LogWarning("⚠ This feature only works in the Unity Editor.");
#endif
    }
}
