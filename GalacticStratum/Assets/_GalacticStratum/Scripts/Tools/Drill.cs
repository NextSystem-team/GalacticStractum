using System.Collections;
using UnityEngine;

public class Drill : MonoBehaviour
{
    [Header("Rope Settings")]
    [SerializeField] private LineRenderer rope;
    public Player startRopePoint;
    public float ropeLength;

    [Header("Drill Settings")]
    [SerializeField] private float drillSpeed;
    [SerializeField] private float timeToExtractEachResource;
    public Vector2 targetPosition;

    public bool canMove = false;

    private Asteroid currentAsteroid;
    private AsteroidData currentData;

    private void Update()
    {
        rope.SetPosition(0, startRopePoint.transform.position);
        rope.SetPosition(1, transform.position);

        if (!canMove) return;

        // Move the drill towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, drillSpeed * Time.deltaTime);
        transform.up = transform.position - startRopePoint.transform.position;

        if (Vector2.Distance(rope.GetPosition(0), rope.GetPosition(1)) > ropeLength)
        {
            Destroy(gameObject);
        }

        if ((Vector2)transform.position == targetPosition && currentAsteroid == null)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Mine()
    {
        yield return new WaitForSeconds(timeToExtractEachResource);

        if (currentData.WaterAmount > 0)
        {
            currentData.TakeResource(AsteroidData.ResourceType.Water);
            print("Extracted Water");
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
                print($"Extracted {randomResource}");

                StartCoroutine(Mine());
            }
            else
            {
                currentAsteroid.Explode();
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            drillSpeed = 0;
            targetPosition = transform.position;
            currentAsteroid = collision.transform.parent.GetComponent<Asteroid>();
            currentData = currentAsteroid.data;

            StartCoroutine(Mine());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ropeLength);
    }
}
