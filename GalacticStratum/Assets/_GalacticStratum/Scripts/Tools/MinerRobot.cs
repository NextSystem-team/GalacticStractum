using System.Collections;
using UnityEngine;

public class MinerRobot : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeToMine;

    public Player player;
    public Asteroid asteroid;

    public int waterAmount;
    public int beskariumAmount;
    public int whitlockiteAmount;
    public int lechatelieriteAmount;
    public int elaliiteAmount;

    private bool isReturning = false;

    private AudioSource audioSorce;

    private void Start()
    {
        audioSorce = GetComponent<AudioSource>();

        if (asteroid != null)
        {
            StartCoroutine(Mine());
        }
        else
        {
            Debug.LogError("Sem asteróide para minerar");
            isReturning = true;
        }
    }

    private void Update()
    {
        if (isReturning)
        {
            if (audioSorce.clip != AudioManager.Instance.GetSound("MinerRobotReturn"))
            {
                audioSorce.clip = AudioManager.Instance.GetSound("MinerRobotReturn");
                audioSorce.Play();
            }

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.up = transform.position - player.transform.position;
        }
    }

    private IEnumerator Mine()
    {
        yield return new WaitForSeconds(timeToMine);

        if (asteroid == null)
        {
            isReturning = true;
            yield break;
        }

        AsteroidData currentData = asteroid.data;

        if (currentData.WaterAmount > 0)
        {
            currentData.TakeResource(AsteroidData.ResourceType.Water);
            StorageResource(AsteroidData.ResourceType.Water);
            StartCoroutine(Mine());
        }
        else
        {
            AsteroidData.ResourceType randomResource;

            if (currentData.ResourcesQuantity > 0)
            {
                while (true)
                {
                    randomResource = (AsteroidData.ResourceType)Random.Range(2, (int)AsteroidData.ResourceType.Count);
                    if (!currentData.CheckIfResourceDepleted(randomResource))
                    {
                        break;
                    }
                }

                currentData.TakeResource(randomResource);
                StorageResource(randomResource);

                StartCoroutine(Mine());
            }
            else
            {
                asteroid.Explode();
                asteroid = null;
                isReturning = true;
            }
        }
    }

    private void StorageResource(AsteroidData.ResourceType resourceType)
    {
        switch (resourceType)
        {
            case AsteroidData.ResourceType.Water:
                waterAmount++;
                break;
            case AsteroidData.ResourceType.Whitlockite:
                whitlockiteAmount++;
                break;
            case AsteroidData.ResourceType.Beskarium:
                beskariumAmount++;
                break;
            case AsteroidData.ResourceType.Elaliite:
                elaliiteAmount++;
                break;
            case AsteroidData.ResourceType.Lechatelierite:
                lechatelieriteAmount++;
                break;
            default:
                break;
        }

    }
}
