using UnityEngine;
using TMPro;
using System.Collections;

public class RoadCheck : MonoBehaviour
{
    public TextMeshProUGUI currentRoad;
    public string roadName;
    public bool onRoad;
    public RaycastMaster rMaster;

    public Animator roadDisp;
    
    public void Start()
    {
        currentRoad.text = "";
        onRoad = false;
    }

    public IEnumerator RoadDisplay()
    {
        roadDisp.SetBool("showName", true);

        currentRoad.text = roadName;
        
        yield return new WaitForSeconds(3);

        currentRoad.text = "";

        roadDisp.SetBool("showName", false);
    }
}
