using UnityEngine;
using TMPro;

public class DistrictCheck : MonoBehaviour
{
    public TextMeshProUGUI currentDistrict;
    public string districtName;
    public bool inDistrict;
    public RaycastMaster rMaster;
    
    public void Start()
    {
        currentDistrict.text = "";
        inDistrict = false;
    }

    public void CheckPlayer()
    {
        currentDistrict.text = districtName;
        inDistrict = true;
    }
}
