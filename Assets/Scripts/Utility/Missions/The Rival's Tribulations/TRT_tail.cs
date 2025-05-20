using UnityEngine;

public class TRT_tail : MonoBehaviour
{
    public bool detect = false;
    public TheRivalsTribulations TRT;
    public bool tailed = false;

    public void TailCar()
    {
        TRT.objective.text = "Maintain a safe distance between you and the Contage.";
    }
}