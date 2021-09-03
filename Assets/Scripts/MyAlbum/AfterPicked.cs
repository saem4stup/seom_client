using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterPicked : MonoBehaviour
{
    public GameObject LastPickedThing; // Top, Bottom�� ��

    public void PickingCompleted()
    {
        GameObject.Find("ProfilePhoto").GetComponent<Image>().sprite 
            = LastPickedThing.transform.Find("Photo").GetComponent<Image>().sprite;
    }

}
