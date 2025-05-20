using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheRivalsTribulations : MonoBehaviour
{
    public TextMeshProUGUI objective;
    public TextMeshProUGUI mission;
    public bool leftSafehouse, arrived, detected, tailedCar, evidenceFound, leftCompound, gangLeaderD, briefcaseCollect, lostPolice, arrivedKingston, isDusk, construct, oneOneSeven, chased, rivalD;

    void Start()
    {
        mission.text = "The Rival's Tribulations";
        LeaveKensingtonBlvdSafehouse();
    }

    // Update is called once per frame
    void Update()
    {
        if (PoliceLevel.policeLevels >= 1)
        {
            objective.text = "Lose the police";
        }
    }

    void LeaveKensingtonBlvdSafehouse()
    {
        if (leftSafehouse)
        {
            objective.text = "Go to (district)";
            GoToDistrict();
        }
    }

    void GoToDistrict()
    {   
        if (arrived)
        {
            objective.text = "Tail the Olympia Cortage.";
            TailCar();
        }
    }

    void TailCar()
    {
        if (!detected && tailedCar)
        {
            objective.text = "Search the La Fontera compound for evidence";
            SearchCompound();
        }
    }

    void SearchCompound()
    {
        if (evidenceFound)
        {
            objective.text = "Leave the La Fontera compound.";
            LeaveCompound();
        }
    }

    void LeaveCompound()
    {
        if (leftCompound)
        {
            objective.text = "Kill the gang leader before he arrives at the airport.";
            StopGangLeader();
        }
    }

    void StopGangLeader()
    {
        if (gangLeaderD)
        {
            objective.text = "Take the briefcase.";
            TakeEvidence();
        }
    }

    void TakeEvidence()
    {
        if (briefcaseCollect)
        {
            objective.text = "Lose the police.";
            LosePolice();
        }
    }

    void LosePolice()
    {
        if (lostPolice)
        {
            objective.text = "Go to Kingston Street on Saint Mary's Island.";
            GoToKingstonStreet();
        }
    }

    void GoToKingstonStreet()
    {
        if (arrivedKingston)
        {
            objective.text = "Wait until dusk.";
            WaitUntilDusk();
        }
    }

    void WaitUntilDusk()
    {
        if (isDusk)
        {
            objective.text = "Go to the Amesbury Tower construction site.";
            ConstructionSite();
        }
    }

    void ConstructionSite()
    {
        if (construct)
        {
            objective.text = "Take the lift to the 117th floor.";
            FloorNumber();
        }   
    }

    void FloorNumber()
    {
        if (oneOneSeven)
        {
            objective.text = "Chase 'The Rival'.";
            ChaseRival();
        }
    }

    void ChaseRival()
    {
        if (chased)
        {
            objective.text = "Kill 'The Rival'.";
            KillRival();
        }
    }

    void KillRival()
    {
        if (rivalD)
        {
            objective.text = "Leave the construction site.";
        }
    }
}
