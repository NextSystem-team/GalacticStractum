using System.Collections;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    [SerializeField] private int maxFuel;
    [SerializeField] private float fuelLossBySecond;

    public float fuelAmount;
    public int beskariumAmount;
    public int whitlockiteAmount;
    public int lechatelieriteAmount;
    public int elaliiteAmount;

    public int MaxFuel => maxFuel;

    private FuelAmountDisplay fuelAmountDisplay;

    private void Start()
    {
        fuelAmount = maxFuel;
        StartCoroutine(LossFuel(fuelLossBySecond));

        fuelAmountDisplay = FindFirstObjectByType<FuelAmountDisplay>();
        fuelAmountDisplay.playerStorage = this;
    }

    public void CollectMinerRobotResources(MinerRobot robot)
    {
        if (fuelAmount + robot.waterAmount <= maxFuel)
        {
            fuelAmount += robot.waterAmount;
        }
        else
        {
            fuelAmount = maxFuel;
        }

        beskariumAmount += robot.beskariumAmount;
        SaveManager.currentPlayerData.beskariumAmount += robot.beskariumAmount;

        whitlockiteAmount += robot.whitlockiteAmount;
        SaveManager.currentPlayerData.whitlockiteAmount += robot.whitlockiteAmount;

        lechatelieriteAmount += robot.lechatelieriteAmount;
        SaveManager.currentPlayerData.lechatelieriteAmount += robot.lechatelieriteAmount;

        elaliiteAmount += robot.elaliiteAmount;
        SaveManager.currentPlayerData.elaliiteAmount += robot.elaliiteAmount;
    }

    private IEnumerator LossFuel(float fuelLoss)
    {
        yield return new WaitForSeconds(1f);
        fuelAmount -= fuelLoss;

        if (fuelAmount > 0)
        {
            StartCoroutine(LossFuel(fuelLoss));
        }
        else
        {
            GlobalEvents.EndMission?.Invoke();
        }
    }

    public void AddResource(AsteroidData.ResourceType resourceType)
    {
        switch (resourceType)
        {
            case AsteroidData.ResourceType.Water:
                fuelAmount++;
                break;
            case AsteroidData.ResourceType.Beskarium:
                beskariumAmount++;
                SaveManager.currentPlayerData.beskariumAmount++;
                break;
            case AsteroidData.ResourceType.Whitlockite:
                whitlockiteAmount++;
                SaveManager.currentPlayerData.whitlockiteAmount++;
                break;
            case AsteroidData.  ResourceType.Lechatelierite:
                lechatelieriteAmount++;
                SaveManager.currentPlayerData.lechatelieriteAmount++;
                break;
            case AsteroidData.ResourceType.Elaliite:
                elaliiteAmount++;
                SaveManager.currentPlayerData.elaliiteAmount++;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MinerRobot"))
        {
            CollectMinerRobotResources(collision.GetComponent<MinerRobot>());
            Destroy(collision.gameObject);
        }
    }
}
