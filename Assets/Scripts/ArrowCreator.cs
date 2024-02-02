using UnityEngine;

public class ArrowCreator : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;

    private Transform toObject, fromObject;
    
    private LineRenderer _lineRenderer;
    private GameObject _arrow;

    public static ArrowCreator Instance;

    private void Awake() => Instance = this;

    public void CreateArrow(Transform from, Transform to)
    {
        DestroyArrow();
        
        toObject = to;
        fromObject = from;
        
        _arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        _arrow.transform.SetParent(transform);
        _arrow.transform.position = to.position;
        _arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, toObject.position - fromObject.position);
        
        _lineRenderer = _arrow.GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0.08f;
        _lineRenderer.endWidth = 0.08f;
        _lineRenderer.sortingOrder = 11;
    }

    public void DestroyArrow()
    {
        if (_arrow != null) Destroy(_arrow);
    }
    
    private void Update()
    {
        if (_lineRenderer == null) return;
        
        _lineRenderer.SetPosition(0, fromObject.position);
        _lineRenderer.SetPosition(1, toObject.position);

        _arrow.transform.position = toObject.position;
        _arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, toObject.position - fromObject.position);
    }
}