using System;
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

        [SerializeField] private Transform mainCanvas;
        
        [SerializeField] private Button acceptBtn;
        
        private Sector from, to;

        public static SectorsTroopsMove Instance;

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

        public void DisableMoveMode()
        {
            isMoveModeOn = false;
            controlBtns.SetActive(false);
            MapScroll.Instance.isScrollActiveAboveUI = true;
            SectorsManager.Instance.EndMovingTroops();
            ArrowCreator.Instance.DestroyArrow();
        }

        public void AcceptMove()
        {
            int troops = Convert.ToInt32(troopsSlider.value);

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

            DisableMoveMode();
        }
    }
}