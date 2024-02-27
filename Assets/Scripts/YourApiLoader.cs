using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class YourApiLoader : MonoBehaviour
{
    private string apiUrl = "http://apis.data.go.kr/1390000/SmartFarmdata/envdatarqst?serviceKey=NZfFyMRJRcUz1lSF67zSSnrjG%2BPX5X0%2F%2FhcAT0gfqgk7yUE1cdFe65d10FcM7JfuVmJ98VNnGXDmUvm4snHYYA%3D%3D";

    void Start()
    {
        StartCoroutine(LoadApiData());
    }

    IEnumerator LoadApiData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // Process the API response here
                Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error loading API: " + webRequest.error);
            }
        }
    }
}
