[System.Serializable]
public class WeatherResponse
{
    public WeatherInfo[] weather;
    public MainInfo main;
    public string name;
    public int timezone;
    public SysInfo sys;
}

[System.Serializable]
public class WeatherInfo
{
    public string main;
    public string description;
}

[System.Serializable]
public class MainInfo
{
    public float temp;
}
[System.Serializable]
public class SysInfo
{
    public int sunrise;
    public int sunset;
}