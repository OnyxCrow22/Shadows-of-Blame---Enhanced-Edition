using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public bool reversed = false;
    public Light[] redLights;
    public Light[] greenLights;
    public Light[] amberLights;

    private enum trafficState { Red, Green, Amber, RedAmber };
    private trafficState currentState;
    private float timer;
    private float greenTime = 10;
    private float amberTime = 2;
    private float redTime = 8;
    private float redAmberTime = 2;

    public bool red, green, amber, redAmber;

    private void Start()
    {
        if (!reversed)
        {
            currentState = trafficState.Red;
            Lights(redLights, true);
            red = true;
        }
        else if (reversed)
        {
            currentState = trafficState.Green;
            Lights(greenLights, true);
            green = true;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        switch (currentState)
        {
            case trafficState.Red:
                {
                    if (timer >= redTime)
                    {
                        SwitchRedAmber();
                    }
                    break;
                }
            case trafficState.RedAmber:
                {
                    if (timer >= redAmberTime)
                    {
                        SwitchGreen();
                    }
                    break;
                }
            case trafficState.Green:
                {
                    if (timer >= greenTime)
                    {
                        SwitchAmber();
                    }
                }
                break;
            case trafficState.Amber:
                {
                    if (timer >= amberTime)
                    {
                        SwitchRed();
                    }
                }
                break;

        }
    }

    public void SwitchRed()
    {
        Lights(amberLights, false);
        Lights(greenLights, false);
        Lights(redLights, true);
        currentState = trafficState.Red;
        green = false;
        amber = false;
        red = true;
        timer = 0;
    }
    
    public void SwitchAmber()
    {
        Lights(amberLights, true);
        Lights(greenLights, false);
        Lights(redLights, false);
        green = false;
        amber = true;
        red = false;
        currentState = trafficState.Amber;
        timer = 0;
    }

    public void SwitchGreen()
    {
        Lights(amberLights, false);
        Lights(greenLights, true);
        Lights(redLights, false);
        green = true;
        amber = false; 
        red = false;
        currentState = trafficState.Green;
        timer = 0;
    }

    public void SwitchRedAmber()
    {
        Lights(amberLights, true);
        Lights(greenLights, false);
        Lights(redLights, true);
        green = false;
        red = true;
        amber = true;
        currentState = trafficState.RedAmber;
        timer = 0;
    }

    /*
    public IEnumerator TrafficA()
    {
        while (true)
        {
            Lights(redLights, true);
            red = true;
            yield return new WaitForSeconds(8);
            Lights(amberLights, true);
            amber = true;
            yield return new WaitForSeconds(2);
            Lights(redLights, false);
            Lights(amberLights, false);
            Lights(greenLights, true);
            red = false;
            amber = false;
            green = true;
            yield return new WaitForSeconds(10);
            Lights(amberLights, true);
            amber = true;
            green = false;
            Lights(greenLights, false);
            yield return new WaitForSeconds(2);
            Lights(redLights, true);
            red = true;
            Lights(amberLights, false);
            amber = false;
        }
    }

    public IEnumerator ReversedTraffic()
    {
        while (true)
        {
            Lights(greenLights, true);
            green = true;
            yield return new WaitForSeconds(10);
            Lights(amberLights, true);
            Lights(greenLights, false);
            amber = true;
            green = false;
            yield return new WaitForSeconds(2);
            Lights(redLights, true);
            Lights(amberLights, false);
            red = true;
            amber = false;
            yield return new WaitForSeconds(8);
            Lights(amberLights, true);
            yield return new WaitForSeconds(2);
            Lights(amberLights, false);
            Lights(redLights, false);
            Lights(greenLights, true);
            green = true;
        }
    }
    */

    public void Lights(Light[] trafficLight, bool isActive)
    {
        for (int i = 0; i < trafficLight.Length; i++)
        {
            trafficLight[i].gameObject.SetActive(isActive);
        }
    }
}
