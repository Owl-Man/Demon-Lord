using TMPro;
using UnityEngine;

namespace SectorsSystem
{
    public class SectorsInfo : MonoBehaviour
    {
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private TMP_Text info;

        public void Show(string text)
        {
            infoPanel.SetActive(true);
            info.text = text;
        }

        public void Close()
        {
            infoPanel.GetComponent<FadeAnimation>().EndFadeAnimCanvasGroup();
        }
    }
}