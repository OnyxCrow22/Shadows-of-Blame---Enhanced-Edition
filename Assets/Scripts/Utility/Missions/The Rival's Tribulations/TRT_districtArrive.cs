using UnityEngine;

public class TRT_districtArrive : MonoBehaviour 
{
    public TheRivalsTribulations TRT;
    public Collider district;
    public bool inDistrict;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inDistrict = true;
            TRT.arrived = true;
            TRT.objective.text = "Find and tail the Olympia Cortage.";
        }
    }
}