using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using UnityEngine.Networking;
using System.Reflection.Emit;


[System.Serializable]
public class MeasurementItem
{
    public float inTp;
    public float inHd;
    public string frmhsId;
    public float cunt;
    public float outWs;
    public float daysuplyqy;
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


public class DropdownM : MonoBehaviour
{
    public float Id;
    public TextMeshProUGUI displayText;
    private string apiUrl;
    public string formid;


    // Public properties to access values
    public float InTpValue { get; private set; }
    public float InHdValue { get; private set; }
    public string FrmhsIdValue { get; private set; }
    public float CuntValue { get; private set; }
    public float OutWsValue { get; private set; }
    public float DaysuplyqyValue { get; private set; }
    

    public TMP_Dropdown parentDropdown; // Parent Dropdown 변수
    public TMP_Dropdown childDropdown;  // Child Dropdown 변수
    public TMP_Text label;
    

    // Parent Dropdown에서 선택한 옵션에 따라 Child Dropdown의 옵션을 업데이트하는 함수
    public void UpdateChildDropdownOptions()
    {
        // Parent Dropdown에서 선택된 옵션에 따라 Child Dropdown의 옵션을 설정
        switch (parentDropdown.value)
        {
            case 0: // 경남 선택한 경우
                SetChildDropdownOptions(new string[] { "지역을 선택해주세요." });
                break;
            case 1: // 경남 선택한 경우
                SetChildDropdownOptions(new string[] { "김해", "사천", "창녕", "함안" });
                break;
            case 2: // 전남 선택한 경우
                SetChildDropdownOptions(new string[] { "강진", "고흥", "담양", "보성", "장흥", "화순" });
                break;
            case 3: // 전북 선택한 경우
                SetChildDropdownOptions(new string[] { "군산", "김제", "완주", "익산", "정읍", "진안" });
                break;
            default: // 다른 경우
                SetChildDropdownOptions(new string[] { });
                break;
        }
    }

    // Child Dropdown의 옵션을 설정하는 함수
    void SetChildDropdownOptions(string[] options)
    {
        childDropdown.ClearOptions(); // 기존 옵션을 모두 제거
        childDropdown.AddOptions(new List<string>(options)); // 새로운 옵션을 추가
    }

    // Start 함수에서 Parent Dropdown의 값 변경 이벤트에 UpdateChildDropdownOptions 함수를 연결
    void Start()
    {
        parentDropdown.onValueChanged.AddListener(delegate {
            UpdateChildDropdownOptions();
        });

        // 초기에도 한번은 호출하여 Child Dropdown을 업데이트
        UpdateChildDropdownOptions();
    }
    public void OnButtonClick()
    {
        label = childDropdown.captionText;
        if(label.text =="김해")
        {
            Id = 50;
            formid = Id.ToString();
        }
        else if(label.text =="사천")
        {
            Id = 48;
            formid = Id.ToString();
        }
        else if(label.text == "창녕")
        {
            Id = 53;
            formid = Id.ToString();
        }
        else if (label.text == "함안")
        {
            Id = 56;
            formid = Id.ToString();
        }

        else if (label.text == "강진")
        {
            Id = 45;
            formid = Id.ToString();
        }
        else if (label.text == "고흥")
        {
            Id = 42; // 못찾아서 장흥 코드 넣음
            formid = Id.ToString();
        }
        else if (label.text == "담양")
        {
            Id = 39;
            formid = Id.ToString();
        }
        else if (label.text == "보성")
        {
            Id = 44;
            formid = Id.ToString();
        }
        else if (label.text == "장흥")
        {
            Id = 42;
            formid = Id.ToString();
        }
        else if (label.text == "화순")
        {
            Id = 204;
            formid = Id.ToString();
        }
        else
        {
            Id = 207;
            formid = Id.ToString();
        }

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