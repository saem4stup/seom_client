using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;
public class TestUser
{
    public string id;
    public string password;
    public string name;
    public int birth;
    public string profileImg;
}
public class SigninTestUser
{
    public string id;
    public string password;
}
public class SignupLogin : MonoBehaviour
{
    public Button btnSend;
    public Button btnRefresh;
    public Button btnDelete;
    public Button btnPost;
    public TestUser User1;
    public SigninTestUser User2;

    private string url = "http://15.165.223.53:3000/";
    private string signinURL = "users/v1/signin";
    private string signupURL = "users/v1/signup";

    public TextMeshProUGUI loginID;
    public TextMeshProUGUI loginPW;

    public TextMeshProUGUI signupID;
    public TextMeshProUGUI signupPW;
    public TextMeshProUGUI signupName;
    public TextMeshProUGUI signupBirth;
    public Image signupProfileImg;

    // Start is called before the first frame update
    void Start()
    {
        User1 = new TestUser();
        User1.id = "9999";
        User1.password = "9999";
        User1.name = "jinho";
        User1.birth = 990909;
        User1.profileImg = "abc";

        User2 = new SigninTestUser();
        User2.id = "ljh9";
        User2.password = "9999";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SignUp()
    {
        StartCoroutine(WebRequest_SignUp());
    }
    public void SignIn()
    {
        StartCoroutine(WebRequest_SignIn());
    }
   
    

    public IEnumerator WebRequest_SignUp()
    {
        string json = JsonUtility.ToJson(User1);
        byte[] path = File.ReadAllBytes(Application.dataPath + "/TestImg.png");
        Debug.Log(Application.dataPath + "/TestImg.png");

        
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("id", System.Text.Encoding.UTF8.GetBytes((User1.id).ToString())));
        form.Add(new MultipartFormDataSection("password", User1.password));
        form.Add(new MultipartFormDataSection("name", User1.name));
        form.Add(new MultipartFormDataSection("birth", System.Text.Encoding.UTF8.GetBytes((User1.birth).ToString())));
        form.Add(new MultipartFormFileSection("profileImg",path,"TestImg.png","image/png"));
        
        UnityWebRequest request = UnityWebRequest.Post(url + signupURL, form);
        yield return request.SendWebRequest();
        String result = request.downloadHandler.text;
        Debug.Log("결과 : "+result);
        /*int index = result.IndexOf("이미");
        if (index == -1)
            Debug.Log("이미 존재하는 아이디");
        Debug.Log(index);*/

    }
    public IEnumerator WebRequest_SignIn()
    {
       
        WWWForm form = new WWWForm();
        form.AddField("id", "9999");
        form.AddField("password", "9999");
        UnityWebRequest request = UnityWebRequest.Post(url + signinURL, form);

        yield return request.SendWebRequest();
        String result = request.downloadHandler.text;
        Debug.Log("결과 : " + result);
        //Debug.Log(request.responseCode);
        if (request.responseCode == 200)
        {
            SceneManager.LoadScene("Main");
            StopCoroutine("WebRequest_SignIn");
        }

    }


}
