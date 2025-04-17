using UnityEngine;

public class WeatherCycle : MonoBehaviour
{
    [Header("Weather Types")]
    public GameObject sunny;
    public GameObject partlyCloudy;
    public GameObject cloudy;
    public GameObject fog;
    public GameObject rain;
    public GameObject thunderStorm;

    public bool isSunny = false;
    public bool isPartCloud = false;
    public bool isCloudy = false;
    public bool isFoggy = false;
    public bool isRaining = false;
    public bool isThundering = false;
    public float timer = 0;
    public float weatherChange = 10;
    public int randomSwitch;
    public enum weatherState { sunny, partlyCloudy, raining, thundering, cloudy, foggy}
    public weatherState currentState;

    public void Awake()
    {
        CurrentWeather(weatherState.sunny);
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= weatherChange)
        {
            NextWeather();
            timer = 0;
        }
    }

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
            default:
                {
                    sunny.SetActive(true);
                    isSunny = true;
                    currentState = weatherState.sunny;
                    break;
                }
        }
    }

    public void DisableWeather()
    {
        sunny.SetActive(false);
        partlyCloudy.SetActive(false);
        cloudy.SetActive(false);
        rain.SetActive(false);
        thunderStorm.SetActive(false);
        fog.SetActive(false);

        isSunny = true;
        isCloudy = false;
        isPartCloud = false;
        isRaining = false;
        isThundering = false;
        isFoggy = false;
    }

    public void NextWeather()
    {
        randomSwitch = Random.Range(10, 15);
        WeatherSwitch();
    }

    public void CurrentWeather(weatherState currentWeather)
    {
        currentState = currentWeather;
        WeatherSwitch();
    }
}
