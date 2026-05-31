using UnityEngine;

public class ProbePulse : MonoBehaviour
{
    [SerializeField] private float growSpeed;
    public float maxRadius = 30f;

    private float unscaledRadius;
    private float scaleFactor;

    private Vector3 targetScale;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        unscaledRadius = spriteRenderer.sprite.bounds.size.x / 2f;
        scaleFactor = maxRadius / unscaledRadius;
        targetScale = new(scaleFactor, scaleFactor, 0f);
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, growSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.localScale, targetScale) <= 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            Asteroid asteroid = collision.transform.parent.GetComponent<Asteroid>();

            asteroid.RevealAsteroid();
        }
    }
}
