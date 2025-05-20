using System.Collections;
using UnityEngine;
public class Lightning : MonoBehaviour
{
    public Light lightningLight;
    public AudioSource thunderSound;
    public LightningBolt lightningBolt;
    public Transform lightningStartPoint;
    public Transform lightningEndPoint;

    public float minDelay = 3f;
    public float maxDelay = 10f;

    void Start()
    {
        StartCoroutine(GenerateLightning());
    }

    IEnumerator GenerateLightning()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            StartCoroutine(FlashLightning());

            if (thunderSound != null)
                StartCoroutine(PlayThunderSound(Random.Range(0.3f, 1.5f)));

            if (lightningBolt != null)
                lightningBolt.Strike(lightningStartPoint.position, lightningEndPoint.position);
        }
    }

    IEnumerator FlashLightning()
    {
        lightningLight.intensity = 8f;
        yield return new WaitForSeconds(0.05f);
        lightningLight.intensity = 0f;
        yield return new WaitForSeconds(0.1f);
        lightningLight.intensity = 6f;
        yield return new WaitForSeconds(0.05f);
        lightningLight.intensity = 0f;
    }

    IEnumerator PlayThunderSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        thunderSound.Play();
    }
}
