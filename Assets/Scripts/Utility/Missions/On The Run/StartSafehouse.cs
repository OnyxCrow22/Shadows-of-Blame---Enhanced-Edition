using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSafehouse : MonoBehaviour
{
    public OnTheRun OTR;
    public bool left;

    private void OnTriggerExit(Collider other)
    {
        if (!OTR.GangEvidence && !OTR.inSafehouse)
        {
            OTR.objective.text = "Go to Westral Square.";
            left = true;
            OTR.inSafehouse = false;
        }
    }
}
