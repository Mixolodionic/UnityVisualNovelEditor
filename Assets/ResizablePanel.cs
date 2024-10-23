using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResizablePanel : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [Header("Panel Settings")]
    [SerializeField] private LayoutElement targetPanelLayout;
    [SerializeField] private RectTransform targetPanelRect;

    [Header("Resizing Type")]
    [SerializeField] private bool isHorizontalResize = true;
    [SerializeField] private bool isLeftPanel = true;
    [SerializeField] private bool isTopPanel = false;

    [Header("Constraints")]
    [SerializeField] private float minLimit = 100f;
    [SerializeField] private float maxLimit = 250f;

    private Vector2 initialMousePosition;
    private float initialPanelSize;

    public void OnPointerDown(PointerEventData eventData)
    {
        initialMousePosition = eventData.position;
        initialPanelSize = isHorizontalResize ? targetPanelRect.rect.width : targetPanelRect.rect.height;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float delta = isHorizontalResize
            ? eventData.position.x - initialMousePosition.x
            : initialMousePosition.y - eventData.position.y;

        float newSize = (isHorizontalResize && isLeftPanel) || (isTopPanel)
            ? initialPanelSize + delta
            : initialPanelSize - delta;

        newSize = Mathf.Clamp(newSize, minLimit, maxLimit);

        if (isHorizontalResize)
        {
            targetPanelLayout.minWidth = newSize;
        }
        else
        {
            targetPanelLayout.minHeight = newSize;
        }
    }
}
