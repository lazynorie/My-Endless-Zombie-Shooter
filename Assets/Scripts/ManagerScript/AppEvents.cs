using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvents
{
    public delegate void MouseCursorEnableEvent(bool enabled);

    public static event MouseCursorEnableEvent MouseCursorEnabled;

    public static void InvokeMousecursorEnable(bool enabled)
    {
        MouseCursorEnabled?.Invoke(enabled);
    }
}
