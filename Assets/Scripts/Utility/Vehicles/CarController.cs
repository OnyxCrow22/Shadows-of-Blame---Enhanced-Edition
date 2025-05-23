using System.Collections;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    float horizontalInput;
    float verticalInput;
    float steeringAngle;
    public float maxSteeringAngle;
    public float motorForce;
    public float brakeForce;
    public float maxSpeed;
    float currentSpeed;
    public float indicator;
    public int pressCount;

    public Rigidbody target;
    public TextMeshProUGUI speedText;
    public Light leftIndicator, rightIndicator;
    public Light rearLight1, rearLight2;
    public Light[] reverseLight;
    public PlayerControls pControls;

    [Header("Wheel colliders")]
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;

    [Header("Transforms")]
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;

    [Header("Booleans")]
    public bool braking = false;
    public bool turningLeft = false;
    public bool turningRight = false;
    public bool indicating = false;
    public bool reversing = false;
    public bool negative = false;
    public bool vehicleonSide = false;

    [Header("Float references")]
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI Elements")]
    public RectTransform needle;
    public GameObject speedometer;
    RaycastMaster rMaster;

    void Awake()
    {
        pControls = new PlayerControls();
    }

    void OnEnable()
    {
        pControls.Enable();
    }

    void OnDisable()
    {
        pControls.Disable();   
    }

    private void Start()
    {
        currentSpeed = 0;
        rMaster = FindFirstObjectByType<RaycastMaster>();
    }

    public void FixedUpdate()
    {
        Steer();
        Accelerate();
        Reverse();
        Brake();
        TurnIndicators();
        UpdateWheelPoses();
    }

    private void Update()
    {
        GetInput();
        UpdateSpeed();

        if (transform.rotation.x > 10)
        {
            vehicleonSide = true;
        }
        else
        {
            vehicleonSide = false;
        }    
    }

    public void GetInput()
    {
       // horizontalInput = pControls.Driving.Accelerate.ReadValue<float>();
        verticalInput = pControls.Driving.Accelerate.ReadValue<float>();

        if (pControls.Driving.Accelerate.IsPressed() && verticalInput > 0)
        {
            Accelerate();
        }

        if (pControls.Driving.Brake.IsPressed() && verticalInput <= 0 && !braking && currentSpeed > 0)
        {
            Brake();
            braking = true;
        }

        else if (verticalInput > 0.1f)
        {
            braking = false;
        }

        if (verticalInput < 0.11f)
        {
            Reverse();
            reversing = true;
            braking = false;
        }

        if (pControls.Driving.LeftIndicator.IsPressed() && !turningLeft && pressCount == 1)
        {
            TurnIndicators();
            turningLeft = true;
            indicating = true;
            pressCount += 1;
        }
        else if (!pControls.Driving.LeftIndicator.IsPressed() && turningLeft && pressCount == 2)
        {
            turningLeft = false;
            indicating = false;
            pressCount -= 1;
        }

        if (pControls.Driving.RightIndicator.IsPressed() && !turningRight && pressCount == 1)
        {
            TurnIndicators();
            turningRight = true;
            indicating = true;
            pressCount += 1;
        }
        else if (!pControls.Driving.RightIndicator.IsPressed() && turningRight && pressCount == 2)
        {
            turningRight = false;
            indicating = false;
            pressCount -= 1;
        }

        if (pControls.Driving.FlipCar.IsPressed() && vehicleonSide)
        {
            FlipCar();
        }

        if (pControls.Driving.RevealInfo.IsPressed() && rMaster.onRoad)
        {
            rMaster.NameChecker();
        }
    }



    private void UpdateSpeed()
    {
        const float MPH_CONVERSION = 2.23694f;
        currentSpeed = target.linearVelocity.magnitude * MPH_CONVERSION;

        speedText.text = currentSpeed.ToString("00" + " MPH");
        needle.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, currentSpeed / maxSpeed));
    }

    private void Steer()
    {
        horizontalInput = pControls.Driving.Steer.ReadValue<Vector2>().x;
        steeringAngle = maxSteeringAngle * horizontalInput;
        frontDriverW.steerAngle = steeringAngle;
        frontPassengerW.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = verticalInput * motorForce;
        frontPassengerW.motorTorque = verticalInput * motorForce;
        rearDriverW.motorTorque = verticalInput * motorForce;
        rearPassengerW.motorTorque = verticalInput * motorForce;
    }

    private void Brake()
    {
        if (braking && currentSpeed >= 0.25f && !reversing)
        {
            frontDriverW.brakeTorque = brakeForce;
            frontPassengerW.brakeTorque = brakeForce;
            rearDriverW.brakeTorque = brakeForce;
            rearPassengerW.brakeTorque = brakeForce;
            rearLight1.gameObject.SetActive(true);
            rearLight2.gameObject.SetActive(true);
        }
        else if (horizontalInput > 0.01f | verticalInput > 0.01f) 
        {
            frontDriverW.brakeTorque = 0f;
            frontPassengerW.brakeTorque = 0f;
            rearDriverW.brakeTorque = 0f;
            rearPassengerW.brakeTorque = 0f;
            rearLight1.gameObject.SetActive(false);
            rearLight2.gameObject.SetActive(false);
        }
    }

    private void Reverse()
    {
        if (verticalInput <= 0.1 && reversing && !braking)
        {
            frontDriverW.motorTorque = verticalInput * motorForce;
            frontPassengerW.motorTorque = verticalInput * motorForce;
            rearDriverW.motorTorque = verticalInput * motorForce;
            rearPassengerW.motorTorque = verticalInput * motorForce;

            reverseLight[0].gameObject.SetActive(true);
            reverseLight[1].gameObject.SetActive(true);
        }
        // No longer reversing, turn off lights.
        else if (verticalInput > 0.1) 
        {
            reverseLight[0].gameObject.SetActive(false);
            reverseLight[1].gameObject.SetActive(false);
            reversing = false;
            negative = false;
        }
    }

    private void FlipCar()
    {

    }

    private void TurnIndicators()
    {
        if (turningLeft)
        {
            StartCoroutine(TurningLeft());
            indicating = true;
        }
        if (turningRight)
        {
            indicating = true;
            StartCoroutine(TurningRight());
        }
    }

    IEnumerator TurningLeft()
    {
        while (turningLeft)
        {
            leftIndicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(indicator);
            leftIndicator.gameObject.SetActive(false);
            yield return new WaitForSeconds(indicator);
            leftIndicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(indicator);
        }
    }

    IEnumerator TurningRight()
    {
        while (turningRight)
        {
            rightIndicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(indicator);
            rightIndicator.gameObject.SetActive(false);
            yield return new WaitForSeconds(indicator);
            rightIndicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(indicator);
        }
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider wCol, Transform vtransform)
    {
        Vector3 carPos;
        Quaternion carQuat;

        wCol.GetWorldPose(out carPos, out carQuat);

        vtransform.transform.position = carPos;
        vtransform.transform.rotation = carQuat;
    }
}
