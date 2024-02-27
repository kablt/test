using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DropdownM : MonoBehaviour
{
    public TMP_Dropdown parentDropdown; // Parent Dropdown ����
    public TMP_Dropdown childDropdown;  // Child Dropdown ����

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
}