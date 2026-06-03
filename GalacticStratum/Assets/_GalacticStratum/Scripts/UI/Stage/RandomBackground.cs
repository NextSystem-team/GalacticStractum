using UnityEngine;

public class RandomBackground : MonoBehaviour
{
    [SerializeField] private Material[] backgroundMaterials;

    private void Start()
    {
        if (backgroundMaterials.Length > 0)
        {
            int randomIndex = Random.Range(0, backgroundMaterials.Length);
            GetComponent<SpriteRenderer>().material = backgroundMaterials[randomIndex];
        }
    }
}
