using UnityEngine;
using TMPro;
using System.Collections;
using System.Net.Security;

public class RoadCheck : MonoBehaviour
{
    public TextMeshProUGUI currentRoad;
    public string roadName;
    public bool onRoad;
    public RaycastMaster rMaster;

    public bool showing;
    public bool autoShow;
    public Animator roadDisp;
    
    public void Start()
    {
        onRoad = false;
        showing = false;
        autoShow = false;
        roadDisp.enabled = false;
    }

    public IEnumerator RoadDisplay(bool manual = false)
    {
        if (showing)
        {
            yield break;
        }

        if (!manual && autoShow)
        {
            yield break;
        }

        showing = true;

        if (!manual)
        {
            autoShow = true;
        }

        roadDisp.enabled = true;

        roadDisp.SetBool("showName", true);

        currentRoad.text = roadName;
        
        yield return new WaitForSeconds(3);

        roadDisp.SetBool("showName", false);

        yield return new WaitForSeconds(3);

        roadDisp.enabled = false;

        showing = false;
    }
}
