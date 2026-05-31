using UnityEngine;
using UnityEngine.UI;

public class ToolButton : MonoBehaviour
{
    public ToolData tool;
    [SerializeField] private Image icon;

    private void Start()
    {
        icon.sprite = tool.Icon;

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GlobalEvents.OnToolSelected?.Invoke(tool.Tool);
    }
}
