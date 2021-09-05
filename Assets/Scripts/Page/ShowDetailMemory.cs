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
}

public class ShowDetailMemory : MonoBehaviour
{
    private string contentUri = "http://15.165.223.53:3000/memory/v1/contents/";
    public int user_idx = 30;
    public int island_idx = 16;
    public MemoryInfo[] memoryInfo;
    
    public GameObject detailMemoryImg;
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
    }
    public void start_showMemory()
    {
        StartCoroutine("showMemory");
    }
    public IEnumerator showMemory()
    {
        GameObject.Find("Canvas_detailMemory_Parent").transform.Find("Canvas_detailMemory").gameObject.SetActive(true);
        contentUri += user_idx;
        contentUri += "/";
        //contentUri += EventSystem.current.currentSelectedGameObject.GetComponent<ContentIdx>().thisIdx.ToString();
        contentUri += GetComponent<ContentIdx>().thisIdx.ToString();
        using (UnityWebRequest memoryRequest = UnityWebRequest.Get(contentUri))
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
            

        }

    }
}
