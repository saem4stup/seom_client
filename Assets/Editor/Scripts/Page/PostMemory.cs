using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json;
using TMPro;

public class PostMemory : MonoBehaviour
{
    public GameObject MemoryImg;
    public TextMeshProUGUI memoContent;
    public int useridx;
    public int islandidx;

    private string imgpath;
    private string url = "http://15.165.223.53:3000/";
    private string newMemoryURL = "memory/v1/memories";
    // Start is called before the first frame update
    void Start()
    {
        useridx = 30;
        islandidx = 16;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testPost()
    {
        defaultImg();
        StartCoroutine("PostNewMemory");
  
    }

    public void defaultImg()//사진 button클릭시 사용할 함수
    {
        MemoryImg.SetActive(true);
        imgpath = "Login/Image/default_bear_img";//Resources 이후의 경로를 써준다.
        //MemoryImg.GetComponent<RawImage>().texture = Resources.Load<RawImage>(imgpath).texture;
        MemoryImg.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(imgpath) as Sprite;

    }

    public IEnumerator PostNewMemory()
    {
        
        byte[] path = File.ReadAllBytes("Assets/Resources/" + imgpath + ".png");
        
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        
        form.Add(new MultipartFormFileSection("memoryImage", path, "MemoImg.png", "image/png"));
        form.Add(new MultipartFormDataSection("userIdx", System.Text.Encoding.UTF8.GetBytes(useridx.ToString())));
        form.Add(new MultipartFormDataSection("islandIdx", System.Text.Encoding.UTF8.GetBytes(islandidx.ToString())));
        form.Add(new MultipartFormDataSection("memo", System.Text.Encoding.UTF8.GetBytes(memoContent.text)));

        UnityWebRequest request = UnityWebRequest.Post(url + newMemoryURL, form);
        request.chunkedTransfer = false;
        yield return request.SendWebRequest();
        string result = request.downloadHandler.text;
        Debug.Log("결과 : " + result);
        /*int index = result.IndexOf("이미");
        if (index == -1)
            Debug.Log("이미 존재하는 아이디");
        Debug.Log(index);*/

    }
}
