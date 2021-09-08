using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

[System.Serializable]
public class SearchRequestingResult
{
    public string status;
    public bool success;
    public string message;
    public List<SearchElementInfo> data;
}
[System.Serializable]
public class SearchElementInfo
{
    public int islandIdx;
    public string deceasedProfileImg;
    public string deceasedName;
    public string deceasedBirth;
    public string deceasedDeath;
    public string id;
    public int likes;

    Sprite profileImage;
    public string getImageURL() { return deceasedProfileImg; }
    public void setSprite(Texture texture)
    {
        profileImage = GetSEOMInfo.Texture2Sprite(texture);
    }
    public Sprite getSprite() { return profileImage; }
    public string getName() { return deceasedName; }
    public string getBirth() { return deceasedBirth; }
    public string getDeath() { return deceasedDeath; }
    public string getlikes() { return likes.ToString(); }
    public string getGuardName() { return id; }
    public bool isLiked()
    {
        bool liked = false;
        List<int> iLikedBogeums = new List<int>();
        List<MyBogeumjari> bogeums = GameObject.Find("SEOMInfo").transform.GetComponent<GetSEOMInfo>().Bogeums;

        for (int i = 0; i < bogeums.Count; i++)
        {
            iLikedBogeums.Add(bogeums[i].idx);
        }

        if (iLikedBogeums.Contains(islandIdx))
            liked = true;
        else
            liked = false;

        return liked;
    }
}


public class SearchManager : MonoBehaviour
{
    private string uri = "http://15.165.223.53:3000/main/v1/island?deceased_name=";
    private List<SearchElementInfo> searchData;
    private List<GameObject> SearchElementsObjects;

    
    public void Researching()
    {
        StartCoroutine(GetSearchResults());
    }
    IEnumerator GetSearchResults()
    {
        string name = GameObject.Find("SearchResultWindow").transform.Find("MRKeyboardInputField_TMP").GetComponent<TMP_InputField>().text;
        /*검색 결과를 서버에서 가져오기*/
        // name = WWW.EscapeURL(name, System.Text.Encoding.UTF8);
        string uri = this.uri + name;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            Debug.Log(uri);
            Debug.Log(webRequest.downloadHandler.text);
            SearchRequestingResult requestingResult = JsonUtility.FromJson<SearchRequestingResult>(webRequest.downloadHandler.text);
            searchData = requestingResult.data;
        }

        /*서버에서 각 프로필 사진 가져오기*/
        StartCoroutine(GetSearchProfileInfo());
    }

    IEnumerator GetSearchProfileInfo()
    {
        /*사용자에 대한 정보에서, 각 보금자리의 프사 얻어오기*/
        Texture[] textures = new Texture[searchData.Count];
        for (int i = 0; i < searchData.Count; i++)
        {
               string image_url = searchData[i].getImageURL();
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(image_url))
            {
                yield return webRequest.SendWebRequest();
                textures[i] = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            }
        }

        /* 프사 텍스처를 스프라이트로 */
        for(int i = 0; i<searchData.Count; i++)
        {
            searchData[i].setSprite(textures[i]);
        }
        /*검색 결과로, 리스트에 들어갈 게임 오브젝트 만들기*/
        for (int i = 0; i < searchData.Count; i++)
        {
            SearchElementsObjects.Add(MakeSearchElement(searchData[i]));
        }   
    }

    GameObject MakeSearchElement(SearchElementInfo info)
    {
        GameObject element_prefab = Resources.Load("Prefabs/SearchResultElement") as GameObject;
        GameObject element = PrefabUtility.InstantiatePrefab(element_prefab) as GameObject;
        element.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        element.transform.localPosition = new Vector3(0f, 0f, 0f);

        element.transform.Find("ProfileImageMasking").transform.Find("ProfileImage").GetComponent<Image>().sprite = info.getSprite();
        element.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = info.getName() + " 님의 보금자리";
        element.transform.Find("LivePeriod").GetComponent<TextMeshProUGUI>().text = info.getBirth() + " ~ " + info.getDeath();
        element.transform.Find("GuardName").GetComponent<TextMeshProUGUI>().text = info.getGuardName();
        element.transform.Find("StarCount").GetComponent<TextMeshProUGUI>().text = info.getlikes();
        if (info.isLiked())
        {
            element.transform.Find("Star").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Icons/StarFull");
        }


        element.transform.parent = transform.Find("Result").transform.Find("Viewport").transform.Find("Content").transform;
        element.transform.localPosition = new Vector3(0f, 0f, 0f);
        return element;
    }
}
