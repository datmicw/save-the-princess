using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    void OnGUI()
    {
        int fps = (int)(1f / Time.unscaledDeltaTime);
        GUI.Label(new Rect(10, 10, 100, 20), "FPS: " + fps);
    }
}
