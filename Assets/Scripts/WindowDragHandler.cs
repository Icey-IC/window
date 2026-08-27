using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Window window;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (window != null)
        {
            window.StartDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (window != null)
        {
            window.DragWindow(eventData);
        }
    }
}
