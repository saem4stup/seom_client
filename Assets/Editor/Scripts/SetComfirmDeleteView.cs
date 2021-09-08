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
        GameObject theView = GameObject.Find("AdditionalViews").transform.Find("ConfirmDelete").gameObject; // 새로 그릴 뷰 가져오기
        TextMeshProUGUI viewContentText = theView.transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>(); // 그릴 뷰에 적을 설명부분
        string name = ThisMarker.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text; //이름 가져오기: ㅇㅇㅇ님
        viewContentText.text = name + " 기억의 섬을\n삭제하시겠습니까?";
        ThisMarker.transform.Find("PinMenuView").gameObject.SetActive(false);
        theView.SetActive(true); // 뷰 띄우기s

        // 뷰에서 삭제하기 버튼이 눌리면
        Interactable deleteButton = theView.transform.Find("DeleteButton").gameObject.GetComponent<Interactable>();
        deleteButton.OnClick.AddListener(() => deleteMarker(theView));
        
    }
    private void deleteMarker(GameObject theView)
    {
        Destroy(ThisMarker);
        theView.SetActive(false);

    }
}
