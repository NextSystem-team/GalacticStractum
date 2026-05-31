using UnityEngine;
using System;

public static class GlobalEvents
{
    public static Action<_ToolObject> OnToolSelected;
    public static Action<AsteroidData> CreateReport;
}
