using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class RequestingResult_Memory
{
    public string status;
    public bool success;
    public string message;
    public MemoryInfo[] data;
}


[System.Serializable]
public class MemoryInfo
{
    public int contentsIdx;
    public string contentsImg;
    public string createDate;
    public string memo;
    public int likes;
    public bool isAlreadyLikes;
}

public class ShowDetailMemory : MonoBehaviour
{
    private string contentUri = "http://15.165.223.53:3000/memory/v1/contents/";
    private string tmpUri = "";
    public int user_idx = 30;
    public int island_idx = 16;
    public MemoryInfo[] memoryInfo;
    
    public GameObject detailMemoryImg;
    public GameObject fullHeart;
    public GameObject blankHeart;
    public TextMeshProUGUI detailMemoryDate;
    public TextMeshProUGUI detailMemoryMemo;
    public TextMeshProUGUI detailMemoryLikes;
    private GameObject Canvas_parent;
    private string detailMemoryImgPath;
    private void Start()
    {
        memoryInfo = new MemoryInfo[1];
        detailMemoryImg = GameObject.Find("Canvas_detailMemory_Parent").transform.Find("Canvas_detailMemory").transform.Find("MemoryImg").gameObject;
        detailMemoryDate = GameObject.Find("Canvas_detailMemory_Parent").transform.Find("Canvas_detailMemory").transform.Find("Texts").transform.Find("Date_detail").GetComponent<TextMeshProUGUI>();
        detailMemoryMemo = GameObject.Find("Canvas_detailMemory_Parent").transform.Find("Canvas_detailMemory").transform.Find("Texts").transform.Find("MemoContent").GetComponent<TextMeshProUGUI>();
        detailMemoryLikes = GameObject.Find("Canvas_detailMemory_Parent").transform.Find("Canvas_detailMemory").transform.Find("Texts").transform.Find("Likes_detail").GetComponent<TextMeshProUGUI>();
        fullHeart = GameObject.Find("Canvas_detailMemory_Parent").transform.Find("Canvas_detailMemory").transform.Find("Buttons").transform.Find("FullHeart").gameObject;
        blankHeart = GameObject.Find("Canvas_detailMemory_Parent").transform.Find("Canvas_detailMemory").transform.Find("Buttons").transform.Find("BlankHeart").gameObject;
    }
    public void start_showMemory()
    {
        StartCoroutine("showMemory");
    }
    public IEnumerator showMemory()
    {
        GameObject.Find("Canvas_detailMemory_Parent").transform.Find("Canvas_detailMemory").gameObject.SetActive(true);
        tmpUri = contentUri;
        tmpUri += user_idx;
        tmpUri += "/";
        //contentUri += EventSystem.current.currentSelectedGameObject.GetComponent<ContentIdx>().thisIdx.ToString();
        tmpUri += GetComponent<ContentIdx>().thisIdx.ToString();
        DataSaver.instance.currMemoryidx = GetComponent<ContentIdx>().thisIdx.ToString();
        //현재 보고 있는 기억의 idx를 인스턴스로 저장 DataSaver 스크립트에 해당 변수 존재함

        using (UnityWebRequest memoryRequest = UnityWebRequest.Get(tmpUri))
        {
            yield return memoryRequest.SendWebRequest();
            RequestingResult_Memory resultMemory = JsonUtility.FromJson<RequestingResult_Memory>(memoryRequest.downloadHandler.text);
            //memoryParent = resultMemory.data;
            memoryInfo = resultMemory.data;
            Debug.Log(memoryInfo[0].createDate);
            //Debug.Log(resultMemory.data);
            
            UnityWebRequest MemoryImgwww = UnityWebRequestTexture.GetTexture(memoryInfo[0].contentsImg);
            yield return MemoryImgwww.SendWebRequest();
            detailMemoryImg.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)MemoryImgwww.downloadHandler).texture;

            detailMemoryDate.text = memoryInfo[0].createDate;
            detailMemoryMemo.text = memoryInfo[0].memo;
            detailMemoryLikes.text = memoryInfo[0].likes.ToString();

            if (memoryInfo[0].isAlreadyLikes == true)
            {
                blankHeart.SetActive(false);
                fullHeart.SetActive(true);
            }
            if (memoryInfo[0].isAlreadyLikes == false)
            {
                blankHeart.SetActive(true);
                fullHeart.SetActive(false);
            }


        }

    }
    
}
