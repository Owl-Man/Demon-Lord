using System;
using ArrowSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SectorsSystem
{
    public class SectorsTroopsMove : MonoBehaviour
    {
        [SerializeField] private Slider troopsSlider;
        [SerializeField] private TMP_Text troopsCount;
        [SerializeField] private GameObject controlBtns;
        
        [SerializeField] private Button acceptBtn;

        [SerializeField] private float moveDuration;
        
        private Sector _currentFrom, _currentTo;

        public static SectorsTroopsMove Instance;

        private int _troopsCount;

        private void Awake() => Instance = this;

        public bool isMoveModeOn { get; private set; }
        
        public void SliderTroopsUpdate() => troopsCount.text = Convert.ToString(troopsSlider.value);

        public void SetDestination(Sector sector)
        {
            _currentTo = sector;

            ArrowCreator.Instance.CreateArrow(_currentFrom, _currentTo);

            acceptBtn.interactable = true;
        }

        public Sector GetFromSector() => _currentFrom;

        public void AcceptMove()
        {
            _troopsCount = Convert.ToInt32(troopsSlider.value);
            
            _currentFrom.troopsCount -= _troopsCount;
            
            ArrowCreator.Instance.MoveTroughArrow(moveDuration, _troopsCount);

            DisableMoveMode();
        }

        public void CalculateTroopsMovingResult(Sector from, Sector to, int troopsCount)
        {
            if (to.isSectorOccupied)
            {
                to.troopsCount += troopsCount;
            }
            else //Attack
            {
                to.troopsCount -= troopsCount;

                if (to.troopsCount < 0)
                {
                    to.troopsCount *= -1;
                    to.isSectorOccupied = true;
                }
            }
        }
        
        public void ActivateMoveMode()
        {
            isMoveModeOn = true;
            controlBtns.SetActive(true);

            SectorsManager.Instance.PrepareForMovingTroops(out _currentFrom);

            MapScroll.Instance.isScrollActiveAboveUI = false;

            troopsSlider.minValue = 1;
            troopsSlider.maxValue = _currentFrom.troopsCount;

            acceptBtn.interactable = false;
        }

        public void CancelMoveMode()
        {
            DisableMoveMode();
            ArrowCreator.Instance.DestroyArrow();
        }

        private void DisableMoveMode()
        {
            isMoveModeOn = false;
            controlBtns.SetActive(false);
            MapScroll.Instance.isScrollActiveAboveUI = true;
            SectorsManager.Instance.EndMovingTroops();

            _currentFrom = null;
            _currentTo = null;
        }
    }
}