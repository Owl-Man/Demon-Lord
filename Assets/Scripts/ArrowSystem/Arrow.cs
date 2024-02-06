using System.Collections;
using SectorsSystem;
using UnityEngine;

namespace ArrowSystem
{
    public class Arrow : MonoBehaviour
    {
        private Sector _from, _to;
        private LineRenderer _lineRenderer;

        private int _troops;

        private bool _isMoveEnd;

        private void Awake() => _lineRenderer = GetComponent<LineRenderer>();
        
        private void Start()
        {
            _lineRenderer.positionCount = 40;
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.sortingOrder = 11;
        }
        
        private void Update()
        {
            if (_from == null) return;
            
            Vector3[] positions = new Vector3[40];
            
            for (int i = 1; i < 39; i++)
            {
                positions[i] = Vector3.Lerp(_from.mainHouse.position, _to.mainHouse.position, i / 40f);
            }
            
            _lineRenderer.SetPositions(positions);
        
            _lineRenderer.SetPosition(0, _from.mainHouse.position);
            _lineRenderer.SetPosition(39, _to.mainHouse.position);

            transform.position = _to.mainHouse.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _to.mainHouse.position - _from.mainHouse.position);
        }
        
        public void SetFrom(Sector from) => _from = from;

        public void SetTo(Sector to) => _to = to;

        public void AcceptMoving(float duration, int troops)
        {
            _troops = troops;
            StartCoroutine(Moving(duration));
        }

        private IEnumerator Moving(float duration)
        {
            _isMoveEnd = false;
            StartCoroutine(MoveDurationTimer(duration));

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

                colorTime += 0.0008f * Time.timeScale;

                yield return new WaitForSeconds(0.001f / (Time.timeScale * 2));
            }
            
            SectorsTroopsMove.Instance.CalculateTroopsMovingResult(_from, _to, _troops);
            
            Destroy(gameObject);
        }

        private IEnumerator MoveDurationTimer(float duration)
        {
            yield return new WaitForSeconds(duration);
            _isMoveEnd = true;
        }
    }
}