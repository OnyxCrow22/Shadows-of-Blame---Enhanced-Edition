using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    public int segments = 10;
    public float jaggedness = 1.5f;
    public float boltDuration = 0.1f;

    private LineRenderer lineRenderer;
    private float timer = 0f;
    private bool isActive = false;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments;
        lineRenderer.enabled = false;
    }

    public void Strike(Vector3 start, Vector3 end)
    {
        Vector3 direction = (end - start) / (segments - 1);
        lineRenderer.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            Vector3 offset = Vector3.zero;
            if (i != 0 && i != segments - 1)
            {
                offset = new Vector3(
                    Random.Range(-jaggedness, jaggedness),
                    Random.Range(-jaggedness, jaggedness),
                    Random.Range(-jaggedness, jaggedness)
                );
            }

            lineRenderer.SetPosition(i, start + direction * i + offset);
        }

        lineRenderer.enabled = true;
        timer = boltDuration;
        isActive = true;
    }

    void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                lineRenderer.enabled = false;
                isActive = false;
            }
        }
    }
}
