using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Dados do Asteroide")]
    [SerializeField] private GameObject brush;
    public AsteroidData data;

    [Header("Banco de dados das sprites de Asteroide")]
    [SerializeField] private AsteroidVisualData spritesDatabase;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer brushRenderer;

    void Start()
    {
        brushRenderer = brush.GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        brush.SetActive(true);
        brushRenderer.enabled = false;

        AsteroidVisualData.AsteroidVisual visual = spritesDatabase.GetRandomSprite();
        spriteRenderer.sprite = visual.Sprite;
        brushRenderer.sprite = visual.BrushSprite;

        transform.localScale = Vector3.one * Random.Range(data.Size.MinSize, data.Size.MaxSize);
    }

    public void RevealAsteroid()
    {
        //Colocar efeitos visuais e sonoros aqui...
        brush.SetActive(true);
        brush.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Explode()
    {
        //Colocar efeitos visuais e sonoros aqui...

        Destroy(gameObject);
    }
}
