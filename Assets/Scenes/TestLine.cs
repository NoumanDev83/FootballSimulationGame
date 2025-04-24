 
using UnityEngine;

public class TestLine : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = Color.green;
        _lineRenderer.endColor = Color.green;

        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, new Vector3(0, 0, 5));
    }
}
