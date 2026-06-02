using System.Collections;
using UnityEngine;

public class AsteroidExplosionParticles : MonoBehaviour
{
    private ParticleSystem system;

    private void OnEnable()
    {
        system = GetComponent<ParticleSystem>();

        StartCoroutine(DestroySelf(system.main.duration));
    }

    private IEnumerator DestroySelf(float time) { 
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
