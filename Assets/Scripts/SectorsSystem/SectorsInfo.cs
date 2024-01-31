using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SectorsSystem
{
    public class SectorsInfo : MonoBehaviour
    {
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private TMP_Text info;
        
        [SerializeField] private Button moveTroopsBtn;

        public void Show(int troopsCount)
        {
            infoPanel.SetActive(true);
            info.text = troopsCount.ToString();

            moveTroopsBtn.interactable = troopsCount != 0;
        }

        public void Close()
        {
            infoPanel.GetComponent<FadeAnimation>().EndFadeAnimCanvasGroup();
        }
    }
}