using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPolice : MonoBehaviour
{
    public GameObject policeOfficer;
    public GameObject[] policeVehicles;
    public PlayerMovementSM playsm;
    PoliceMovementSM policesm;
    public GameObject[] pedestrianSpawns;
    GameObject newPolicePedestrian;
    NavMeshAgent PoliceAI;
    Vector3 lastKnownPos;
    GameObject player;


    private void Start()
    {
        pedestrianSpawns = GameObject.FindGameObjectsWithTag("Spawn");
        StartCoroutine(PolicePedestrians());
        policesm = newPolicePedestrian.GetComponent<PoliceMovementSM>();
    }

    public IEnumerator PolicePedestrians()
    {
        int policePedestriansCount = 0;
        while (policePedestriansCount < 15)
        {
            int SpawnIndex = Random.Range(0, pedestrianSpawns.Length);
            int RandomSpawnDelay = Random.Range(0, 4);
            int RandomSpeed = Random.Range(1, 3);
            newPolicePedestrian = Instantiate(policeOfficer, pedestrianSpawns[SpawnIndex].transform.position, Quaternion.identity);

            policesm = newPolicePedestrian.GetComponent<PoliceMovementSM>();
            PoliceAI = newPolicePedestrian.GetComponent<NavMeshAgent>();

            player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                playsm = player.GetComponent<PlayerMovementSM>();
                player.GetComponent<GameObject>();
                if (playsm != null && (policesm != null))
                {
                    policesm.player = player;
                    policesm.playsm = playsm;
                }
            }

            if (PoliceLevel.policeLevels >= 1)
            {
                PoliceAI.SetDestination(player.transform.position);
            }
            PoliceAI.speed = RandomSpeed;
            yield return new WaitForSeconds(RandomSpawnDelay);
            policePedestriansCount++;
        }

        if (policePedestriansCount > 15)
        {
            StopCoroutine(PolicePedestrians());
        }
    }
}
