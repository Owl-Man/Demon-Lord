﻿using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private PolygonCollider2D _polygonCollider2D;

    private Vector2[] _vertices;

    public bool IsEnabled { get; private set; }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        _vertices = _polygonCollider2D.points;

        _lineRenderer.positionCount = _vertices.Length;
    }

    private void Update() => UpdateHighlight();

    private void UpdateHighlight()
    {
        if (!IsEnabled) return;
        
        _vertices = _polygonCollider2D.points;
        
        for (int i = 0; i < _vertices.Length; i++)
        {
            Vector3 position = new Vector3(_vertices[i].x, _vertices[i].y, 1);
            _lineRenderer.SetPosition(i, position);
        }
    }

    public void EnableHighlight()
    {
        _lineRenderer.enabled = true;
        IsEnabled = true;
    }

    public void DisableHighlight()
    {
        _lineRenderer.enabled = false;
        IsEnabled = false;
    }
}