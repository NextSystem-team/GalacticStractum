using UnityEngine;

public class PopUpCanva : MonoBehaviour
{
    public static PopUpCanva Instance { get; private set; }

    [SerializeField] private GameObject popUpPrefab;

    public const string CANT_PAY_TOOL = "You can't pay for this!";
    public const string NO_DISCOVERED_OBJECT = "There's nothing revealed here!";
    public const string NO_ASTEROID = "There's no asteroid here!";
    public const string NO_RANGE = "That tool can't reach here!";

    private void Start()
    {
        Instance = this;
    }

    public void SpawnAlertPopUp(Vector2 position, string text)
    {
        GameObject popUp = Instantiate(popUpPrefab, position, Quaternion.identity, transform);
        PopUpText popUpText = popUp.GetComponent<PopUpText>();
        popUpText.Setup(text);
    }

    public void SpawnResourcePopUp(Vector2 position, AsteroidData.ResourceType resource)
    {
        GameObject popUp = Instantiate(popUpPrefab, position, Quaternion.identity, transform);
        PopUpText popUpText = popUp.GetComponent<PopUpText>();
        popUpText.Setup($"+{resource}");
    }
}
