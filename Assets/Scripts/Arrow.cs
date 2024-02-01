using UnityEngine;

public class Arrow : MonoBehaviour
{
	[SerializeField] private float baseHeight;
	[SerializeField] private RectTransform baseRect;
	[SerializeField] private Transform from, to;
	[SerializeField] private bool startsActive;

	private RectTransform myRect;
	private Canvas canvas;
	private Camera mainCamera;
	private bool isActive;

	private void Awake()
	{
		myRect = (RectTransform)transform;
		canvas = GetComponentInParent<Canvas>();
		mainCamera = Camera.main;
		SetActive(startsActive);
	}

	private void Update ()
	{
		if (!isActive)
			return;
		Setup();
	}

	private void Setup ()
	{
		if (from == null)
			return;
		Vector2 fromPosOnScreen = mainCamera.WorldToScreenPoint(from.position);
		Vector2 toPosOnScreen = mainCamera.WorldToScreenPoint(to.position);
			
		myRect.anchoredPosition = new Vector2(fromPosOnScreen.x - Screen.width / 2, fromPosOnScreen.y - Screen.height / 2) / canvas.scaleFactor;
		Vector2 difference = toPosOnScreen - fromPosOnScreen;
		difference.Scale(new Vector2(1f / myRect.localScale.x, 1f / myRect.localScale.y));
		transform.up = difference;
		baseRect.anchorMax = new Vector2(baseRect.anchorMax.x, difference.magnitude / canvas.scaleFactor / baseHeight);
	}

	private void SetActive (bool b)
	{
		isActive = b;
		if (b)
			Setup();
		baseRect.gameObject.SetActive(b);
	}

	public void Activate(Transform setFrom, Transform setTo)
	{
		from = setFrom;
		to = setTo;
			
		SetActive(true);
	}

	public void Deactivate () => SetActive(false);
}