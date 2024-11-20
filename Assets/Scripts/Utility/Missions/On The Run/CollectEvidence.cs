using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectEvidence : MonoBehaviour
{
    [Header("GameObject References")]
    public GameObject evidence;
    public GameObject panel;
    public GameObject clueText;
    public bool reading = false;

    [Header("Script references")]
    public OnTheRun OTR;
    public RaycastMaster rMaster;

    public void PickUp()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        panel.SetActive(true);
    }

    public void CloseWindow()
    {
        OTR.collectedEvidence += 1;
        panel.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        reading = false;
        clueText.SetActive(true);
        OTR.clue.SetActive(true);
        rMaster.interactKey.SetActive(false);
        evidence.SetActive(false);
        OTR.magGlass.SetActive(true);
        
        OTR.objective.text = "Search Westral Square for evidence: " + OTR.collectedEvidence + " / " + OTR.totalEvidence;

        if (OTR.collectedEvidence == OTR.totalEvidence)
        {
            OTR.Evidence = true;
            OTR.clue.SetActive(false);
            OTR.magGlass.SetActive(false);
            OTR.objective.text = "Go to the gang compound.";
        }
    }
}
