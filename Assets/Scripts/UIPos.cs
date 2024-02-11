using UnityEngine;

public class UIPos : MonoBehaviour
{
    public GameObject targetObject; // Объект, координаты которого будут использованы для позиционирования UI элемента
    private RectTransform _uiElement; // UI элемент, который нужно переместить

    private Camera _camera;

    private void Start()
    {
        _uiElement = GetComponent<RectTransform>();
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 screenPos = _camera.WorldToScreenPoint(targetObject.transform.position);
        _uiElement.position = screenPos;
    }
}