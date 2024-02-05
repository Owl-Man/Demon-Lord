using SectorsSystem;
using UnityEngine;

namespace ArrowSystem
{
    public class ArrowCreator : MonoBehaviour
    {
        [SerializeField] private GameObject arrowPrefab;

        private Arrow _arrow;

        public static ArrowCreator Instance;

        private void Awake() => Instance = this;

        public void MoveTroughArrow(float moveDuration, int troopsCount)
        {
            _arrow.AcceptMoving(moveDuration, troopsCount);
            _arrow = null;
        }

        public void CreateArrow(Sector from, Sector to)
        {
            DestroyArrow();

            _arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity).GetComponent<Arrow>();
            
            _arrow.SetFrom(from);
            _arrow.SetTo(to);
        }

        public void DestroyArrow()
        {
            if (_arrow != null) Destroy(_arrow.gameObject);
        }
    }
}