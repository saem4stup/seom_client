using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine;
using TMPro;
public class SignupProcess : MonoBehaviour
{
    public TMP_InputField signupID;
    public TMP_InputField signupPW;
    public TMP_InputField signupPW_check;
    public TMP_InputField signupName;
    public TMP_InputField signupBirth;
    public TextMeshProUGUI passportName;
    public TextMeshProUGUI passportAge;
    public TextMeshProUGUI passportImmigrationDate;

    public GameObject IDCheck;
    public GameObject PWCheck;
    public GameObject PWCheck_Check;
    public GameObject NameCheck;
    public GameObject BirthCheck;
    public GameObject diffPW;
    public GameObject birthFormat;
    public GameObject pinknextBTN;
    public GameObject imgRegisterUI;
    public GameObject signupUI;
    public GameObject graynextBTN;
    public Image userImage;
    public GameObject pinkRegisterBTN;
    public GameObject grayRegisterBTN;
    public GameObject userImageObj;
    public SpriteRenderer passportImage;

    private int userAge;
    private string userImmigrationDate;
    private Sprite userIMG;//�ε��� �̹��� ������ ����

    private string imgpath;
    private string PW;
    private string PW_check;
    private string rightID;

    private string userID;
    private string userPW;
    private string userName;
    private string userBirth;
    //�� 4���� ������ ���� ������

    private string url = "http://15.165.223.53:3000/";
    private string check_id_URL = "users/v1/check_id";
    private string signupURL = "users/v1/signup";
    private bool IDokay;
    private bool PWokay;
    private bool Nameokay;
    private bool Birthokay;
    private bool Imgokay;
    // Start is called before the first frame update
    void Start()
    {
       signupID.text = "Imdlwlsgh";

        IDokay = false;
        PWokay = false;
        Nameokay = false;
        Birthokay = false;
        Imgokay = false;

        userImageObj.SetActive(false);
    }
    


    // Update is called once per frame
    void Update()
    {
        if (signupPW.text != "")
        {
            PWCheck.SetActive(true);
        }
        if (signupPW.text != signupPW_check.text)
        {
            diffPW.SetActive(true);
            PWCheck_Check.SetActive(false);
        }
        if ((signupPW.text == signupPW_check.text)&&(signupPW_check.text!=""))
        {
            diffPW.SetActive(false);
            PWCheck_Check.SetActive(true);
            PWokay = true;
        }
        
        if (signupID.text != "" && IDokay==true)
        {
            IDCheck.SetActive(true);
        }
        
        if (signupName.text != "")
        {
            NameCheck.SetActive(true);
            Nameokay = true;
        }
        if (signupBirth.text != "" && signupBirth.text.Length == 6)
        {
            BirthCheck.SetActive(true);
            birthFormat.SetActive(false);
            Birthokay = true;
        }
        if ((signupBirth.text.Length != 6)&&(signupBirth.text.Length!=0))
        {
            BirthCheck.SetActive(false);
            birthFormat.SetActive(true);
        }
        if (rightID != signupID.text)//�ߺ�Ȯ�� �޾Ƴ��� ���̵� �ٲٸ�
        {
            IDokay = false;
        }
        if (IDokay == true && PWokay == true && Nameokay == true && Birthokay == true)
        {
            pinknextBTN.SetActive(true);
            graynextBTN.SetActive(false);
        }
        else
        {
            pinknextBTN.SetActive(false);
            graynextBTN.SetActive(true);
        }
        if (userImageObj.GetComponent<SpriteRenderer>().sprite!=null)
        {
            grayRegisterBTN.SetActive(false);
            pinkRegisterBTN.SetActive(true);
        }
        else if (userImageObj.GetComponent<SpriteRenderer>().sprite == null)
        {
            grayRegisterBTN.SetActive(true);
            pinkRegisterBTN.SetActive(false);
        }
    }

