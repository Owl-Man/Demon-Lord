using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour
{
    [Header("Fading")] 
    
    [Tooltip("Delay between animation steps. The LOWER the value, the FASTER the animation\n \nvalue > 0"), SerializeField] 
    private float fadingSubTime = 0.01f;

    [Tooltip("The value of changing the appearance of an object in one step." +
             " The HIGHER the value, the FASTER the animation." +
             " However, with LARGE values, the animation may not be smooth, but too SHARP" +
             " - the number of frames in the animation will decrease. The lower the value," +
             " the smoother the animation. \n \nIt is recommended to change this value only if" +
             " it is impossible to adjust only the fadingSubTime\n \nvalue > 0"), SerializeField]
    private float fadingStep = 0.01f;

    [Tooltip("If disabled, the animation will be affected by time scaling." +
             " If enabled, the animation will be played completely regardless of the time scale and" +
             " setting fadingSubTime will not work"), SerializeField]
    private bool isFadeAnimByUpdateFrame;
    
    [Header("Scaling")]
    
    [SerializeField] private bool isWithScale;
    [SerializeField] private float scaleStep = 0.02f;
    
    private Image _image;
    private TMP_Text _text;
    private CanvasGroup _canvasGroup;

    private RectTransform _rectTransform;

    private bool _isStop;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        
        TryGetComponent(out _canvasGroup);
        TryGetComponent(out _image);
        TryGetComponent(out _text);
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        if (_canvasGroup != null) StartCoroutine(StartFadedAnim(_canvasGroup));
        else if (_image != null) StartCoroutine(StartFadedAnim(_image));
        else if (_text != null) StartCoroutine(StartFadedAnim(_text));
    }

    private void OnDisable() => Stop();

    public void Stop() => StopAllCoroutines();
    public void StopWithReturn() => _isStop = true;

    public void EndFadeAnimText() => StartCoroutine(EndFadedAnim(_text));

    public void EndFadeAnimImage() => StartCoroutine(EndFadedAnim(_image));

    public void EndFadeAnimCanvasGroup()
    {
        StopCoroutine(EndFadedAnim(_canvasGroup));
        StartCoroutine(EndFadedAnim(_canvasGroup));
    }
    
    private YieldInstruction FadeYield() => isFadeAnimByUpdateFrame ? null : new WaitForSeconds(fadingSubTime);

    private IEnumerator StartFadedAnim(TMP_Text text)
    {
        if (isWithScale) StartCoroutine(UpScaling());
        
        text.alpha = 0;
        
        while (text.alpha < 1)
        {
            if (_isStop)
            {
                text.alpha = 1;
                _isStop = false;
                break;
            }
            
            text.alpha += fadingStep;
            
            yield return FadeYield();
        }
    }

    private IEnumerator StartFadedAnim(Image image)
    {
        if (isWithScale) StartCoroutine(UpScaling());
        
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        
        while (image.color.a < 1)
        {
            if (_isStop)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                _isStop = false;
                break;
            }
            
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + fadingStep);
            
            yield return FadeYield();
        }
    }
    
    private IEnumerator StartFadedAnim(CanvasGroup canvasGroup)
    {
        if (isWithScale) StartCoroutine(UpScaling());
        
        canvasGroup.alpha = 0;
        
        while (canvasGroup.alpha < 1)
        {
            if (_isStop)
            {
                canvasGroup.alpha = 1;
                _isStop = false;
                break;
            }
            
            canvasGroup.alpha += fadingStep;
            
            yield return FadeYield();
        }
    }

    private IEnumerator UpScaling()
    {
        _rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        while (_rectTransform.localScale.x < 1)
        {
            _rectTransform.localScale = new Vector3(_rectTransform.localScale.x + scaleStep, _rectTransform.localScale.y + scaleStep, _rectTransform.localScale.z + scaleStep);
            yield return null;
        }
    }
    
    private IEnumerator DownScaling()
    {
        _rectTransform.localScale = new Vector3(1, 1, 1);

        while (_rectTransform.localScale.x > 0)
        {
            _rectTransform.localScale = new Vector3(_rectTransform.localScale.x - scaleStep, _rectTransform.localScale.y - scaleStep, _rectTransform.localScale.z - scaleStep);
            yield return null;
        }
    }
    
    private IEnumerator EndFadedAnim(TMP_Text text)
    {
        if (isWithScale) StartCoroutine(DownScaling());
        
        while (text.alpha > 0)
        {
            if (_isStop)
            {
                text.alpha = 0;
                _isStop = false;
                break;
            }
            
            text.alpha -= fadingStep;
            
            yield return FadeYield();
        }
        
        gameObject.SetActive(false);
    }
    
    private IEnumerator EndFadedAnim(Image image)
    {
        if (isWithScale) StartCoroutine(DownScaling());
        
        while (image.color.a > 0)
        {
            if (_isStop)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                _isStop = false;
                break;
            }
            
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - fadingStep);
            
            yield return FadeYield();
        }
        
        gameObject.SetActive(false);
    }
    
    private IEnumerator EndFadedAnim(CanvasGroup canvasGroup)
    {
        if (isWithScale) StartCoroutine(DownScaling());
        
        while (canvasGroup.alpha > 0)
        {
            if (_isStop)
            {
                canvasGroup.alpha = 0;
                _isStop = false;
                break;
            }
            
            canvasGroup.alpha -= fadingStep;
            
            yield return FadeYield();
        }
        
        gameObject.SetActive(false);
    }
}