using System.Collections;
using SectorsSystem;
using UnityEngine;

namespace ArrowSystem
{
    public class Arrow : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        private bool _isMoveEnd;

        private void Awake() => _lineRenderer = GetComponent<LineRenderer>();

        public void StartMoving(float duration) => StartCoroutine(Moving(duration));

        private IEnumerator Moving(float duration)
        {
            _isMoveEnd = false;
            StartCoroutine(Timer(duration));

            float colorTime = 0;

            while (!_isMoveEnd)
            {
                Gradient gradient = new Gradient();

                gradient.SetKeys(
                    new GradientColorKey[]
                    {
                        new GradientColorKey(new Color(1f, 0.73f, 0.52f, 1), colorTime), new GradientColorKey(Color.white, colorTime + 0.01f)
                    },
                    new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 1.0f), new GradientAlphaKey(1.0f, 1.0f) }
                );

                _lineRenderer.colorGradient = gradient;

                colorTime += 0.0008f;

                yield return null;
            }
            
            SectorsTroopsMove.Instance.CalculateTroopsMovingResult();
            
            Destroy(gameObject);
        }

        private IEnumerator Timer(float duration)
        {
            yield return new WaitForSeconds(duration);
            _isMoveEnd = true;
        }
    }
}