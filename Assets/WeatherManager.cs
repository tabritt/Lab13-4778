using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public abstract class WeatherManager : MonoBehaviour
{
    public WeatherSkyboxManager weatherSkyboxManager;

    private const string apiBase = "https://api.openweathermap.org/data/2.5/weather";
    private const string apiKey = "63ff9c310694960d0bdbc738e30f9d13";

    public abstract float latitude { get; }
    public abstract float longitude { get; }

    public void GetWeather()
    {
        StartCoroutine(GetWeatherRoutine());
    }

    private IEnumerator GetWeatherRoutine()
    {
        string url = $"{apiBase}?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";
        Debug.Log("Requesting URL: " + url);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Network error: " + request.error);
                yield break;
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("HTTP error: " + request.responseCode);
                yield break;
            }

            Debug.Log("Weather API Response: " + request.downloadHandler.text);

            WeatherResponse data = JsonUtility.FromJson<WeatherResponse>(request.downloadHandler.text);

            if (data != null && data.weather.Length > 0)
            {
                string condition = data.weather[0].main;
                Debug.Log($"Weather Condition: {condition}, Temperature: {data.main.temp}°C");

                // Apply skybox based on weather
                weatherSkyboxManager.ApplyWeather(condition);

                // Set sun position based on sunrise, sunset, and timezone
                weatherSkyboxManager.SetSunPosition(data.sys.sunrise, data.sys.sunset, data.timezone);
            }
            else
            {
                Debug.LogWarning("Weather data is empty or invalid!");
            }
        }
    }

}

