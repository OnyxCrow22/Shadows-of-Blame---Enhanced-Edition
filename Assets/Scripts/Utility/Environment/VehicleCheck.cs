using UnityEngine;
using TMPro;

public class VehicleCheck : MonoBehaviour
{
    public TextMeshProUGUI currentVehicle;
    public string vehicleName;
    public bool inVehicle;
    public RaycastMaster rMaster;

    public Animator vehicleDisp;
    
    public void Start()
    {
        currentVehicle.text = "";
        inVehicle = false;
        rMaster = GameObject.FindGameObjectWithTag("Player").GetComponent<RaycastMaster>();
    }

    public void CheckPlayer()
    {
        currentVehicle.text = vehicleName;
        inVehicle = true;
    }
}
