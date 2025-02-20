using UnityEngine;
using TMPro;

public class RoadCheck : MonoBehaviour
{
    public TextMeshProUGUI currentRoad;
    public string roadName;
    public bool onRoad;
    public RaycastMaster rMaster;
    
    public void Start()
    {
        currentRoad.text = "";
        onRoad = false;
    }

    public void CheckPlayer()
    {
        currentRoad.text = roadName;
        onRoad = true;
    }
}
