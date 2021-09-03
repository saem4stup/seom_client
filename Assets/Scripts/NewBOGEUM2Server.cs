using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NewBOGEUM2Server : MonoBehaviour
{
    public int userIdx;
    public string deceasedName;
    public string deceasedBirth;
    public string deceasedDeath;
    public string relation;
    public string deceasedProfileImg;

    private string uri = "http://15.165.223.53:3000//main/v1/island";
    

    public void Start()
    {
        StartCoroutine(Upload());
    }
    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.my-server.com/myform", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
