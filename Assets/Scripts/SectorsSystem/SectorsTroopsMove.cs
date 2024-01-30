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
        [SerializeField] private Button acceptBtn;

        private Sector from, to;

        public static SectorsTroopsMove Instance;

        private void Awake() => Instance = this;

        public bool isMoveModeOn { get; private set; }
        
        public void SliderTroopsUpdate() => troopsCount.text = Convert.ToString(troopsSlider.value);

        public void SetDestination(Sector sector)
        {
            to = sector;
            acceptBtn.interactable = true;
        }
        
        public void ActivateMoveMode()
        {
            isMoveModeOn = true;
            controlBtns.SetActive(true);

            SectorsManager.Instance.PrepareForMovingTroops(out from);

            troopsSlider.minValue = 1;
            troopsSlider.maxValue = from.troopsCount;

            acceptBtn.interactable = false;
        }

        public void DisableMoveMode()
        {
            isMoveModeOn = false;
            controlBtns.SetActive(false);
            SectorsManager.Instance.EndMovingTroops();
        }

        public void AcceptMove()
        {
            ushort troops = Convert.ToUInt16(troopsSlider.value);
            from.troopsCount -= troops;
            to.troopsCount += troops;
            
            DisableMoveMode();
        }
    }
}