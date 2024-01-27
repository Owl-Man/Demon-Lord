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
        if (_outline.isEnabled)
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
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 touchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            
            if (hit.collider == null) return;
            
            if (hit.collider.gameObject == gameObject)
            {
                OnSelectSector();
            }
        }
    }
}