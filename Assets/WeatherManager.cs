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

        Debug.Log("Requesting: " + url);

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

            WeatherResponse data = JsonUtility.FromJson<WeatherResponse>(request.downloadHandler.text);
            string condition = data.weather[0].main;

            weatherSkyboxManager.ApplyWeather(condition);
        }
    }
}

