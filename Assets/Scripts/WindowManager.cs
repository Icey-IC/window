using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [Header("窗口设置")]
    [SerializeField] private Transform windowLayer;
    [SerializeField] private GameObject windowPrefab;

    // 当前打开的所有窗口
    private readonly List<Window> windows = new List<Window>();

    /// <summary>
    /// 创建一个窗口
    /// </summary>
    public Window OpenWindow()
    {
        if (windowPrefab == null)
        {
            Debug.LogError("WindowManager：没有设置 Window Prefab！");
            return null;
        }

        if (windowLayer == null)
        {
            Debug.LogError("WindowManager：没有设置 Window Layer！");
            return null;
        }

        GameObject windowObject = Instantiate(
            windowPrefab,
            windowLayer
        );

        Window window = windowObject.GetComponent<Window>();

        if (window == null)
        {
            Debug.LogError("WindowManager：Window Prefab 上没有 Window.cs！");
            Destroy(windowObject);
            return null;
        }

        windows.Add(window);

        BringToFront(window);

        return window;
    }

    /// <summary>
    /// 关闭指定窗口
    /// </summary>
    public void CloseWindow(Window window)
    {
        if (window == null)
            return;

        if (windows.Contains(window))
        {
            windows.Remove(window);
        }

        Destroy(window.gameObject);
    }

    /// <summary>
    /// 将窗口移动到最上层
    /// </summary>
    public void BringToFront(Window window)
    {
        if (window == null)
            return;

        if (!windows.Contains(window))
        {
            windows.Add(window);
        }

        window.transform.SetAsLastSibling();
    }

    /// <summary>
    /// 获取当前所有打开的窗口
    /// </summary>
    public List<Window> GetOpenWindows()
    {
        return windows;
    }
}