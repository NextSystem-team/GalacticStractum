using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Scriptable Objects/Tools/Tool")]
public class ToolData : ScriptableObject
{
    public enum ToolType
    {
        All,
        Locator,
        Miner,
        Other
    }

    [Header("Tool Information")]
    [SerializeField] private string toolID;
    [SerializeField] private ToolType toolType;
    [SerializeField] private string toolName;
    [SerializeField] private string toolDescription;
    [SerializeField] private Sprite toolIcon;

    [Header("Tool Properties")]
    [SerializeField] private int toolPrice;
    [SerializeField] private int toolUsePrice;
    [SerializeField] private _ToolObject tool;

    public string ToolID => toolID;
    public ToolType Type => toolType;
    public string Name => toolName;
    public string Description => toolDescription;
    public Sprite Icon => toolIcon;
    public int Price => toolPrice;
    public int UsePrice => toolUsePrice;
    public _ToolObject Tool => tool;
}
