using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SectorsSystem
{
    public class Sector : MonoBehaviour
    {
        public Transform mainHouse;
        public List<Sector> neighbourSectors;
        public int troopsCount;

        public bool isSectorOccupied;
    
        private Camera _camera;
        private EventSystem _eventSystem;
        private ObjectHighlight _outline;

        private void Start()
        {
            _camera = Camera.main;
            _eventSystem = EventSystem.current;
            
            _outline = GetComponent<ObjectHighlight>();
        
            _outline.DisableHighlight();
        }

        public void OnSelectSector()
        {
            SectorsManager.Instance.ChoseTheSector(this);
        }

        public void EnableSectorInteraction(bool isWithPulse)
        {
            _outline.EnableHighlight(isWithPulse, isSectorOccupied);
        }

        public void DisableSectorInteraction()
        {
            _outline.DisableHighlight();
        }
    
        private void Update()
        {
            if (_eventSystem.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            {
                Vector2 touchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            SectorsManager.Instance.sectorOnWhichWasPointer = this;
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0) && SectorsManager.Instance.sectorOnWhichWasPointer == this)
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            OnSelectSector();
                        }
                    }
                }
            }
        }
    }
}