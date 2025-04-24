#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PythonScriptUploader : MonoBehaviour
{
    public Button uploadButton,teamTwoUploadBtn;
    public PythonScriptRunner scriptManager;  // Reference to PythonScriptRunner
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
        
        uploadButton.onClick.AddListener(() => UploadScriptForTeam(1));
        teamTwoUploadBtn.onClick.AddListener(() => UploadScriptForTeam(2));
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

                if (scriptManager != null)
                {
                    scriptManager.LoadScriptList();
                    scriptManager.UpdateScriptDropdown(); // Refresh dropdown after upload
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
