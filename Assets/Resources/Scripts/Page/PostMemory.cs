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

    public void defaultImg()//���� buttonŬ���� ����� �Լ�
    {
        MemoryImg.SetActive(true);
        imgpath = "Login/Image/default_bear_img";//Resources ������ ��θ� ���ش�.
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
        Debug.Log("��� : " + result);
        /*int index = result.IndexOf("�̹�");
        if (index == -1)
            Debug.Log("�̹� �����ϴ� ���̵�");
        Debug.Log(index);*/

    }
}
