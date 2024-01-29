using UnityEngine;

public class Sector : MonoBehaviour
{
    [SerializeField] private ushort armyCount;
    [SerializeField] private bool isSectorOccupied;
    
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
        SectorsManager.Instance.ChoseTheSector(this);
    }

    public void EnableSectorInteraction()
    {
        _outline.EnableHighlight();
    }

    public void DisableSectorInteraction()
    {
        _outline.DisableHighlight();
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