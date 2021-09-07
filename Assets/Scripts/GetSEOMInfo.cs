using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class RequestingResult
{
    public string status;
    public bool success;
    public string message;
    public UserData data;
}

[System.Serializable]
public class UserData
{
    public string name;
    public List<DeceasedInfo> deceasedInfo;
}
[System.Serializable]
public class DeceasedInfo
{
    public int islandIdx;
    public string deceasedName;
    public string deceasedProfileImg;
    public int madeByMyself;
    public Texture profileTexture;
}

public class GetSEOMInfo : MonoBehaviour
{
    private string uri = "http://15.165.223.53:3000/main/v1/";
    public UserData userData;
    
    public int user_idx;
    public List<MyBogeumjari> Bogeums;

    public void Start()
    {     
        StartCoroutine(GetUserInfo()); // ����� ���� ������
    }
    IEnumerator GetUserInfo()
    {
        /*����ڿ� ���� ���� ������*/
        string uri = this.uri + user_idx ;
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            RequestingResult requestingResult = JsonUtility.FromJson<RequestingResult>(webRequest.downloadHandler.text);
            userData = requestingResult.data;
        }

        /*������� ����� �� ���� ����*/
        GameObject.Find("MySEOM").GetComponent<TextMeshProUGUI>().text = userData.name + "���� ����� ��";
        StartCoroutine(GetBogeumProfileInfo()); // ����ڰ� ����� �����ڸ��� ������ ���� ������
    }
    IEnumerator GetBogeumProfileInfo()
    {
        /*����ڿ� ���� ��������, �� �����ڸ��� ���� ������*/
        for (int i = 0; i < userData.deceasedInfo.Count; i++)
        {
            string image_uri = userData.deceasedInfo[i].deceasedProfileImg;
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(image_uri))
            {
                yield return webRequest.SendWebRequest();
                
                userData.deceasedInfo[i].profileTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture; // �̹��� �ٿ�ε�
            }
        }

        MakeBogeumList();
    }

    void MakeBogeumList()
    {        
        /*�����ڸ� ����Ʈ �����, ��Ŀ �� ����Ʈ ���� �ֱ�*/
        for (int i = 0; i < userData.deceasedInfo.Count; i++)
        {
            
            bool madeMyself = userData.deceasedInfo[i].madeByMyself == 1 ? true : false;
            Bogeums.Add(new MyBogeumjari(userData.deceasedInfo[i].deceasedName, madeMyself, Texture2Sprite(userData.deceasedInfo[i].profileTexture), userData.deceasedInfo[i].islandIdx, i));
        }
    }

    public void DeleteBogeum(int target_bogeum_idx)
    {
        int delete_listidx = -1;
        for (int i = 0; i < Bogeums.Count; i++)
        {
            if(Bogeums[i].idx == target_bogeum_idx)
            {
                delete_listidx = i;
                break;
            }
        }
        
        if(delete_listidx > 0)
        {
            Destroy(Bogeums[delete_listidx].list_element);
            if (Bogeums[delete_listidx].marker != null)
            {
                Destroy(Bogeums[delete_listidx].marker);
            }
            Bogeums.RemoveAt(delete_listidx);
        }

        // ������ ���� ��û
        uri = this.uri + user_idx + "/" + target_bogeum_idx;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.SendWebRequest();
            Debug.Log("�������\n" + webRequest.downloadHandler.text);
        }
    }

    Sprite Texture2Sprite(Texture texture)
    {
        Texture2D texture2D = Texture2D.CreateExternalTexture(texture.width, texture.height, TextureFormat.RGB24, false, false, texture.GetNativeTexturePtr());
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        return Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f));
    }

    public void Awake()
    {
        // �ش� ��ü�� �������� �ʰ� �صα� --> ���߿� ��ȣ dont destroy ��ü ������ �̺κ��� �����
        DontDestroyOnLoad(GameObject.Find("SEOMInfo"));
    }
}
