using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class RequestingResult_Like
{
    public string status;
    public bool success;
    public string message;
    public MemoryLikeInfo data;
}


[System.Serializable]
public class MemoryLikeInfo
{
    public bool isLike;
    public int likes;
}

public class PutLikeToMemory : MonoBehaviour
{
    private string tmpUri = "";
    private string likeUri = "http://15.165.223.53:3000/memory/v1/contents/likes/";
    public MemoryLikeInfo memoryLikeInfo;
    public TextMeshProUGUI numOfLikes;
    public GameObject fullHeart;
    public GameObject blankHeart;
    public void start_likeMemory()
    {
        StartCoroutine("likeMemory");
    }
    public IEnumerator likeMemory()
    {
        //tmpUri = DataSaver.instance.userIdx.ToString();
        tmpUri = likeUri;
        tmpUri += 30;
        tmpUri += "/";
        tmpUri += DataSaver.instance.currMemoryidx;

        WWWForm form = new WWWForm();
        //form.AddField("id", signupID.text);

        // Upload via post request
        var www = UnityWebRequest.Post(tmpUri, form);
        // change the method name
        www.method = "PUT";
        yield return www.SendWebRequest();
        RequestingResult_Like resultLike = JsonUtility.FromJson<RequestingResult_Like>(www.downloadHandler.text);
        memoryLikeInfo = resultLike.data;
        numOfLikes.text = memoryLikeInfo.likes.ToString();
        if (memoryLikeInfo.isLike == true)
        {
            fullHeart.SetActive(true);
            blankHeart.SetActive(false);
        }
        if (memoryLikeInfo.isLike == false)
        {
            fullHeart.SetActive(false);
            blankHeart.SetActive(true);
        }
        string result = www.downloadHandler.text;
        Debug.Log(result);

    }
}
