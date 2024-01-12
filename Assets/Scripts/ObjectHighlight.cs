using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private PolygonCollider2D _polygonCollider2D;

    private Vector2[] _vertices;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        
        _vertices = _polygonCollider2D.points;

        _lineRenderer.positionCount = _vertices.Length;
    }

    private void Update() => UpdateHighlight();

    private void UpdateHighlight()
    {
        _vertices = _polygonCollider2D.points;
        
        for (int i = 0; i < _vertices.Length; i++)
        {
            Vector3 position = new Vector3(_vertices[i].x, _vertices[i].y, 1);
            _lineRenderer.SetPosition(i, position);
        }
    }
}