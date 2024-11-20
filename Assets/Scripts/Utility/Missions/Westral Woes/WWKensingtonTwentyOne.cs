using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWKensingtonTwentyOne : MonoBehaviour
{
    public WestralWoes WW;
    public bool on21stFloor = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !on21stFloor && !WW.placedEvidence)
        {
            on21stFloor = true;
            WW.on21stFloor = true;
            WW.objective.text = "Place the final pieces of evidence on the wall in your suite.";
            WW.locationClues[0].text = "";
            WW.locationClues[1].text = "";
            WW.locationClues[2].text = "";
        }
    }
}
