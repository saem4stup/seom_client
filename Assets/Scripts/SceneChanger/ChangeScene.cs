using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToSignup()
    {
        SceneManager.LoadScene("Signup");
        Debug.Log("SceneLoaded");
    }
    public void ChangeToLogin()
    {
        SceneManager.LoadScene("Login");
        Debug.Log("SceneLoaded");
    }


}
