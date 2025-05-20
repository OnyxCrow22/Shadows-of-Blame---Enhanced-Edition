using UnityEngine;

public class TRT_Safehouse : MonoBehaviour
{
    public TheRivalsTribulations TRT;
    public bool exitedSafe;

    private void OnTriggerExit(Collider other)
    {
        if (TRT.leftSafehouse)
        {
            TRT.objective.text = "Go to (district)";
            exitedSafe = true;
            TRT.leftSafehouse = true;
        }
    }
}
