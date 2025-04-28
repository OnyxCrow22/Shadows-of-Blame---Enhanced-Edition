using UnityEngine;

public class WeatherCycle : MonoBehaviour
{
    [Header("Weather Types")]
    public GameObject sunny;
    public GameObject partlyCloudy;
    /*
    public GameObject cloudy;
    public GameObject fog;
    public GameObject rain;
    public GameObject thunderStorm;
    */

    public bool isSunny = false;
    public bool isPartCloud = false;
    /*
    public bool isCloudy = false;
    public bool isFoggy = false;
    public bool isRaining = false;
    public bool isThundering = false;
    */
    public float timer = 0;
    public float weatherChange;
    public int randomSwitch;
    public enum weatherState { sunny, partlyCloudy, /*raining, thundering, cloudy, foggy*/}
    public weatherState currentState;

    /// <summary>
    /// Update the timer every frame?
    /// </summary>
    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= Random.Range(10, 240))
        {
            NextWeather();
            timer = 0;
        }
    }

    /// <summary>
    /// Switches the Weather according to values.
    /// </summary>
    public void WeatherSwitch()
    {
        switch (currentState)
        {
            case weatherState.sunny:
                {
                    sunny.SetActive(true);
                    isSunny = true;
                    currentState = weatherState.sunny;
                    break;
                }
            case weatherState.partlyCloudy:
                {
                    partlyCloudy.SetActive(true);
                    sunny.SetActive(false);
                    currentState = weatherState.partlyCloudy;
                    isPartCloud = true;
                    isSunny = false;
                    break;
                }
                /*
            case weatherState.cloudy:
                {
                    break;
                }
            case weatherState.raining:
                {
                    break;
                }
            case weatherState.thundering:
                {
                    break;
                }
            case weatherState.foggy:
                {
                    break;
                }
                */
            default:
                {
                    break;
                }
        }
    }

    /// <summary>
    /// All weather disabled.
    /// </summary>
    public void DisableWeather()
    {
        sunny.SetActive(false);
        partlyCloudy.SetActive(false);
        /*
        cloudy.SetActive(false);
        rain.SetActive(false);
        thunderStorm.SetActive(false);
        fog.SetActive(false);
        */

        isSunny = false;
        isPartCloud = false;
        /*
        isCloudy = false;
        isRaining = false;
        isThundering = false;
        isFoggy = false;
        */
    }

    /// <summary>
    /// Changes the weather after a random period of time..
    /// </summary>
    public void NextWeather()
    {
        DisableWeather();

        int weatherCount = System.Enum.GetValues(typeof(weatherState)).Length;
        weatherState newState;
        do
        {
            newState = (weatherState)Random.Range(0, weatherCount);
        }
        while (newState == currentState);

        CurrentWeather(newState);
    }

    /// <summary>
    /// Get the current weather.
    /// </summary>
    /// <param name="currentWeather"></param>
    public void CurrentWeather(weatherState currentWeather)
    {
        currentState = currentWeather;
        WeatherSwitch();
    }
}
