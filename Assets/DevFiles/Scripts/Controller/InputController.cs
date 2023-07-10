using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoSingleton<InputController> , IDragHandler
{
    public float Horizontal => input.x;
    public float Vertical => input.y;

    [SerializeField] private RectTransform _touchPoint = null;
    [SerializeField] private float moveThreshold = 1;

    private Canvas canvas;
    private Camera cam;
    private Vector2 input = Vector2.up;

    private void Start()
    {
        canvas = GetComponent<Canvas>();

        Vector2 center = new Vector2(0.5f, 0.5f);
        _touchPoint.pivot = center;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, _touchPoint.position);
        Vector2 radius = _touchPoint.sizeDelta / 2;
        input = (eventData.position - position) / (radius * canvas.scaleFactor);
        HandleInput(input.magnitude, input.normalized, radius);
    }

    private void HandleInput(float magnitude, Vector2 normalised, Vector2 radius)
    {
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            _touchPoint.anchoredPosition += difference;
        }
       
    }
}