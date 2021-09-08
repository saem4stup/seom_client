using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class NewBOGEUM2Server : MonoBehaviour
{
    public int userIdx;
    public string deceasedName;
    public string deceasedBirth;
    public string deceasedDeath;
    public string relation;
    public string deceasedProfileImg;

    private string uri = "http://15.165.223.53:3000/main/v1/island";
    
    public void Send()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        byte[] image = File.ReadAllBytes(deceasedProfileImg);

        form.Add(new MultipartFormDataSection("userIdx", System.Text.Encoding.UTF8.GetBytes((userIdx).ToString())));
        form.Add(new MultipartFormDataSection("deceasedName", deceasedName));
        form.Add(new MultipartFormDataSection("deceasedBirth", deceasedBirth));
        form.Add(new MultipartFormDataSection("deceasedDeath", deceasedDeath));
        form.Add(new MultipartFormDataSection("relation", relation));
        form.Add(new MultipartFormFileSection("deceasedProfileImg", image, "deceasedImage.png", "image/png"));


        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();           
        }
    }
    private void Awake()
    {
        userIdx = GameObject.Find("SEOMInfo").GetComponent<GetSEOMInfo>().user_idx;
    }
}
