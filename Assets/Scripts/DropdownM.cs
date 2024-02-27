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
    

    public TMP_Dropdown parentDropdown; // Parent Dropdown ����
    public TMP_Dropdown childDropdown;  // Child Dropdown ����
    public TMP_Text label;
    

    // Parent Dropdown���� ������ �ɼǿ� ���� Child Dropdown�� �ɼ��� ������Ʈ�ϴ� �Լ�
    public void UpdateChildDropdownOptions()
    {
        // Parent Dropdown���� ���õ� �ɼǿ� ���� Child Dropdown�� �ɼ��� ����
        switch (parentDropdown.value)
        {
            case 0: // �泲 ������ ���
                SetChildDropdownOptions(new string[] { "������ �������ּ���." });
                break;
            case 1: // �泲 ������ ���
                SetChildDropdownOptions(new string[] { "����", "��õ", "â��", "�Ծ�" });
                break;
            case 2: // ���� ������ ���
                SetChildDropdownOptions(new string[] { "����", "����", "���", "����", "����", "ȭ��" });
                break;
            case 3: // ���� ������ ���
                SetChildDropdownOptions(new string[] { "����", "����", "����", "�ͻ�", "����", "����" });
                break;
            default: // �ٸ� ���
                SetChildDropdownOptions(new string[] { });
                break;
        }
    }

    // Child Dropdown�� �ɼ��� �����ϴ� �Լ�
    void SetChildDropdownOptions(string[] options)
    {
        childDropdown.ClearOptions(); // ���� �ɼ��� ��� ����
        childDropdown.AddOptions(new List<string>(options)); // ���ο� �ɼ��� �߰�
    }

    // Start �Լ����� Parent Dropdown�� �� ���� �̺�Ʈ�� UpdateChildDropdownOptions �Լ��� ����
    void Start()
    {
        parentDropdown.onValueChanged.AddListener(delegate {
            UpdateChildDropdownOptions();
        });

        // �ʱ⿡�� �ѹ��� ȣ���Ͽ� Child Dropdown�� ������Ʈ
        UpdateChildDropdownOptions();
    }
    public void OnButtonClick()
    {
        label = childDropdown.captionText;
        if(label.text =="����")
        {
            Id = 50;
            formid = Id.ToString();
        }
        else if(label.text =="��õ")
        {
            Id = 48;
            formid = Id.ToString();
        }
        else if(label.text == "â��")
        {
            Id = 53;
            formid = Id.ToString();
        }
        else if (label.text == "�Ծ�")
        {
            Id = 56;
            formid = Id.ToString();
        }

        else if (label.text == "����")
        {
            Id = 45;
            formid = Id.ToString();
        }
        else if (label.text == "����")
        {
            Id = 42; // ��ã�Ƽ� ���� �ڵ� ����
            formid = Id.ToString();
        }
        else if (label.text == "���")
        {
            Id = 39;
            formid = Id.ToString();
        }
        else if (label.text == "����")
        {
            Id = 44;
            formid = Id.ToString();
        }
        else if (label.text == "����")
        {
            Id = 42;
            formid = Id.ToString();
        }
        else if (label.text == "ȭ��")
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

                    displayText.text = $"���οµ�: {InTpValue}\n���ν���: {InHdValue}\n���ڵ�: {FrmhsIdValue}\n�� �޾�Ƚ��: {CuntValue}\n�ܺ� ǳ��: {OutWsValue}\n�� ���޷�: {DaysuplyqyValue}";
                }
            }
        }
    }

}