using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Player player;
    public float aimZoneRadius;
    public bool isAiming;

    public _ToolObject currentTool;

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
        if (isAiming)
        {
            ApplyClick();
        }
    }

    private void ApplyClick()
    {
        if (rightClickInput.action.WasPressedThisFrame())
        {
            TurnOffAim();
            currentTool = null;
        }

        if (leftClickInput.action.WasPressedThisFrame())
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Vector2 mouseScreenPosition = Pointer.current.position.ReadValue();
            Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
            float clickDistance = Vector2.Distance(mouseWorldPosition, transform.position);

            print(mouseScreenPosition);

            if (currentTool != null && currentTool.UseAim && clickDistance > aimZoneRadius)
            {
                return;
            }

            currentTool.OnUse(mouseWorldPosition, player);
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
        isAiming = false;
        aimZoneRenderer.enabled = false;
    }

    public void TurnOnAim()
    {
        isAiming = true;
        aimZoneRenderer.enabled = true;
        AdjustAimZoneSize();
    }

    private void EquipTool(_ToolObject tool)
    {
        currentTool = tool;

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
}
