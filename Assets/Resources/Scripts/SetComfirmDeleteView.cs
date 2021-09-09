using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class SetComfirmDeleteView : MonoBehaviour
{
    public GameObject ThisMarker;
    public void writeConfirmDeleteView()
    {
        GameObject theView = GameObject.Find("AdditionalViews").transform.Find("ConfirmDelete").gameObject; // ���� �׸� �� ��������
        TextMeshProUGUI viewContentText = theView.transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>(); // �׸� �信 ���� ����κ�
        string name = ThisMarker.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text; //�̸� ��������: ��������
        viewContentText.text = name + " ����� ����\n�����Ͻðڽ��ϱ�?";
        ThisMarker.transform.Find("PinMenuView").gameObject.SetActive(false);
        theView.SetActive(true); // �� ����s

        // �信�� �����ϱ� ��ư�� ������
        Interactable deleteButton = theView.transform.Find("DeleteButton").gameObject.GetComponent<Interactable>();
        deleteButton.OnClick.AddListener(() => deleteMarker(theView));
        
    }
    private void deleteMarker(GameObject theView)
    {
        Destroy(ThisMarker);
        theView.SetActive(false);

    }
}
