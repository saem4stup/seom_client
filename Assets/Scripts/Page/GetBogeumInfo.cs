using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;

[System.Serializable]
public class RequestingResult_Bogeum
{ 
    public string status;
    public bool success;
    public string message;
    public BogeumData data;
}

[System.Serializable]
public class BogeumData
{
    public IslandInfo[] islandInfo;
    public ContentsInfo[] contentsInfo;
}
[System.Serializable]
public class IslandInfo
{
    public int islandIdx;
    public string deceasedProfileImg;
    public string deceasedName;
    public string deceasedBirth;
    public string deceasedDeath;
    public int likes;
    public bool isAlreadyBookmark;
}

[System.Serializable]
public class ContentsInfo
{
    public int contentsIdx;
    public string contentsImg;
    public int likes;
    public string createDate;
    public int commentCount;
}



public class GetBogeumInfo : MonoBehaviour
{
    private string uri = "http://15.165.223.53:3000/memory/v1/memories/";
    private string contentUri= "http://15.165.223.53:3000/memory/v1/contents/";
    public int user_idx = 30;
    public int island_idx = 16;

    public BogeumData bogeumData;

    public TextMeshProUGUI bogeumName;
    public TextMeshProUGUI bogeumBirthDeathDate;
    public TextMeshProUGUI bogeumBookmarks;
    public GameObject fullStar;
    public GameObject blankStar;
    public Transform parent_content;
    public RawImage bogeumImage;
    
    private List<GameObject> memoryArray;
    private GameObject memoryPrefab;
    private GameObject tmp;
    private int numOfMemory;
    public void Start()
    {
        memoryPrefab = Resources.Load<GameObject>("a_memory");
        starttestGET();

    }
    public void starttestGET()
    {
        //StopCoroutine(testGET());
        StartCoroutine(testGET());
    }
    

    
    public IEnumerator testGET()
    {
        uri += user_idx;
        uri += "/";
        uri += island_idx;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            //bogeumData = JsonConvert.DeserializeObject<BogeumData>(webRequest.downloadHandler.text);
            RequestingResult_Bogeum requestingResult = JsonUtility.FromJson<RequestingResult_Bogeum>(webRequest.downloadHandler.text);
            bogeumData = requestingResult.data;
            Debug.Log(webRequest.downloadHandler.text);
            
            UnityWebRequest ImgWWW = UnityWebRequestTexture.GetTexture(bogeumData.islandInfo[0].deceasedProfileImg);
            yield return ImgWWW.SendWebRequest();
            bogeumImage.texture = ((DownloadHandlerTexture)ImgWWW.downloadHandler).texture;

            bogeumName.text = bogeumData.islandInfo[0].deceasedName +" 님의 보금자리";
            bogeumBirthDeathDate.text = bogeumData.islandInfo[0].deceasedBirth + " ~ " + bogeumData.islandInfo[0].deceasedDeath;
            bogeumBookmarks.text = bogeumData.islandInfo[0].likes.ToString();
            if (bogeumData.islandInfo[0].isAlreadyBookmark == true)
            {
                fullStar.SetActive(true);
                blankStar.SetActive(false);
            }
            else
            {
                fullStar.SetActive(false);
                blankStar.SetActive(true);

            }
            numOfMemory = bogeumData.contentsInfo.Length;
           
            if (numOfMemory > 0)
            {
                for (int i = 0; i < numOfMemory; i++)
                {
                    tmp = Instantiate(memoryPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tmp.transform.parent = parent_content;
                    tmp.transform.localScale = new Vector3(1, 1, 1);
                    tmp.transform.localPosition = Vector3.zero;
                    tmp.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = bogeumData.contentsInfo[i].createDate;
                    tmp.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = bogeumData.contentsInfo[i].commentCount.ToString();
                    tmp.transform.GetChild(3).GetComponent<ContentIdx>().thisIdx = bogeumData.contentsInfo[i].contentsIdx;
                    UnityWebRequest ContentsImgwww = UnityWebRequestTexture.GetTexture(bogeumData.contentsInfo[i].contentsImg);
                    yield return ContentsImgwww.SendWebRequest();
                    tmp.transform.GetChild(2).transform.GetChild(0).GetComponent<RawImage>().texture= ((DownloadHandlerTexture)ContentsImgwww.downloadHandler).texture;
                    //memoryArray.Add(tmp);
                }

            }
            //Debug.Log(bogeumData.islandInfo[0].deceasedName);
            
        }

    }
}
