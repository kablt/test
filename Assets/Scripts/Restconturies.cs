using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using UnityEngine.UI;
using static Restconturies;
using UnityEditor.PackageManager.Requests;

public class Restconturies : MonoBehaviour
{
    public TMP_Text countryNameText;
    public TMP_Text capitalText;
    public TMP_Text populationText;

    [System.Serializable]
    public class CountryDataArrary
    {
        public List<CountryData> countries;
    }
    [System.Serializable]
    public class CountryData
    {
        public Name name;
        public string[] capital;
        public int population;
    }

    [System.Serializable]
    public class Name
    {
        public string common;
        public string official;

    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetWeatherInfo());
    }
    IEnumerator GetWeatherInfo()
    {
        string url = $"https://restcountries.com/v3.1/name/south%20korea";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Error : {webRequest.error}");
            }
            else
            {
                CountryDataArrary countryDataArray = JsonUtility.FromJson<CountryDataArrary>("{\"countries\":" + webRequest.downloadHandler.text + "}");
                UpdateCountryInfo(countryDataArray.countries[0]);
           
            }
        }

    }
    void UpdateCountryInfo(CountryData countryData)
    {
        countryNameText.text = "Name: " + countryData.name.official;
        capitalText.text = "Capital: " + countryData.capital[0];
        populationText.text = "Population: " + countryData.population.ToString();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
