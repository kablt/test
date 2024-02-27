using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Restconturies;
using UnityEngine.Networking;

public class OpenAPI : MonoBehaviour
{
    [System.Serializable]
    public class ApiResponse
    {
        public ResponseData response;
        public string resultCode;
        public string resultMessage;
    }

    [System.Serializable]
    public class ResponseData
    {
        public HeaderData header;
        public BodyData body;
    }

    [System.Serializable]
    public class HeaderData
    {
        public string resultCode;
        public string resultMsg;
    }

    [System.Serializable]
    public class BodyData
    {
        public int pageNo;
        public int totalCount;
        public ItemsData items;
        public int numOfRows;
    }

    [System.Serializable]
    public class ItemsData
    {
        public List<Item> item;
    }

    [System.Serializable]
    public class Item
    {
        // Define properties of your 'item' here
    }


    IEnumerator GetWeatherInfo()
    {
        string url = "http://apis.data.go.kr/1390000/SmartFarmdata/envdatarqstNZfFyMRJRcUz1lSF67zSSnrjG+PX5X0//hcAT0gfqgk7yUE1cdFe65d10FcM7JfuVmJ98VNnGXDmUvm4snHYYA==?pageSize=10&PageNo=1&searchFrmhsCode=Testfarm01&searchMeasDt=2019010100&returnType=json";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Error : {webRequest.error}");
            }
            else
            {
                ApiResponse DataArray = JsonUtility.FromJson<ApiResponse>(webRequest.downloadHandler.text);
                Debug.Log(webRequest.downloadHandler.text);

            }
        }

    }
    void Start()
    {
        StartCoroutine(GetWeatherInfo());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

