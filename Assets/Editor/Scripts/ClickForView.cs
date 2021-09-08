using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class ClickForView : MonoBehaviour
{
    public GameObject view;
    public GameObject button;
    public Sprite unclicked_image;
    public Sprite clicked_image;

    public void showView()
    {
        /*GameObject myobject = EventSystem.current.currentSelectedGameObject;
        Debug.Log(myobject.name);*/
        if (view.activeSelf == true)
        {
            // 뷰가 보이는 상황
            view.SetActive(false);
        }
        else if (view.activeSelf == false)
        {
            view.SetActive(true);
        }
    }

    public void buttonColorChange()
    {
        if (view.activeSelf == true)
        {
            // 뷰가 보이는 상황
            button.GetComponent<SpriteRenderer>().sprite = clicked_image;
        }
        else if (view.activeSelf == false)
        {
            button.GetComponent<SpriteRenderer>().sprite = unclicked_image;
        }
    }

    public void withProfileButton()
    {
        if(view.activeSelf == true)
        {
            view.SetActive(false);
            /*GameObject.Find("MySEOM").GetComponent<TextMeshProUGUI>().text = UserSEOMInfo.USERNAME + "님의 기억의 섬";*/
            GameObject.Find("MapButton").GetComponent<SpriteRenderer>().sprite = clicked_image;
            button.GetComponent<SpriteRenderer>().sprite = unclicked_image;
            GameObject.Find("Background").transform.Find("Island").gameObject.SetActive(true);
            GameObject.Find("BaseElements").transform.Find("Markers").gameObject.SetActive(true);

        }
        else
        {
            view.SetActive(true);
            /*GameObject.Find("MySEOM").GetComponent<TextMeshProUGUI>().text = UserSEOMInfo.USERNAME + "님의 정보";*/
            GameObject.Find("MapButton").GetComponent<SpriteRenderer>().sprite = unclicked_image;
            button.GetComponent<SpriteRenderer>().sprite = clicked_image;
            GameObject.Find("Island").SetActive(false);
            GameObject.Find("Markers").SetActive(false);

        }
    }
    public void withSearchButton()
    {
        if (view.activeSelf == true)
        {
            view.SetActive(false); // 뷰 닫힘
            GameObject.Find("BaseElements").transform.Find("Markers").gameObject.SetActive(true); // 마커 보임
        }
        else
        {
            view.SetActive(true); // 뷰 열림
            GameObject.Find("Markers").SetActive(false);
        }
    }
 
}
