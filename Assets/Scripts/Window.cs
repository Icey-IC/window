using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Window : MonoBehaviour, IPointerDownHandler
{
    [Header("窗口组件")]
    public RectTransform windowRect;
    public Button closeButton;

    [Header("窗口缩放")]
    public Vector2 minSize = new Vector2(300f, 200f);

    private Canvas canvas;
    private RectTransform parentRect;
    private WindowManager windowManager;

    private Vector2 dragOffset;

    // 缩放相关
    private Vector2 resizeStartMousePosition;
    private Vector2 resizeStartSize;
    private Vector2 resizeStartPosition;
    private Vector2 resizeStartMouseLocalPosition;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        if (windowRect == null)
        {
            windowRect = GetComponent<RectTransform>();
        }

        parentRect = windowRect.parent as RectTransform;

        windowManager = FindFirstObjectByType<WindowManager>();

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseWindow);
        }
    }

    // =========================
    // 窗口点击
    // =========================

    public void OnPointerDown(PointerEventData eventData)
    {
        BringToFront();
    }

    // =========================
    // 窗口拖动
    // =========================

    public void StartDrag(PointerEventData eventData)
    {
        BringToFront();

        Vector2 localPointerPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            canvas.worldCamera,
            out localPointerPosition))
        {
            dragOffset =
                localPointerPosition -
                (Vector2)windowRect.localPosition;
        }
    }

    public void DragWindow(PointerEventData eventData)
    {
        Vector2 localPointerPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            canvas.worldCamera,
            out localPointerPosition))
        {
            windowRect.localPosition =
                localPointerPosition - dragOffset;
        }
    }

    // =========================
    // 窗口缩放
    // =========================

    public void StartResize(PointerEventData eventData)
    {
        BringToFront();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            eventData.pressEventCamera,
            out resizeStartMouseLocalPosition
        );

        resizeStartSize = windowRect.sizeDelta;
    }

    public void ResizeWindow(PointerEventData eventData)
    {
        Vector2 currentMouseLocalPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            eventData.pressEventCamera,
            out currentMouseLocalPosition
        );

        Vector2 mouseDelta =
            currentMouseLocalPosition - resizeStartMouseLocalPosition;

        Vector2 newSize = resizeStartSize;

        // 右边跟随鼠标
        newSize.x += mouseDelta.x;

        // 下边跟随鼠标
        // 注意 Unity UI Y 轴方向，所以这里要减
        newSize.y -= mouseDelta.y;

        // 最小尺寸限制
        newSize.x = Mathf.Max(newSize.x, minSize.x);
        newSize.y = Mathf.Max(newSize.y, minSize.y);

        // 只改变大小
        windowRect.sizeDelta = newSize;
    }

    // =========================
    // 窗口置顶
    // =========================

    private void BringToFront()
    {
        if (windowManager != null)
        {
            windowManager.BringToFront(this);
        }
        else
        {
            windowRect.SetAsLastSibling();
        }
    }

    // =========================
    // 关闭窗口
    // =========================

    private void CloseWindow()
    {
        if (windowManager != null)
        {
            windowManager.CloseWindow(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}