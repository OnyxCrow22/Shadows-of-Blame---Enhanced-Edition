using UnityEngine;

public class RaycastMaster : MonoBehaviour
{
    [Header("Raycast References")]
    public PlayerMovementSM playsm;
    public OnTheRun OTR;
    public WestralWoes WW;
    public VehicleEnterExit vehicular;

    [Header("UI Reference")]
    public GameObject interactKey;

    [Header("Component References")]
    Door doorS;
    CollectEvidence collectEvidence;
    WWCollectHParkEvidence HParkEvidence;
    WWCollectPrescottEvidence prescottEvidence;
    GangEvidenceCollect gECollect;
    WWNorthbyGangEvidence northbyCollect;
    WWNorthBeachEvidence northBeachCollect;
    EvidencePlace placeEvidence;
    WWPlaceEvidence finalEvidence;
    Lift newLift;
    LiftCall callLift;

    RoadCheck roads;
    DistrictCheck districts;
    VehicleCheck vehicles;

    [Header("Booleans")]

    public bool door = false;
    public bool evidence = false;
    public bool carDoor = false;
    public bool board = false;
    public bool buttonPressed = false;
    public bool inLift = false;

    public bool onRoad = false;
    public bool inDistrict = false;
    public bool inVehicle = false;

    // Update is called once per frame
    void Update()
    {
        DownChecker();
        ForwardChecker();
        NameChecker();
    }

    public void ForwardChecker()
    {
        Ray forwardRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        RaycastHit forwardHit;
        float forwardLength = 10;

        if (Physics.Raycast(forwardRay, out forwardHit, forwardLength))
        {
            switch (forwardHit.collider.gameObject.CompareTag("Door"))
            {
                case true:
                    door = true;
                    doorS = forwardHit.collider.gameObject.GetComponent<Door>();
                    interactKey.SetActive(true);
                    if (/* Input.GetKeyDown(KeyCode.E) && */ doorS.isOpen)
                    {
                        StartCoroutine(doorS.ClosingDoor());
                        StopCoroutine(doorS.OpeningDoor());
                        interactKey.SetActive(false);
                    }
                    else if (/* Input.GetKeyDown(KeyCode.E) && */ !doorS.isOpen)
                    {
                        StartCoroutine(doorS.OpeningDoor());
                        StopCoroutine(doorS.ClosingDoor());
                        interactKey.SetActive(false);
                    }
                    // We did not hit a door, set the interact key to false.
                    else
                    {
                        interactKey.SetActive(false);
                    }
                    break;
                case false:
                    break;
            }

            switch (forwardHit.collider.gameObject.CompareTag("VehicleDoor"))
            {
                case true:
                    vehicular = forwardHit.collider.gameObject.GetComponent<VehicleEnterExit>();
                    interactKey.SetActive(true);
                    vehicular.canEnter = true;
                    if (playsm.pControls.Player.Interact.IsPressed() && !vehicular.inVehicle)
                    {
                        vehicular.EnterVehicle();
                        vehicular.canEnter = false;
                        interactKey.SetActive(false);
                        inVehicle = true;
                    }
                    else if (playsm.pControls.Player.Interact.IsPressed() && vehicular.inVehicle)
                    {
                        vehicular.ExitVehicle();
                        vehicular.canExit = true;
                        vehicular.canEnter = false;
                        interactKey.SetActive(false);
                    }
                    else
                    {
                        interactKey.SetActive(false);
                        vehicular.canEnter = false;
                    }
                    break;
                case false:
                    break;
            }

            switch (forwardHit.collider.gameObject.CompareTag("EvidenceBoard"))
            {
                case true:
                    if (OTR.GangEvidence && OTR.enabled)
                    {
                        placeEvidence = forwardHit.collider.gameObject.GetComponent<EvidencePlace>();
                        Debug.Log("Board hit!");
                        interactKey.SetActive(true);
                        if (playsm.pControls.Player.Interact.IsPressed() && !placeEvidence.EvidencePlaced)
                        {
                            placeEvidence.StartCoroutine(placeEvidence.EvidenceSwap());
                            placeEvidence.EvidencePlaced = true;
                            interactKey.SetActive(false);
                        }
                    }
                    break;
                case false:
                    break;
            }

            switch (forwardHit.collider.gameObject.CompareTag("WesteriaEvidenceBoard"))
            {
                case true:
                    if (WW.collectedNorthBeachEvidence && WW.enabled)
                    {
                        finalEvidence = forwardHit.collider.gameObject.GetComponent<WWPlaceEvidence>();
                        Debug.Log("Final board hit!");
                        interactKey.SetActive(true);
                        if (playsm.pControls.Player.Interact.IsPressed() && !finalEvidence.EvidencePlaced)
                        {
                            finalEvidence.StartCoroutine(finalEvidence.EvidenceSwap());
                            finalEvidence.EvidencePlaced = true;
                            interactKey.SetActive(false);
                        }
                    }
                    break;
                case false:
                    break;
            }

            switch (forwardHit.collider.gameObject.CompareTag("LiftObj"))
            {
                case true:
                    newLift = forwardHit.collider.gameObject.GetComponent<Lift>();
                    Debug.Log("Going up or down?");
                    interactKey.SetActive(true);
                    if (playsm.pControls.Player.Interact.IsPressed())
                    {
                        if (newLift.atBottom)
                        {
                            StartCoroutine(newLift.OperateLift());
                            interactKey.SetActive(false);
                            buttonPressed = true;
                            inLift = true;
                            Debug.Log("Please stand clear of the doors. We are now going up to the 21st Floor.");
                        }
                        else if (newLift.atTop)
                        {
                            StartCoroutine(newLift.GoingDown());
                            interactKey.SetActive(false);
                            buttonPressed = true;
                            inLift = true;
                            Debug.Log("Please stand clear of the doors. We are now going down to the ground floor.");
                        }
                    }
                    break;
                case false:
                    break;
            }

            switch (forwardHit.collider.gameObject.CompareTag("Button"))
            {
                case true:
                    callLift = forwardHit.collider.gameObject.GetComponent<LiftCall>();
                    interactKey.SetActive(true);
                    if (playsm.pControls.Player.Interact.IsPressed())
                    {
                        if (callLift.liftToCall.atBottom)
                        {
                            callLift.Up();
                            inLift = false;
                            interactKey.SetActive(false);
                        }
                        else if (callLift.liftToCall.atTop)
                        {
                            callLift.Down();
                            inLift = false;
                            interactKey.SetActive(false);
                        }
                    }
                    break;
                case false:
                    break;
            }
        }
        else
        {
            door = false;
            if (vehicular != null) vehicular.canEnter = false;
            interactKey.SetActive(false);
        }
    }

