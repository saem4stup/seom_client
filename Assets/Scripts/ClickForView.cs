using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


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
}
