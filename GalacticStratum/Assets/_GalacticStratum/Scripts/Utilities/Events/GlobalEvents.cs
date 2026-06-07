using UnityEngine;
using System;

public static class GlobalEvents
{
    public static Action<ToolData> OnToolSelected;
    public static Action<AsteroidData> CreateReport;

    public static Action ToggleSettings;
    public static Action<bool> NotifySettingsToggle;

    public static Action EndMission;
}
