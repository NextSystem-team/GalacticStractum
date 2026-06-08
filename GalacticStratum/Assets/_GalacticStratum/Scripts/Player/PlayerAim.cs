using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Player player;
    public float aimZoneRadius;

    public _ToolObject currentTool;
    public ToolData currentToolData;

    [SerializeField] private InputActionReference leftClickInput;
    [SerializeField] private InputActionReference rightClickInput;

    private Camera mainCamera;

    private SpriteRenderer aimZoneRenderer;

    private void OnEnable()
    {
        if (leftClickInput != null) leftClickInput.action.Enable();
        if (rightClickInput != null) rightClickInput.action.Enable();

        GlobalEvents.OnToolSelected += EquipTool;
    }

    private void OnDisable()
    {
        if (leftClickInput != null) leftClickInput.action.Disable();
        if (rightClickInput != null) rightClickInput.action.Disable();

        GlobalEvents.OnToolSelected -= EquipTool;
    }

    private void Start()
    {
        aimZoneRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;

        ApplyClick();
    }

    private void ApplyClick()
    {
        if (currentTool != null && rightClickInput.action.WasPressedThisFrame())
        {
            TurnOffAim();
            SetDefaultCursor();
            currentTool = null;
        }


        if (currentTool != null && leftClickInput.action.WasPressedThisFrame())
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Vector2 mouseScreenPosition = Pointer.current.position.ReadValue();
            Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
            float clickDistance = Vector2.Distance(mouseWorldPosition, transform.position);

            if (SaveManager.currentPlayerData.moneyAmount >= currentToolData.UsePrice)
            {
                if (currentTool != null && currentTool.UseAim && clickDistance > aimZoneRadius)
                {
                    PopUpCanva.Instance.SpawnAlertPopUp(mouseWorldPosition, PopUpCanva.NO_RANGE);
                    return;
                }

                if (currentTool.OnUse(mouseWorldPosition, player))
                {
                    SaveManager.currentPlayerData.moneyAmount -= currentToolData.UsePrice;
                }
            }
            else
            {
                PopUpCanva.Instance.SpawnAlertPopUp(mouseWorldPosition, PopUpCanva.CANT_PAY_TOOL);
            }
        }
    }

    private void AdjustAimZoneSize()
    {
        if (aimZoneRenderer != null)
        {
            float unscaledWidth = aimZoneRenderer.sprite.bounds.size.x;
            float unscaledRadius = unscaledWidth / 2f;
            float scaleFactor = aimZoneRadius / unscaledRadius;
            transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        }
    }

    public void TurnOffAim()
    {
        aimZoneRenderer.enabled = false;
    }

    public void TurnOnAim()
    {
        aimZoneRenderer.enabled = true;
        AdjustAimZoneSize();
    }

    private void EquipTool(ToolData tool)
    {
        currentTool = tool.Tool;
        currentToolData = tool;

        if (currentTool != null && currentTool.UseAim)
        {
            aimZoneRadius = currentTool.AimRadius;
            AdjustAimZoneSize();
            TurnOnAim();
        }
        else
        {
            TurnOffAim();
        }
    }

    private void SetDefaultCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        CursorSpriteManager.currentCursorSprite = null;
        CursorSpriteManager.currentCursorHotspot = Vector2.zero;
    }
}
