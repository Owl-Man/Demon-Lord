using UnityEngine;
using UnityEngine.EventSystems;

public class MapScroll : MonoBehaviour
{
    public float desensitization = 13;
    
    public float rightMaxPosition = 10f;
    public float leftMaxPosition = 10f;
    
    public float topMaxPosition = 10f;
    public float downMaxPosition = 10f;
    
    private Vector3 _initialMousePosition;
    private Vector3 _initialObjectPosition;

    private EventSystem _eventSystem;

    private void Start() => _eventSystem = EventSystem.current;

    private void Update()
    {
        //if (_eventSystem.IsPointerOverGameObject()) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            _initialMousePosition = Input.mousePosition;
            _initialObjectPosition = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float deltaX = (Input.mousePosition.x - _initialMousePosition.x) / desensitization;
            float deltaY = (Input.mousePosition.y - _initialMousePosition.y) / desensitization;
            
            float newPositionX = _initialObjectPosition.x + deltaX;
            float newPositionY = _initialObjectPosition.y + deltaY;
            
            newPositionX = Mathf.Clamp(newPositionX, rightMaxPosition * -1, leftMaxPosition);
            newPositionY = Mathf.Clamp(newPositionY, topMaxPosition * -1, downMaxPosition);
            
            transform.position = new Vector3(newPositionX, newPositionY, _initialObjectPosition.z);
        }
    }
}