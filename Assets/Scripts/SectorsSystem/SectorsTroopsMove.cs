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
        
        private Sector from, to;

        public static SectorsTroopsMove Instance;

        private int troops;

        private void Awake() => Instance = this;

        public bool isMoveModeOn { get; private set; }
        
        public void SliderTroopsUpdate() => troopsCount.text = Convert.ToString(troopsSlider.value);

        public void SetDestination(Sector sector)
        {
            to = sector;
            
            //_currentArrow.GetComponent<Arrow>().Activate(from.transform, to.transform);
            
            ArrowCreator.Instance.CreateArrow(from.mainHouse, to.mainHouse);

            acceptBtn.interactable = true;
        }

        public Sector GetFromSector() => from;
        
        public void ActivateMoveMode()
        {
            isMoveModeOn = true;
            controlBtns.SetActive(true);

            SectorsManager.Instance.PrepareForMovingTroops(out from);

            MapScroll.Instance.isScrollActiveAboveUI = false;

            troopsSlider.minValue = 1;
            troopsSlider.maxValue = from.troopsCount;

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
        }

        public void AcceptMove()
        {
            troops = Convert.ToInt32(troopsSlider.value);
            
            ArrowCreator.Instance.MoveTroughArrow(moveDuration);

            DisableMoveMode();
        }

        public void CalculateTroopsMovingResult()
        {
            from.troopsCount -= troops;
            
            if (to.isSectorOccupied)
            {
                to.troopsCount += troops;
            }
            else //Attack
            {
                to.troopsCount -= troops;

                if (to.troopsCount < 0)
                {
                    to.troopsCount *= -1;
                    to.isSectorOccupied = true;
                }
            }
        }
    }
}