    public void showPassport()
    {
        passportName.text = userName;
        string thisYear = DateTime.Now.ToString("yyyy");
        string year4 = "";
        int year = 0;
        string userBirthYear = userBirth.Substring(0, 2);
        int userBirthInt = int.Parse(userBirth);
        
        int result = 0;
        if (userBirth[0] <= 1)//2000�����̶�� ����
        {
            year4 = "20" + userBirthYear;

            userAge = int.Parse(thisYear) - int.Parse(year4) + 1;
        }
        else if (userBirth[0] > 2)//19xx�����̶�� ����
        {
            year4 = "19" + userBirthYear;
            userAge = int.Parse(thisYear) - int.Parse(year4) + 1;
        }
        passportAge.text = userAge.ToString();
        userImmigrationDate = DateTime.Now.ToString("yyyy") + "." + DateTime.Now.ToString("MM") + "." + DateTime.Now.ToString("dd");
        passportImmigrationDate.text = userImmigrationDate;

        passportImage.sprite= Resources.Load<Sprite>(imgpath) as Sprite;
    }

    public void defaultImg()
    {
        userImageObj.SetActive(true);   
        imgpath = "Login/Image/default_bear_img";//Resources ������ ��θ� ���ش�.
        userImageObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(imgpath) as Sprite;
    }

    public void next()//nextBTN�� onclick�� �߰��� ��
    {
        userID = signupID.text;
        userPW = signupPW.text;
        userName = signupName.text;
        userBirth = signupBirth.text;
        signupUI.SetActive(false);
        imgRegisterUI.SetActive(true);
    }
    public void Checking_ID()
    {
        StartCoroutine("WebRequest_IDCheck");
    }
    public void SignUp()
    {
        Debug.Log(userID + userPW + userName + userBirth);
        userIMG = userImageObj.GetComponent<SpriteRenderer>().sprite;
        StartCoroutine("WebRequest_SignUp");
    }

    public IEnumerator WebRequest_SignUp()
    {
        //string json = JsonUtility.ToJson(User1);
        //byte[] path = File.ReadAllBytes(Application.dataPath + "/TestImg.png");
        byte[] path = File.ReadAllBytes("Assets/Resources/"+imgpath+".png");
        //Debug.Log(Application.dataPath + "/TestImg.png");


        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("id", System.Text.Encoding.UTF8.GetBytes(userID)));
        form.Add(new MultipartFormDataSection("password", userPW));
        form.Add(new MultipartFormDataSection("name", userName));
        form.Add(new MultipartFormDataSection("birth", System.Text.Encoding.UTF8.GetBytes(userBirth)));
        form.Add(new MultipartFormFileSection("profileImg", path, "UserImg.png", "image/png"));

        UnityWebRequest request = UnityWebRequest.Post(url + signupURL, form);
        request.chunkedTransfer = false;
        yield return request.SendWebRequest();
        string result = request.downloadHandler.text;
        Debug.Log("��� : " + result);
        /*int index = result.IndexOf("�̹�");
        if (index == -1)
            Debug.Log("�̹� �����ϴ� ���̵�");
        Debug.Log(index);*/

    }

    public IEnumerator WebRequest_IDCheck()
    {
        Debug.Log("Pressed");

        WWWForm form = new WWWForm();
        form.AddField("id", signupID.text);

        UnityWebRequest request = UnityWebRequest.Post(url + check_id_URL, form);
        request.chunkedTransfer = false;
        yield return request.SendWebRequest();
        string result = request.downloadHandler.text;
        if (result.Contains("�̹�"))
        {
            Debug.Log("Already Exists!");
            IDokay = false;
            StopCoroutine("WebRequest_IDCheck");
        }
        else if (result.Contains("�ߺ��Ǵ�"))
        {
            Debug.Log("Pass!");
            IDokay = true;
            rightID = signupID.text;
            StopCoroutine("WebRequest_IDCheck");
        }
        else if (result.Contains("�ʿ���"))
        {
            Debug.Log("No value");
            StopCoroutine("WebRequest_IDCheck");
        }
        else if (request.responseCode == 500)
        {
            Debug.Log("Server error");
            StopCoroutine("WebRequest_IDCheck");
        }
        Debug.Log("��� : " + result);
        /*int index = result.IndexOf("�̹�");
        if (index == -1)
            Debug.Log("�̹� �����ϴ� ���̵�");
        Debug.Log(index);*/





    }
}
