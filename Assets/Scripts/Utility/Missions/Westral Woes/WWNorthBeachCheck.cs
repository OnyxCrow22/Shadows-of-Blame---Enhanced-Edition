using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWNorthBeachCheck : MonoBehaviour
{
    public bool enteredNorthBeach = false;
    public WestralWoes WW;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && WW.collectedNorthbyEvidence && !WW.collectedNorthBeachEvidence || other.CompareTag("Vehicle") && WW.collectedNorthbyEvidence && !WW.collectedNorthBeachEvidence)
        {
            enteredNorthBeach = true;
            WW.enteredNorthBeach = true;
            WW.objective.text = "Go to Palm Surf.";
            WW.locationClues[0].text = "Palm Surf is located on Beachfront Avenue.";
            WW.locationClues[1].text = "The road has apartment buildings with neon lighting.";
            WW.locationClues[2].text = "It's in the NORTHEAST of North Beach.";
        }
    }
}
