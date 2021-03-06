using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
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
        StartCoroutine(GetUserInfo()); // 사용자 정보 얻어오기
    }
    IEnumerator GetUserInfo()
    {
        /*사용자에 대한 정보 얻어오기*/
        string uri = this.uri + user_idx ;
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {

            webRequest.chunkedTransfer = false;
            yield return webRequest.SendWebRequest();
            userData = JsonConvert.DeserializeObject<UserData>(webRequest.downloadHandler.text);
            RequestingResult requestingResult = JsonUtility.FromJson<RequestingResult>(webRequest.downloadHandler.text);
            userData = requestingResult.data;
            Debug.Log(userData.deceasedInfo[0].deceasedName);
       
        }

        /*사용자의 기억의 섬 제목 띄우기*/
        GameObject.Find("MySEOM").GetComponent<TextMeshProUGUI>().text = userData.name + "님의 기억의 섬";
        StartCoroutine(GetBogeumProfileInfo()); // 사용자가 등록한 보금자리의 프로필 정보 얻어오기
    }
    IEnumerator GetBogeumProfileInfo()
    {
        /*사용자에 대한 정보에서, 각 보금자리의 프사 얻어오기*/
        for (int i = 0; i < userData.deceasedInfo.Count; i++)
        {
            string image_uri = userData.deceasedInfo[i].deceasedProfileImg;
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(image_uri))
            {   webRequest.chunkedTransfer = false;
                yield return webRequest.SendWebRequest();
                
                userData.deceasedInfo[i].profileTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture; // 이미지 다운로드
            }
        }

        MakeBogeumList();
    }

    void MakeBogeumList()
    {        
        /*보금자리 리스트 만들고, 마커 및 리스트 내용 넣기*/
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

        // 서버에 삭제 요청
        uri = this.uri + user_idx + "/" + target_bogeum_idx;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.chunkedTransfer = false;
            webRequest.SendWebRequest();
            Debug.Log("삭제결과\n" + webRequest.downloadHandler.text);
        }
    }

    public static Sprite Texture2Sprite(Texture texture)
    {
        Texture2D texture2D = Texture2D.CreateExternalTexture(texture.width, texture.height, TextureFormat.RGB24, false, false, texture.GetNativeTexturePtr());
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        return Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f));
    }

    private void SetProfile()
    {
        GameObject profileView = GameObject.Find("AdditionalViews").transform.Find("ProfileView").gameObject;
        profileView.transform.Find("Text").transform.Find("Username").transform.GetComponent<TextMeshProUGUI>().text = userData.name;

    }

    public void Awake()
    {
        user_idx = DataSaver.instance.userIdx;
    }
}
