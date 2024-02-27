using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.Playables;
using System.Collections.Generic;
/*
[System.Serializable]
public class MeasurementItem
{
    public float inTp;
    public float inHd;
    public string frmhsId;
    public float cunt;
    public float outWs;       // New variable
    public float daysuplyqy;  // New variable
}

[System.Serializable]
public class JsonResponse
{
    public Response response;
}

[System.Serializable]
public class Response
{
    public Body body;
}

[System.Serializable]
public class Body
{
    public Items items;
}

[System.Serializable]
public class Items
{
    public MeasurementItem[] item;
}

public class APICall : MonoBehaviour
{
    public string Id;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI text;
    private string apiUrl;
    public string formid;
 

    // Public properties to access values
    public float InTpValue { get; private set; }
    public float InHdValue { get; private set; }
    public string FrmhsIdValue { get; private set; }
    public float CuntValue { get; private set; }
    public float OutWsValue { get; private set; }       // New variable
    public float DaysuplyqyValue { get; private set; }  // New variable

    void Start()
    {
       
    }

    // Function to call when the button is clicked
    public void OnButtonClick()
    {
        Id = text.text;
        formid = Id.ToString() ;
        apiUrl = $"http://apis.data.go.kr/1390000/SmartFarmdata/envdatarqst?serviceKey=ndExmAZPa6Z1SBWydoZsH8RFcdL6XjiFlmZ4Qe0LVdu6WyGJJpkvYMB5ecMII4AIXi0P%2BYcuqLKslBw6ILFgbA%3D%3D&searchFrmhsCode={formid}&returnType=json";
        // Fetch API data when the button is clicked
        StartCoroutine(GetApiData());
    }

    IEnumerator GetApiData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string apiResponse = webRequest.downloadHandler.text;
                Debug.Log("API Response: " + apiResponse);

                JsonResponse jsonResponse = JsonUtility.FromJson<JsonResponse>(apiResponse);

                foreach (var measurementItem in jsonResponse.response.body.items.item)
                {
                    InTpValue = measurementItem.inTp;
                    InHdValue = measurementItem.inHd;
                    FrmhsIdValue = measurementItem.frmhsId;
                    CuntValue = measurementItem.cunt;
                    OutWsValue = measurementItem.outWs;          // Assign the new value
                    DaysuplyqyValue = measurementItem.daysuplyqy; // Assign the new value

                    Debug.Log($"inTp: {InTpValue}, inHd: {InHdValue}, frmhsId: {FrmhsIdValue}, cunt: {CuntValue}, outWs: {OutWsValue}, daysuplyqy: {DaysuplyqyValue}");

                    displayText.text = $"내부온도: {InTpValue}\n내부습도: {InHdValue}\n농가코드: {FrmhsIdValue}\n일 급액횟수: {CuntValue}\n외부 풍속: {OutWsValue}\n일 공급량: {DaysuplyqyValue}";
                }
            }
        }
    }
}
*/
