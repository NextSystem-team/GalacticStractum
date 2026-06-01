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

    private void Start()
    {
        fuelAmount = maxFuel;
        StartCoroutine(LossFuel(fuelLossBySecond));
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
        whitlockiteAmount += robot.whitlockiteAmount;
        lechatelieriteAmount += robot.lechatelieriteAmount;
        elaliiteAmount += robot.elaliiteAmount;
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
            print("Cabô o combustível");
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
                break;
            case AsteroidData.ResourceType.Whitlockite:
                whitlockiteAmount++;
                break;
            case AsteroidData.  ResourceType.Lechatelierite:
                lechatelieriteAmount++;
                break;
            case AsteroidData.ResourceType.Elaliite:
                elaliiteAmount++;
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
