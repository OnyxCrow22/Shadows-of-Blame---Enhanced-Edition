using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 15f;
    public float force = 700;
    public float grenadeDamage = 100;
    public GameObject explosionVFX;
    public GameObject grenade;
    public bool hasExploded = false;
    public AudioSource explosion;
    EnemyHealth damage;
    NPCHealth NPCs;

    Collider[] cols;

    [SerializeField] float countdown;

    void Start()
    {
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    public void Explode()
    {
        // Instantiate(explosionVFX, grenade.transform.position, grenade.transform.rotation);

        GameObject newGrenade = ObjectPool.oPInstance.GetPooledObject();

        if (newGrenade != null)
        {
            newGrenade.transform.position = grenade.transform.position;
            newGrenade.SetActive(true);
        }
        
        cols = new Collider[10];
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, radius, cols);

        explosion.Play();

        foreach (Collider nearbyObject in cols)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

            damage = nearbyObject.GetComponent<EnemyHealth>();
            if (damage != null)
            {
                damage.LoseHealth(damage.healthLoss + grenadeDamage);
            }

            NPCs = nearbyObject.GetComponent<NPCHealth>();
            NPCs.LoseHealth(NPCs.healthLoss + grenadeDamage);
        }
        gameObject.SetActive(false);
    }
}

