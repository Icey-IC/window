using UnityEngine;
using UnityEngine.EventSystems;

public class WindowResizeHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Window window;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (window != null)
        {
            window.StartResize(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (window != null)
        {
            window.ResizeWindow(eventData);
        }
    }
}