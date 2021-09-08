using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class MyBogeumjari : MonoBehaviour
{
    public string bogeumName;
    public bool madeByMyself;
    public Sprite profileImage;
    public int idx;
    public bool hasMarker;
    static Vector3[] marker_position = new Vector3[5] { new Vector3(320.0f, 80.0f, 0.0f), new Vector3(114.0f, 112.0f, 0.0f), new Vector3(-64.0f, 142.0f, 0.0f), new Vector3(-219.0f, 46.0f, 0.0f), new Vector3(-373.0f, -238.0f, 0.0f) };

    public GameObject list_element;
    public GameObject marker;

    private GameObject MakeListElement()
    {
        GameObject lst_element_prefab;

        if (madeByMyself)
            lst_element_prefab = Resources.Load("Prefabs/ListElementBOGEUMMine") as GameObject;
        else
            lst_element_prefab = Resources.Load("Prefabs/ListElementBOGEUMOthers") as GameObject;
        
        GameObject element = PrefabUtility.InstantiatePrefab(lst_element_prefab) as GameObject;
        element.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

        element.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = bogeumName; // �̸� ����
        
        // �̹��� ����
        element.transform.Find("ProfileImage").transform.Find("Image").GetComponent<Image>().sprite = profileImage;
        // ��ũ�Ѻ信 ����
        element.transform.parent = GameObject.Find("AdditionalViews").transform.Find("ListView").transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").transform;
        element.transform.localPosition = new Vector3(0f, 0f, 0f); // ��ġ ���ϱ�
        return element;
    }

    private GameObject MakeMarker(int marker_count)
    {
        GameObject marker_prefab;
        marker_prefab = Resources.Load("Prefabs/MarkerMine") as GameObject; // ���������� ����
        GameObject marker = PrefabUtility.InstantiatePrefab(marker_prefab) as GameObject;
        marker.transform.parent = null;
        marker.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        // �Ϻδ� ��Ŀ�ε� �߰�
        if (hasMarker)
        {
            marker.transform.parent = GameObject.Find("BaseElements").transform.Find("Markers").transform; // �θ� ���ϱ�
            marker.transform.localPosition = marker_position[marker_count]; // ��ġ ���ϱ�
            marker.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = bogeumName + "��"; // �̸� ���ϱ�
        }
        else
            marker = null;

        return marker;
    }

    public MyBogeumjari(string name, bool madeByMyself, Sprite profileImageTexture, int seomIdx, int bogeumListIdx)
    {
        bogeumName = name;
        this.madeByMyself = madeByMyself;
        profileImage = profileImageTexture;
        idx = seomIdx;
        if(bogeumListIdx< marker_position.Length)
        {
            hasMarker = true;
        }
        else
        {
            hasMarker = false;
        }
        list_element = MakeListElement();
        marker = MakeMarker(bogeumListIdx);
        marker.GetComponent<BogeumIdx>().bogeumIdx = idx;
    }
}
