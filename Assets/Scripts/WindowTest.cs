using UnityEngine;

public class WindowTest : MonoBehaviour
{
    public WindowManager windowManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            windowManager.OpenWindow();
        }
    }
}