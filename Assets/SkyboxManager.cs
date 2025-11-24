using System;
using UnityEngine;

public class WeatherSkyboxManager : MonoBehaviour
{
    public WeatherManager weatherManager;

    public Material clearSkybox;
    public Material cloudySkybox;
    public Material rainSkybox;
    public Material thunderSkybox;
    public Material snowSkybox;
    public Material fogSkybox;

    public Light sunLight;

    public void ApplyWeather(string condition)
    {
        Material newSkybox = null;

        switch (condition)
        {
            case "Clear":
                newSkybox = clearSkybox;
                break;

            case "Clouds":
                newSkybox = cloudySkybox;
                break;

            case "Rain":
                newSkybox = rainSkybox;
                break;

            case "Thunderstorm":
                newSkybox = thunderSkybox;
                break;

            case "Snow":
                newSkybox = snowSkybox;
                break;

            case "Mist":
            case "Fog":
            case "Haze":
                newSkybox = fogSkybox;
                break;
        }

        if (newSkybox != null)
        {
            RenderSettings.skybox = newSkybox;
            Debug.Log("Skybox changed to: " + condition);
        }
        else
        {
            Debug.LogWarning("No skybox material assigned for condition: " + condition);
        }
    }
    public void SetSunPosition(int sunriseUnix, int sunsetUnix, int timezoneOffset)
    {
        // 
        DateTime sunrise = DateTimeOffset.FromUnixTimeSeconds(sunriseUnix + timezoneOffset).DateTime;
        DateTime sunset = DateTimeOffset.FromUnixTimeSeconds(sunsetUnix + timezoneOffset).DateTime;
        DateTime now = DateTime.UtcNow.AddSeconds(timezoneOffset);

        // How far through the day are we?
        float totalDaySeconds = (float)(sunset - sunrise).TotalSeconds;
        float elapsedSeconds = (float)(now - sunrise).TotalSeconds;

        // Clamp so we stay in range
        float t = Mathf.InverseLerp(0f, totalDaySeconds, elapsedSeconds);

        // Convert 0–1 progress to a sun angle (0 = sunrise, 180 = sunset)
        float sunAngle = Mathf.Lerp(0f, 180f, t);

        // Apply rotation (sun travels on X axis)
        sunLight.transform.rotation = Quaternion.Euler(sunAngle - 90f, 0f, 0f);
    }
}