    public void DownChecker()
    {
        Ray downRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
        float downLength = 4;
        if (Physics.Raycast(downRay, out RaycastHit downHit, downLength))
        {
            switch (downHit.collider.gameObject.CompareTag("Evidence"))
            {
                case true:
                    collectEvidence = downHit.collider.gameObject.GetComponent<CollectEvidence>();
                    Debug.Log("HIT THE EVIDENCE!");
                    interactKey.SetActive(true);
                    if (playsm.pControls.Player.Interact.IsPressed() && !collectEvidence.reading)
                    {
                        collectEvidence.PickUp();
                        collectEvidence.reading = true;
                    }
                    else if (playsm.pControls.Player.Interact.IsPressed() && collectEvidence.reading)
                    {
                        collectEvidence.CloseWindow();
                    }
                    break;
                case false:
                    break;
            }

            switch (downHit.collider.gameObject.CompareTag("HParkEvidence"))
            {
                case true:
                    HParkEvidence = downHit.collider.gameObject.GetComponent<WWCollectHParkEvidence>();
                    Debug.Log("HIT THE EVIDENCE!");
                    interactKey.SetActive(true);
                    if (playsm.pControls.Player.Interact.IsPressed() && !HParkEvidence.reading)
                    {
                        HParkEvidence.PickUp();
                        HParkEvidence.reading = true;
                    }
                    else if (playsm.pControls.Player.Interact.IsPressed() && HParkEvidence.reading)
                    {
                        HParkEvidence.CloseWindow();
                    }
                    break;
                case false:
                    break;
            }

            switch (downHit.collider.gameObject.CompareTag("PrescottEvidence"))
            {
                case true:
                    prescottEvidence = downHit.collider.gameObject.GetComponent<WWCollectPrescottEvidence>();
                    Debug.Log("HIT THE EVIDENCE!");
                    interactKey.SetActive(true);
                    if (playsm.pControls.Player.Interact.IsPressed() && !prescottEvidence.reading)
                    {
                        prescottEvidence.PickUp();
                        prescottEvidence.reading = true;
                    }
                    else if (playsm.pControls.Player.Interact.IsPressed() && prescottEvidence.reading)
                    {
                        prescottEvidence.CloseWindow();
                    }
                    break;
                case false:
                    break;
            }

            switch (downHit.collider.gameObject.CompareTag("GEvidence"))
            {
                case true:
                    gECollect = downHit.collider.gameObject.GetComponent<GangEvidenceCollect>();
                    Debug.Log("Evidence hit!");
                    interactKey.SetActive(true);
                    if (playsm.pControls.Player.Interact.IsPressed() && !gECollect.isgReading)
                    {
                        gECollect.GEPickup();
                        gECollect.isgReading = true;
                    }
                    else if (playsm.pControls.Player.Interact.IsPressed() && gECollect.isgReading)
                    {
                        gECollect.GECloseWindow();
                    }
                    break;
                case false:
                    break;
            }

            switch (downHit.collider.gameObject.CompareTag("NorthbyEvidence"))
            {
                case true:
                    northbyCollect = downHit.collider.gameObject.GetComponent<WWNorthbyGangEvidence>();
                    Debug.Log("Evidence hit!");
                    interactKey.SetActive(true);
                    if (playsm.pControls.Player.Interact.IsPressed() && !northbyCollect.isgReading)
                    {
                        northbyCollect.GEPickup();
                        northbyCollect.isgReading = true;
                    }
                    else if (playsm.pControls.Player.Interact.IsPressed() && northbyCollect.isgReading)
                    {
                        northbyCollect.GECloseWindow();
                    }
                    break;
                case false:
                    break;
            }

            switch (downHit.collider.gameObject.CompareTag("NorthBeachEvidence"))
            {
                case true:
                    northBeachCollect = downHit.collider.gameObject.GetComponent<WWNorthBeachEvidence>();
                    Debug.Log("Evidence hit!");
                    interactKey.SetActive(true);
                    if (playsm.pControls.Player.Interact.IsPressed() && !northBeachCollect.isgReading)
                    {
                        northBeachCollect.GEPickup();
                        northBeachCollect.isgReading = true;
                    }
                    else if (playsm.pControls.Player.Interact.IsPressed() && northBeachCollect.isgReading)
                    {
                        northBeachCollect.GECloseWindow();
                    }
                    break;
                case false:
                    break;
            }
        }
        else
        {
            interactKey.SetActive(false);
            evidence = false;
        }
    }

