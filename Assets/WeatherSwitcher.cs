using UnityEngine;

public class WeatherSwitcher : MonoBehaviour
{
    public WeatherManager[] locations;
    private int index = 0;

    public void NextLocation()
    {
        index++;
        if (index >= locations.Length)
            index = 0;

        locations[index].GetWeather();
    }
}
