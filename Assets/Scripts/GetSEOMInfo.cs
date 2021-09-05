using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
class RequestingResult
{
    public string status;
    public bool success;
    public string message;
    public UserData data;
}

[System.Serializable]
class UserData
{
    public string name;
    public DeceasedInfo[] deceasedInfo;
}
[System.Serializable]
class DeceasedInfo
{
    public int islandIdx;
    public string deceasedName;
    public string deceasedProfilineImg;
    public int madeByMyself;
}

public class GetSEOMInfo : MonoBehaviour
{
    private string uri = "http://15.165.223.53:3000/main/v1/";
    public int user_idx;
    UserData userData; 

    public void Start()
    {     
        StartCoroutine(testGET());
    }
    public IEnumerator testGET()
    {
        uri += user_idx ;
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            RequestingResult requestingResult = JsonUtility.FromJson<RequestingResult>(webRequest.downloadHandler.text);
            userData = requestingResult.data;
        }
        
    }
    public void Awake()
    {
        // �ش� ��ü�� �������� �ʰ� �صα� --> ���߿� ��ȣ dont destroy ��ü ������ �̺κ��� �����
        DontDestroyOnLoad(GameObject.Find("SEOMInfo"));
    }
}
