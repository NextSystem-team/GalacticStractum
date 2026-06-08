using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Scriptable Objects/Tools/Tool Brain/Miner Robot")]
public class MinerRobotObject : _ToolObject
{
    public override bool UseAim => false;
    public override float AimRadius => 0f;

    [SerializeField] private GameObject minerRobotPrefab;
    [SerializeField] private RenderTexture persistentMap;

    private Camera mapCamera;
    private RaycastHit2D hit;

    public override bool OnUse(Vector2 targetPosition, Player player)
    {
        hit = Physics2D.Raycast(targetPosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Asteroid"))
            {
                if (CheckIfAsteroidIsRevealed(hit.point))
                {
                    Asteroid asteroid = hit.collider.GetComponent<Asteroid>();
                    MinerRobot robot = Instantiate(minerRobotPrefab, hit.collider.transform.position, Quaternion.identity).GetComponent<MinerRobot>();
                    robot.asteroid = asteroid;
                    robot.player = player;
                    return true;
                }
                else
                {
                    PopUpCanva.Instance.SpawnAlertPopUp(targetPosition, PopUpCanva.NO_DISCOVERED_OBJECT);
                }
            }
            else
            {
                PopUpCanva.Instance.SpawnAlertPopUp(targetPosition, PopUpCanva.NO_ASTEROID);
            }
        }
        else
        {
            PopUpCanva.Instance.SpawnAlertPopUp(targetPosition, PopUpCanva.NO_DISCOVERED_OBJECT);
        }

        return false;
    }

    private bool CheckIfAsteroidIsRevealed(Vector2 clickedPosition)
    {
        if (mapCamera == null)
        {
            mapCamera = GameObject.FindGameObjectWithTag("MapCamera").GetComponent<Camera>();
        }

        Vector3 viewportPosition = mapCamera.WorldToViewportPoint(clickedPosition);

        int pixelX = Mathf.FloorToInt(viewportPosition.x * persistentMap.width);
        int pixelY = Mathf.FloorToInt(viewportPosition.y * persistentMap.height);

        Texture2D pickedPixel = new(1, 1, TextureFormat.RGBA32, false);

        RenderTexture.active = persistentMap;
        pickedPixel.ReadPixels(new Rect(pixelX, pixelY, 1, 1), 0, 0);
        pickedPixel.Apply();
        RenderTexture.active = null;

        Color colorAtPosition = pickedPixel.GetPixel(0, 0);

        Destroy(pickedPixel);

        return colorAtPosition.r != 0;
    }
}
