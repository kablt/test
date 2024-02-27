using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DropdownM : MonoBehaviour
{
    public TMP_Dropdown parentDropdown; // Parent Dropdown 변수
    public TMP_Dropdown childDropdown;  // Child Dropdown 변수

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
}