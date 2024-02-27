using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


public class WeatherAPIScript : MonoBehaviour
{
    private readonly string apikey = "59cd07632c3df8c7c96fc7d5a73a7407";
    private readonly string city = "Seoul";
    [SerializeField] private TMP_Text uiText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetWeatherInfo());
    }
    IEnumerator GetWeatherInfo()
    {
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apikey}&units=metric";
        
        using(UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if(webRequest.result== UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"Error : {webRequest.error}");
            }
            else
            {
                processWeatherInfo(webRequest.downloadHandler.text);
            }
        }
    }

    private void processWeatherInfo(string jsonData)
    {
        //json파일을 전체를 불러온것
        //Debug.Log($"Recive Weather Information : {jsonData}");
        WeatherResponse response = JsonUtility.FromJson<WeatherResponse>(jsonData);

        //콘솔에 날씨 정보 출력
        Debug.Log($"City : {response.name}");
        Debug.Log($"Temperature : {response.main.temp}*C");
        Debug.Log($"Weather : {response.weather[0].main}");
        Debug.Log($"Weather : {response.weather[0].icon}");
        Debug.Log($"Description : {response.weather[0].description}");
        Debug.Log("city" + response.name);
        string outputOne = $"City: {response.name}";
        uiText.text = $"도시: {response.name}, 온도 : {response.main.temp}, 날씨 : {response.weather[0].main},  \n 설명 : {response.weather[0].description} \n";
        foreach(var item in response.weather)
        {
            outputOne += $"state : {item.main}, sub : {item.description}\n";
        }

    }
    //json 응답을 위한 클래스, 홈페이지를 참고하여 만든다
    [System.Serializable]
    public class WeatherResponse
    {
        public string name;
        public Weather[] weather;
        public WeatherData main;
    }

    [System.Serializable]
    public class WeatherData
    {
        public float temp;
    }

    [System.Serializable]
    public class Weather
    {
        public string main;
        public string description;
        public string icon;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
