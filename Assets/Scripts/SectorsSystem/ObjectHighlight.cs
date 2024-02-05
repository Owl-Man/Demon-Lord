using System.Collections;
using UnityEngine;

namespace SectorsSystem
{
    [ExecuteAlways]
    public class ObjectHighlight : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private PolygonCollider2D _polygonCollider2D;

        private Vector2[] _vertices;

        [SerializeField] private float _defaultLineWidth;

        public bool isEnabled;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _polygonCollider2D = GetComponent<PolygonCollider2D>();
        }

        private void Start()
        {
            isEnabled = false;

            _defaultLineWidth = _lineRenderer.startWidth;
        }

        private void Update() => UpdateHighlight();

        private void UpdateHighlight()
        {
            if (!isEnabled) return;
        
            _vertices = _polygonCollider2D.points;

            _lineRenderer.positionCount = _vertices.Length;

            for (int i = 0; i < _vertices.Length; i++)
            {
                Vector3 position = new Vector3(_vertices[i].x, _vertices[i].y, 1);
                _lineRenderer.SetPosition(i, position);
            }
        }

        private IEnumerator Pulsing()
        {
            while (true)
            {
                while (_lineRenderer.startWidth >= 0.04f)
                {
                    _lineRenderer.startWidth -= 0.001f;
                    yield return new WaitForSecondsRealtime(0.008f);
                }

                yield return new WaitForSecondsRealtime(0.2f);

                while (_lineRenderer.startWidth <= _defaultLineWidth + 0.02f)
                {
                    _lineRenderer.startWidth += 0.001f;
                    yield return new WaitForSecondsRealtime(0.008f);
                }
            
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }

        public void EnableHighlight(bool isWithPulse, bool isSectorOccupied)
        {
            _lineRenderer.enabled = true;
            isEnabled = true;

            if (isSectorOccupied)
            {
                _lineRenderer.startColor = SectorsManager.Instance.playerSectorColor;
                _lineRenderer.endColor = SectorsManager.Instance.playerSectorColor;
            }
            else
            {
                _lineRenderer.startColor = SectorsManager.Instance.enemySectorColor;
                _lineRenderer.endColor = SectorsManager.Instance.enemySectorColor;
            }

            if (isWithPulse)
            {
                StopAllCoroutines();
                StartCoroutine(Pulsing());
            }
        }

        public void DisableHighlight()
        {
            _lineRenderer.enabled = false;
            isEnabled = false;

            _lineRenderer.startWidth = _defaultLineWidth;
            StopAllCoroutines();
        }
    }
}