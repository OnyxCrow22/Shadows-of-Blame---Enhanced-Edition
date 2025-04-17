using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceLevel : MonoBehaviour
{
    // TO DO: Rework this completely.
    public GameObject[] levels;

    public enum policeState {normal, searching, pursuit}

    public policeState currentState;
    public GameObject policeBorder;
    public bool addingLevel;
    public static int policeLevels;
    public static bool activateLevel;
    public int killedNPCS;
    public int killedOfficers;
    public float flashDelay = 0.5f;

    public GangEvidenceCollect alejandro;
    public WWNorthBeachEvidence NorthBeach;
    public PoliceEvaded evaded;
    public OnTheRun OTR;

    public bool spottedPlayer = false;
    public bool cancelPursuit = false;
    float lastSighted = 0;

    private void Update()
    {
        if (!cancelPursuit)
        {
            AddingLevel();
            UpdateLevel();
        }
    }

    public void AddingLevel()
    {
        if (!addingLevel && activateLevel)
        {
            activateLevel = false;
            addingLevel = true;
            policeBorder.SetActive(true);
            StartCoroutine(AddLevel());
        }
    }

    IEnumerator AddLevel()
    {
        for (int i = 0; i < policeLevels; i++)
        {
            levels[i].SetActive(true);
            yield return new WaitForSeconds(flashDelay);
            levels[i].SetActive(false);
            yield return new WaitForSeconds(flashDelay);
            levels[i].SetActive(true);
        }
    }

    public void LostPlayer()
    {
        if (!spottedPlayer && policeLevels >= 1)
        {
            currentState = policeState.searching;
            StartCoroutine(PlayerSearch());
            Debug.Log("Begin search...");
        }
        else if (cancelPursuit)
        {
            AbortPursuit();
        }
    }

    public IEnumerator PlayerSearch()
    {
        float searchTime = 0;

        while (searchTime < 5)
        {
            yield return new WaitForSeconds(1);
            searchTime++;
            if (!spottedPlayer && searchTime >= 5)
            {
                AbortPursuit();
                yield break;
            }
        }
    }

    public void PlayerSpotted()
    {
        spottedPlayer = true;
        lastSighted = Time.time;
        levels[policeLevels].SetActive(true);
    }

    public void UpdateLevel()
    {
        switch (killedNPCS)
        {
            case 1:
            {
                policeLevels = 1;
                activateLevel = true;
                break;
            }
            case 3:
            {
                policeLevels = 2;
                break;
            }
            case 9:
            {
                policeLevels = 3;
                break;
            }
            case 12:
            {
                policeLevels = 4;
                break;
            }
            case 15:
            {
                policeLevels = 5;
                break;
            }
        }

        switch (killedOfficers)
        {
            case 1:
            {
                policeLevels = 2;
                if (!activateLevel)
                {
                    activateLevel = true;
                }
                break;
            }
            case 3:
            {
                policeLevels = 3;
                break;
            }
            case 5:
            {
                policeLevels = 4;
                break;
            }
            case > 7:
            {
                policeLevels = 5;
                break;
            }
        }

        if (OTR.wLocked.attemptingWesteria)
        {
            policeLevels = 5;
            activateLevel = true;
        }

        if (alejandro.evidence)
        {
            policeLevels = 1;
            activateLevel = true;
        }

        if (NorthBeach.evidence)
        {
            policeLevels = 1;
            activateLevel = true;
        }

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i < policeLevels);
        }
    }

    public void AbortPursuit()
    {
        activateLevel = false;
        addingLevel = false;
        policeBorder.SetActive(false);
        cancelPursuit = true;
        policeLevels = 0;
        killedNPCS = 0;
        Debug.Log("All units: Harrison Felton has escaped. Return to patrols. Repeat: Return to patrols, Felton has escaped. Don't tell the police commissioner..");

        if (alejandro.evidence)
        {
            alejandro.CancelPursuit();
        }
        if (NorthBeach.evidence)
        {
            NorthBeach.Next();
        }

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }
}
