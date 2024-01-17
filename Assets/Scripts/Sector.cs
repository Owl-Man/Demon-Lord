using UnityEngine;

public class Sector : MonoBehaviour
{
    [SerializeField] private ushort armyCount;

    private Camera _camera;
    private ObjectHighlight _outline;

    private void Start()
    {
        _camera = Camera.main;
        _outline = GetComponent<ObjectHighlight>();
        
        _outline.DisableHighlight();
    }

    public void OnSelectSector()
    {
        if (_outline.IsEnabled)
        {
            _outline.DisableHighlight();
        }
        else
        {
            _outline.EnableHighlight();
        }
    }
    
    private void Update() 
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = _camera.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            if (hit.collider.gameObject == gameObject)
            {
                OnSelectSector();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 touchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            
            if (hit.collider.gameObject == gameObject)
            {
                OnSelectSector();
            }
        }
    }
}