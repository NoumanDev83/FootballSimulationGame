using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PythonScriptRunner : MonoBehaviour
{
    public TMP_Dropdown scriptDropdown;
    public TMP_Text outputText;
    public Button runButton;

    private string scriptFolder;
    public List<string> scriptFiles = new List<string>();

    private string scriptFolderPath;
    
    private bool isInitialized = false;
    
    void Awake()
    {
        // Initialize the scriptFolderPath in Awake (or Start)
        scriptFolderPath = Path.Combine(Application.persistentDataPath, "PythonScripts");
    }
    void Start()
    {
        if (isInitialized) return;

        scriptFolder = Path.Combine(Application.persistentDataPath, "PythonScripts");

        if (!Directory.Exists(scriptFolder))
            Directory.CreateDirectory(scriptFolder);

        LoadScriptList();

        runButton.onClick.AddListener(() =>
        {
            if (scriptDropdown.value >= 0 && scriptDropdown.value < scriptFiles.Count)
            {
                RunPythonScript(scriptFiles[scriptDropdown.value]);
            }
        });
    }

    public void LoadScriptList()
    {
        scriptDropdown.ClearOptions();
        scriptFiles.Clear();

        string[] pyFiles = Directory.GetFiles(scriptFolder, "*.py");
        List<string> options = new List<string>();

        foreach (string filePath in pyFiles)
        {
            string fileName = Path.GetFileName(filePath);
            scriptFiles.Add(filePath);
            options.Add(fileName);
        }

        if (options.Count > 0)
        {
            scriptDropdown.AddOptions(options);
            outputText.text = "Select a script and click Run.";
        }
        else
        {
            outputText.text = "No Python scripts found!";
        }
    }

    public void RunPythonScript(string path)
    {
        string pythonPath = @"C:\Users\HP\AppData\Local\Programs\Python\Python313\python.exe"; // Adjust for your Python version
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = pythonPath,  // Use the full path here
            Arguments = $"\"{path}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = new Process { StartInfo = psi };
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        outputText.text = string.IsNullOrWhiteSpace(error) ? output : $"Error:\n{error}";
    }

    public void ClearScriptsFolder()
    {
        if (Directory.Exists(scriptFolderPath))
        {
            string[] scripts = Directory.GetFiles(scriptFolderPath, "*.py");

            foreach (string script in scripts)
            {
                File.Delete(script);  // Delete the script
                print("Deleted: " + script);  // Optional: log the deletion
            }
        }
        else
        {
            print("No PythonScripts folder found.");
        }

        // After clearing, update the dropdown to reflect the changes
        StartCoroutine(RefreshScriptsDropdown());
    }

    private IEnumerator RefreshScriptsDropdown()
    {
        // Wait for the file system changes to reflect (a short delay)
        yield return new WaitForSeconds(0.1f);

        // Now update the dropdown
        UpdateScriptDropdown();
    }

    public void UpdateScriptDropdown()
    {
        if (string.IsNullOrEmpty(scriptFolderPath))
        {
            //UnityEngine.Debug.LogError("Script folder path is null or empty!");
            return;
        }
        
        // Clear the dropdown options
        scriptDropdown.ClearOptions();

        // Get the updated list of Python scripts from the directory
        string[] scripts = Directory.GetFiles(scriptFolderPath, "*.py");

        // Create a list of script names to populate the dropdown
        var options = new List<string>();
        foreach (string script in scripts)
        {
            string scriptName = Path.GetFileName(script);  // Extract the file name
            options.Add(scriptName);
        }

        // Add the options to the dropdown (this will refresh the dropdown)
        scriptDropdown.AddOptions(options);

        // Display appropriate message if there are no scripts
        if (options.Count == 0)
        {
            outputText.text = "No Python scripts found!";
        }
        else
        {
            outputText.text = "Select a script and click Run.";
        }

        // Ensure the dropdown is properly refreshed
        scriptDropdown.RefreshShownValue();
    }


    // OnValidate will trigger whenever there is a change in the scriptFiles list
    // This ensures the list is updated in the inspector immediately
    void OnValidate()
    {
        UpdateScriptDropdown();
    }
    
    
}
