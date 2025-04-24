using System.Collections;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

public class CapsulePythonController : MonoBehaviour
{
    private string pythonScriptPath;
    private Vector3 targetPosition;
    private float moveSpeed = 5f;
    private bool isMoving = false;

    private void Awake()
    {
        pythonScriptPath = Path.Combine(Application.streamingAssetsPath, "PythonScripts", "capsule_brain.py");
        Debug.Log("Python script path: " + pythonScriptPath);

    }

    private void Start()
    {
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            GetNewTargetFromPython();

            float elapsedTime = 0f;
            float moveDuration = 2f;
            Vector3 startPosition = transform.position;

            isMoving = true;

            while (elapsedTime < moveDuration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
            isMoving = false;

            yield return null;
        }
    }

    private void GetNewTargetFromPython()
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "python";
        start.Arguments = $"\"{pythonScriptPath}\"";
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.CreateNoWindow = true;

        using (Process process = Process.Start(start))
        {
            string result = process.StandardOutput.ReadLine();
            Debug.Log("Python output: " + result);

            if (!string.IsNullOrEmpty(result))
            {
                string[] parts = result.Split(',');
                if (parts.Length == 3)
                {
                    float.TryParse(parts[0], out float x);
                    float.TryParse(parts[1], out float y);
                    float.TryParse(parts[2], out float z);

                    targetPosition = transform.position + new Vector3(x, y, z);
                }
            }
        }
    }
}