    public void NameChecker()
    {
        Ray nameRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        float nameLength = 24;
        if (Physics.Raycast(nameRay, out RaycastHit nameHit, nameLength))
        {
            // Switch statement for checking each name individually.
            switch (nameHit.collider.gameObject.CompareTag("Road"))
            {
                case true:
                    // Update the road accordingly.
                    roads = nameHit.collider.gameObject.GetComponent<RoadCheck>();
                    onRoad = true;

                    if (playsm.pControls.Player.RevealInfo.IsPressed())
                    {
                        roads.StartCoroutine(roads.RoadDisplay(true));
                        Debug.Log("I hit " + roads.roadName);
                    }
                    else
                    {
                        roads.StartCoroutine(roads.RoadDisplay(false));
                    }
                    break;
                case false:
                    onRoad = false;
                    if (roads != null) roads.StartCoroutine(roads.RoadDisplay(false));
                    break;
            }
            switch (nameHit.collider.gameObject.CompareTag("District"))
            {
                case true:
                    districts = nameHit.collider.gameObject.GetComponent<DistrictCheck>();
                    districts.currentDistrict.text = districts.districtName;
                    inDistrict = true;
                    Debug.Log("Welcome to " + districts.districtName);
                    break;
                case false:
                    inDistrict = false;
                    if (districts != null) districts.currentDistrict.text = "";
                    break;
            }

            switch (nameHit.collider.gameObject.CompareTag("Vehicle"))
            {
                case true:
                    vehicles = nameHit.collider.gameObject.GetComponent<VehicleCheck>();
                    vehicles.currentVehicle.text = vehicles.vehicleName;
                    Debug.Log("You are currently driving" + vehicles.currentVehicle);
                    break;
                case false:
                    if (vehicles != null) vehicles.currentVehicle.text = "";
                    break;
            }
        }
        else
        {
            onRoad = false;
            inDistrict = false;
            if (roads != null) roads.StartCoroutine(roads.RoadDisplay(false));
            if (districts != null) districts.currentDistrict.text = "";
            if (vehicles != null) vehicles.currentVehicle.text = "";
        }
    }
}