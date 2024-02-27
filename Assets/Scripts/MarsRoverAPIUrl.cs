using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MarsRoverAPIUrl : MonoBehaviour
{
    public const string APIKey = "hL9xTB7A5ypvOH9xoWRJjQzqpWRGJbxjvVC3SDci";
    public const string url = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/latest_photos?api_key=" + APIKey;

    // Start is called before the first frame update
    public RawImage imageDisplay;

    [System.Serializable]
    private class MarRoverAPIResponse
    {
        public MarsRoverPhoto[] latest_photos; 
    }

    [System.Serializable]
    private class MarsRoverPhoto
    {
        public int id;
        public string earth_date;
        public string img_src;
    }
    private IEnumerator Start()
    {

        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("NASA API��û ����:" +webRequest.error);
               yield break;
            }

        MarRoverAPIResponse response = JsonUtility.FromJson<MarRoverAPIResponse>(webRequest.downloadHandler.text);

        if(response.latest_photos != null && response.latest_photos.Length>0)
        {
            MarsRoverPhoto latestPhoto = response.latest_photos[0];
            Debug.Log("�ֽ� ������ ID:" + latestPhoto.id);
            Debug.Log("�ֽ� ������ ��¥:" + latestPhoto.earth_date);
            Debug.Log("�ֽ� ������ URL:" + latestPhoto.img_src);

            StartCoroutine(LoadImage(latestPhoto.img_src));
        }
        else
        {
            Debug.Log("������ �����ϴ�.");
        }
    }

    private IEnumerator LoadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if(request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("�̹��� �ε� ����" + request.error);
            yield break;
        }

        Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        imageDisplay.texture = texture;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
