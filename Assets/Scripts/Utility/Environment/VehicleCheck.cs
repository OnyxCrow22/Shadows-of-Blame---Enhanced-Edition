using UnityEngine;
using TMPro;

public class VehicleCheck : MonoBehaviour
{
    public TextMeshProUGUI currentVehicle;
    public string vehicleName;
    public bool inVehicle;
    public RaycastMaster rMaster;
    
    public void Start()
    {
        currentVehicle.text = "";
        inVehicle = false;
    }

    public void CheckPlayer()
    {
        currentVehicle.text = vehicleName;
        inVehicle = true;
    }
}
