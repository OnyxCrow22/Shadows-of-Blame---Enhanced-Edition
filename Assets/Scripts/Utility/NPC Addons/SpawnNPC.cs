using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawnNPC : MonoBehaviour
{
    public GameObject[] WalkAI;
    public int AICount;
    public GameObject player;
    public LayerMask pavement;
    public GameObject[] spawnPoint;
    [HideInInspector]
    public GameObject newNPC;
    [HideInInspector]
    public NavMeshAgent AI;
    [HideInInspector]
    public NPCMovementSM NPCSM;

    public GameObject gameManager;

    PlayerMovementSM playsm;

    private void Start()
    {
        spawnPoint = GameObject.FindGameObjectsWithTag("Spawn");
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (AICount < 75)
        {
            int RandomIndex = Random.Range(0, WalkAI.Length);
            int RandomSpawnIndex = Random.Range(0, spawnPoint.Length);
            int RandomSpawnDelay = Random.Range(0, 4);
            int RandomSpeed = Random.Range(1, 3);
            newNPC = Instantiate(WalkAI[RandomIndex], spawnPoint[RandomSpawnIndex].transform.position, Quaternion.identity);

            NPCSM = newNPC.GetComponent<NPCMovementSM>();
            AI = newNPC.GetComponent<NavMeshAgent>();
            newNPC.GetComponent<NPCMovementSM>().spawnedIn = true;

            if (newNPC.CompareTag("MaleNPC"))
            {
                NPCSM.isMale = true;
            }
            else if (newNPC.CompareTag("FemaleNPC"))
            {
                NPCSM.isFemale = true;
                NPCSM.aggression = 0;
            }

            if (gameManager != null && NPCSM != null)
            {
                NPCSM.police = gameManager.GetComponent<PoliceLevel>();
            }

            if (player != null)
            {
                playsm = player.GetComponent<PlayerMovementSM>();
                player.GetComponent<GameObject>();
                if (playsm != null && NPCSM != null)
                {
                    NPCSM.playsm = playsm;
                    NPCSM.player = player;
                }

            }

            AI.speed = RandomSpeed;

            yield return new WaitForSeconds(RandomSpawnDelay);
            AICount++;
        }
        if (AICount > 75)
        {
            StopCoroutine(Spawn());
            AICount = 0;
        }
    }
}