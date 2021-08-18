using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoProfileScene : MonoBehaviour
{
    public void Change()
    {
        SceneManager.LoadScene("EditProfile");
    }
}
