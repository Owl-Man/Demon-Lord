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

        public Sector from, to;

        public static SectorsTroopsMove Instance;

        private void Awake() => Instance = this;

        public bool isMoveModeOn { get; private set; }
        
        public void SliderArmyUpdate() => troopsCount.text = Convert.ToString(troopsSlider.value);

        public void ActivateMoveMode()
        {
            isMoveModeOn = true;
            controlBtns.SetActive(true);

            troopsSlider.minValue = 1;
            troopsSlider.maxValue = from.troopsCount;
        }

        public void CancelTheMove()
        {
            isMoveModeOn = false;
            controlBtns.SetActive(false);
        }

        public void AcceptMove()
        {
            ushort troops = Convert.ToUInt16(troopsSlider.value);
            from.troopsCount -= troops;
            to.troopsCount += troops;
        }
    }
}