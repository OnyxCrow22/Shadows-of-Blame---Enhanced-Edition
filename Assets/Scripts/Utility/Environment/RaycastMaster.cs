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
            switch (forwardHit.collider.gameObject.tag)
            {
                case "Door":
                door = true;
                doorS = forwardHit.collider.gameObject.GetComponent<Door>();
                interactKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && doorS.isOpen)
                {
                    StartCoroutine(doorS.ClosingDoor());
                    StopCoroutine(doorS.OpeningDoor());
                    interactKey.SetActive(false);
                }
                else if (Input.GetKeyDown(KeyCode.E) && !doorS.isOpen)
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

                case "VehicleDoor":
                {
                    vehicular = forwardHit.collider.gameObject.GetComponent<VehicleEnterExit>();
                    interactKey.SetActive(true);
                    vehicular.canEnter = true;
                    if (Input.GetKeyDown(KeyCode.E) && !vehicular.inVehicle)
                    {
                        vehicular.EnterVehicle();
                        vehicular.canEnter = false;
                        interactKey.SetActive(false);
                        inVehicle = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && vehicular.inVehicle)
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
                }

                case "EvidenceBoard":
                {
                    if (OTR.GangEvidence && OTR.enabled)
                    {
                        placeEvidence = forwardHit.collider.gameObject.GetComponent<EvidencePlace>();
                        Debug.Log("Board hit!");
                        interactKey.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E) && !placeEvidence.EvidencePlaced)
                        {
                            placeEvidence.StartCoroutine(placeEvidence.EvidenceSwap());
                            placeEvidence.EvidencePlaced = true;
                            interactKey.SetActive(false);
                        }
                    }
                    break;
                }

                case "WesteriaEvidenceBoard":
                {
                    if (WW.collectedNorthBeachEvidence && WW.enabled)
                    {
                        finalEvidence = forwardHit.collider.gameObject.GetComponent<WWPlaceEvidence>();
                        Debug.Log("Final board hit!");
                        interactKey.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E) && !finalEvidence.EvidencePlaced)
                        {
                            finalEvidence.StartCoroutine(finalEvidence.EvidenceSwap());
                            finalEvidence.EvidencePlaced = true;
                            interactKey.SetActive(false);
                        }
                    }
                    break;
                }

                case "LiftObj":
                {
                    newLift = forwardHit.collider.gameObject.GetComponent<Lift>();
                    Debug.Log("Going up or down?");
                    interactKey.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
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
                }
                
                case "Button":
                {
                    callLift = forwardHit.collider.gameObject.GetComponent<LiftCall>();
                    interactKey.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
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
                }
            }
        }
    }

    public void DownChecker()
    {
        Ray downRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
        float downLength = 4;
        if (Physics.Raycast(downRay, out RaycastHit downHit, downLength))
        {
            switch (downHit.collider.gameObject.tag)
            {
                case "Evidence":
                {
                    collectEvidence = downHit.collider.gameObject.GetComponent<CollectEvidence>();
                    Debug.Log("HIT THE EVIDENCE!");
                    interactKey.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && !collectEvidence.reading)
                    {
                        collectEvidence.PickUp();
                        collectEvidence.reading = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && collectEvidence.reading)
                    {
                        collectEvidence.CloseWindow();
                    }
                    break;
                }

                case "HParkEvidence":
                {
                    HParkEvidence = downHit.collider.gameObject.GetComponent<WWCollectHParkEvidence>();
                    Debug.Log("HIT THE EVIDENCE!");
                    interactKey.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && !HParkEvidence.reading)
                    {
                        HParkEvidence.PickUp();
                        HParkEvidence.reading = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && HParkEvidence.reading)
                    {
                        HParkEvidence.CloseWindow();
                    }
                    break;
                }

                case "PrescottEvidence":
                {
                    prescottEvidence = downHit.collider.gameObject.GetComponent<WWCollectPrescottEvidence>();
                    Debug.Log("HIT THE EVIDENCE!");
                    interactKey.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && !prescottEvidence.reading)
                    {
                        prescottEvidence.PickUp();
                        prescottEvidence.reading = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && prescottEvidence.reading)
                    {
                        prescottEvidence.CloseWindow();
                    }
                    break;
                }

                case "GEvidence":
                {
                    gECollect = downHit.collider.gameObject.GetComponent<GangEvidenceCollect>();
                    Debug.Log("Evidence hit!");
                    interactKey.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && !gECollect.isgReading)
                    {
                        gECollect.GEPickup();
                        gECollect.isgReading = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && gECollect.isgReading)
                    {
                        gECollect.GECloseWindow();
                    }
                    break;
                }

                case "NorthbyEvidence":
                {
                    northbyCollect = downHit.collider.gameObject.GetComponent<WWNorthbyGangEvidence>();
                    Debug.Log("Evidence hit!");
                    interactKey.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && !northbyCollect.isgReading)
                    {
                        northbyCollect.GEPickup();
                        northbyCollect.isgReading = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && northbyCollect.isgReading)
                    {
                        northbyCollect.GECloseWindow();
                    }
                    break;
                }

                case "NorthBeachEvidence":
                {
                    northBeachCollect = downHit.collider.gameObject.GetComponent<WWNorthBeachEvidence>();
                    Debug.Log("Evidence hit!");
                    interactKey.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && !northBeachCollect.isgReading)
                    {
                        northBeachCollect.GEPickup();
                        northBeachCollect.isgReading = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.E) && northBeachCollect.isgReading)
                    {
                        northBeachCollect.GECloseWindow();
                    }
                    break;
                }
            }
        }
    }

    public void NameChecker()
    {
        Ray nameRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        float nameLength = 24;
        if (Physics.Raycast(nameRay, out RaycastHit nameHit, nameLength))
        {
            // Switch statement for checking each name indivdiually.
            switch (nameHit.collider.gameObject.tag)
            {
                case "Road":
                {
                    // Update the road accordingly.
                    roads = nameHit.collider.gameObject.GetComponent<RoadCheck>();
                    onRoad = true;

                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        roads.StartCoroutine(roads.RoadDisplay(true));
                        Debug.Log("I hit " + roads.roadName);
                    }
                    else
                    {
                        roads.StartCoroutine(roads.RoadDisplay(false));
                    }
                    break;
                }
                case "District":
                {
                    districts = nameHit.collider.gameObject.GetComponent<DistrictCheck>();
                    districts.currentDistrict.text = districts.districtName;
                    inDistrict = true;
                    Debug.Log("Welcome to " + districts.districtName);
                    break;
                }

                case "Vehicle":
                {
                    vehicles = nameHit.collider.gameObject.GetComponent<VehicleCheck>();
                    vehicles.currentVehicle.text = vehicles.vehicleName;
                    Debug.Log("You are currently driving" + vehicles.currentVehicle);
                    break;
                }
            }
        }
    }
}
